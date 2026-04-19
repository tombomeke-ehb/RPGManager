# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

**Important:** The user codes everything themselves to learn C#. Do NOT write or suggest C# code unless explicitly asked. Help with planning, system design, story, and save/load data only.

---

## Build & Run

```bash
dotnet build                      # Build all projects
dotnet run --project RPGManager   # Run the game
```

**Publish single-file binaries** (used by CI):
```bash
dotnet publish RPGManager -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true
# Replace win-x64 with: win-arm64, osx-x64, osx-arm64, linux-x64, linux-arm64
```

There are no automated tests or linting tools configured.

**Update ROADMAP.md** after API changes:
```bash
dotnet run --project Tools/RoadmapUpdater
```

---

## Architecture

Three projects in the solution:
- **`RPGManager`** — Console entry point. Displays splash screen, then calls `GameMenu.Start()`.
- **`RPGManagerLib`** — All game logic. No external dependencies.
- **`Tools/RoadmapUpdater`** — Scans `RPGManagerLib` and auto-generates the "Project Overview" section of `ROADMAP.md`.

### Core Subsystems (Current)

**Character hierarchy** (`Characters/`): Abstract `Character` base holds health, mana, gold, and power level. `Warrior`, `Mage`, and `Archer` extend it. NPCs (`NPC`, `BlackSmith`) are separate from the hero hierarchy.

**Item/Equipment model** (`Items/`): `IEquipable` is the root interface. `Weapon` is the primary abstract base (damage, durability, `Rarity` enum, `Element` enum). Melee weapons, bows, quivers, and staffs live under `Items/Weapons/` and `Items/Staffs/`.

**Spell system** (`Spells/`): Base `Spell` with `Cast()`. `Fireball` is the only implementation. Mana-based casting is incomplete — branch: `feature/spells`.

**Save system** (`Saves/`): JSON via `System.Text.Json` with polymorphic deserialization. Save location defaults to `Save/characters.json`, overridable via `RPGMANAGER_SAVE_DIR`. Key classes: `SaveManager`, `CharacterSaveData`, `EquipableSaveData`, `WeaponSaveData`, `QuiverSaveData`. When adding a new system (WorldState, quests, etc.), a matching `*SaveData` class and JSON type discriminator registration is required.

**UI/Game loop** (`UI/`): `GameMenu` owns the main loop. `CharacterFactory` handles character creation. `MenuSystem` is a thin console-menu helper. Explore and Fight are stubs — new features slot in there.

**Exceptions** (`Exceptions/`): All derive from `CharacterException`.

### Key Design Conventions

- All public APIs carry XML doc comments (`<summary>`, `<param>`, `<returns>`, `<exception>`).
- Enums for game constants: `Rarity`, `Element`, `WeaponType`, `EquipableType`, `MagicType`, `InventorySpaceAmount`.
- When adding a new weapon type: class in subfolder + `*SaveData` class + register discriminator in save config.

---

## Game Design Vision

### The Central Mechanic: Shared World State

This is the defining design of the game. You manage a **roster of heroes** — not a party, but separate characters you switch between. Each hero has their own class, weapons, level, and story. But all of them exist in the **same living world**.

What one character does changes the world for all the others:
- Your Warrior defeats a bandit camp → NPCs in that region thank your Mage too, because they know she's your hero
- Your Mage breaks a magical seal → a door unlocks that your Archer can now walk through
- Your Archer assassinates a corrupt merchant → the economy in that town shifts, affecting prices for everyone

NPCs don't react to the character, they react to **the Keeper** (you). They know all your heroes are bound to you. Dialogue, prices, quests, and hostility all change based on your collective reputation — not just what the current character has done.

### Character Unlock System

- You start with **1 character slot**.
- Slots unlock when you hit specific milestones (e.g., first character reaches level 10 → slot 2 unlocks, level 20 → slot 3, etc.).
- Each class is useful in different scenarios — a Warrior can't charm NPCs, a Mage can't pick locks, an Archer can't tank hits.
- You switch characters freely. Progress for each is independent (XP, inventory, quests taken), but world consequences are shared.

### Story Direction (suggestion — adapt freely)

**The world is Tavaryn.** You are the **Keeper of Destiny**, an immortal entity that bonds with mortal heroes and guides them. You can't directly enter the world — you act through your champions.

Tavaryn has no single great war right now. Instead it has *many* smaller problems: corrupt lords, monster migrations, factions competing for power, ancient ruins waking up. No class can handle all of it. That's why the Keeper bonds with multiple heroes — a Warrior handles brute force, a Mage handles the arcane, an Archer handles infiltration and recon.

The underlying antagonist is the **Hollow Court** — a secret society of nobles from fallen kingdoms who oppose the Keeper's growing influence. They don't want Tavaryn unified under a legend. They operate in the shadows: assassins, saboteurs, corrupted merchants. Each hero will encounter them differently, in ways that fit their class. They're not fought in one battle — they're exposed and dismantled piece by piece, across all regions, over the full game.

There is no hard "end" — you build your legacy until you choose to stop.

---

## System Plans

These are the systems that need to be built, with the data each one needs to track. Listed in dependency order — build earlier ones first since later ones depend on them.

---

### 1. Combat System
**Branch:** `feature/combat`

The first thing to build. Everything else (XP, quests, loot, reputation) flows out of combat existing.

**Enemy needs:**
- Name, description
- Current health, max health
- Base damage, defense rating
- Level
- Element type + elemental resistances (tie to existing `Element` enum)
- Loot table: list of possible item drops + gold range + XP value
- Optional: behavior type (aggressive attacks first, defensive, support)

**CombatManager needs to track:**
- The active player character
- List of enemies in the encounter
- Whose turn it is
- Round counter
- Any active status effects (burn, freeze, poison — from `Element`)

**Turn structure:**
- Player turn: Attack / Use Spell / Use Item / Flee
- Enemy turn: chooses action based on behavior type
- After each round: process status effects, check win/lose

**Outcome:**
- Win: grant XP + loot to character, log world event ("Warrior defeated 3 bandits in Ashlands")
- Flee: no reward, maybe a penalty
- Lose: decide on a consequence (death = permanent loss, or respawn with penalty)

**TODO — feature/combat:**
- [ ] Create `Enemy` class under `Characters/Enemies/`
- [ ] Create `CombatManager` under a new `Combat/` folder
- [ ] Wire elemental resistances to existing `Element` enum
- [ ] Implement player action menu inside combat (Attack, Spell, Item, Flee)
- [ ] Implement basic enemy AI (attack the player)
- [ ] Handle win/lose outcomes with XP grant and loot drop
- [ ] Log combat result to `WorldState` (see system 3)
- [ ] Hook into `GameMenu` — replace the Fight stub

---

### 2. XP, Leveling & Character Roster
**Branch:** `feature/leveling`

Depends on: Combat (XP source).

**Character needs (additions to existing `Character`):**
- `Experience` (current XP total)
- `Level` (starts at 1)
- `ExperienceToNextLevel` (computed from a threshold table)

**Level-up logic:**
- Each level: increase MaxHealth, MaxMana, base power
- Per-class bonus on level-up (Warrior gets more health/power, Mage gets more mana/spell damage, Archer gets speed/crit)
- Trigger a level-up check after every XP gain

**CharacterRoster (new class):**
- List of all created characters
- Index of the currently active character
- `MaxSlots` (starts at 1)
- Unlock conditions: dictionary mapping slot number → milestone condition
  - Example: slot 2 unlocks when any character reaches level 10
  - Example: slot 3 unlocks when any character reaches level 20
- `TryUnlockSlot()` — called after every level-up to check if new slots are now available
- `SwitchCharacter()` — change active character

**Save system impact:** `CharacterSaveData` needs `Experience` and `Level` fields. Roster state (max slots, which characters exist) needs its own save entry.

**TODO — feature/leveling:**
- [ ] Add `Experience` and `Level` to `Character`
- [ ] Define XP threshold table (level 1→2: 100 XP, 2→3: 250, 3→4: 450, etc.)
- [ ] Implement per-class stat bonuses on level-up
- [ ] Create `CharacterRoster` class under `Characters/`
- [ ] Implement slot unlock conditions in `CharacterRoster`
- [ ] Add `SwitchCharacter` flow to `GameMenu`
- [ ] Lock "Create new character" in menu when roster is full or slot not unlocked
- [ ] Update save system for `Experience`, `Level`, and roster state

---

### 3. Shared World State
**Branch:** `feature/world-state`

Depends on: nothing — can be started in parallel with combat, but combat needs it to log events.

This is the engine of the multi-character mechanic. One `WorldState` object exists for the whole save file. Every character shares it.

**WorldState needs to track:**
- **Flags** — `Dictionary<string, bool>`: named world events. Examples: `"AshlandsBanditCampCleared"`, `"BlacksmithQuestComplete"`, `"HollowCourtMerchantExposed"`. Set by actions, read by dialogue and quest systems.
- **Counters** — `Dictionary<string, int>`: tracked quantities. Examples: `"TotalEnemiesKilled"`, `"GoldEarned"`, `"SpellsCast"`.
- **Regional Reputations** — `Dictionary<string, int>`: a reputation score per region. Score goes up (heroic acts, completed quests) or down (harming villagers, fleeing encounters). Score maps to a tier label.
- **NPC Relationships** — `Dictionary<string, int>`: per-NPC score, keyed by NPC ID. Separate from regional rep because some NPCs are personal.
- **Event Log** — `List<string>`: human-readable log of significant events. Used to show a "what happened in this world" summary.

**Reputation tiers (regional):**
- 0: Unknown
- 10+: Stranger
- 30+: Known
- 60+: Trusted
- 100+: Honored
- 200+: Legendary
- Negative versions: -10 Suspicious, -30 Distrusted, -60 Feared, -100 Enemy of the Keeper

**Save system impact:** `WorldState` needs its own `WorldStateSaveData` class. It should be saved alongside the character list in the same JSON file (or a separate `world.json`).

**TODO — feature/world-state:**
- [ ] Create `WorldState` class under a new `World/` folder (or `Worlds/`)
- [ ] Implement flags, counters, regional reputations, NPC relationships, event log
- [ ] Create `ReputationTier` enum and a method to compute tier from score
- [ ] Create `WorldStateSaveData` and wire into `SaveManager`
- [ ] Make `WorldState` a singleton or inject it where needed (combat, quests, dialogue)
- [ ] Add helper methods: `SetFlag()`, `GetFlag()`, `AddReputation()`, `LogEvent()`

---

### 4. NPC Dialogue System
**Branch:** `feature/npc-dialogue`

Depends on: WorldState (dialogue reads flags and reputation).

NPCs don't have static text — they have **dialogue trees** where each node can have conditions. Conditions check WorldState.

**DialogueNode needs:**
- An ID
- The text to display
- A speaker name
- List of `DialogueOption` choices (or empty = auto-advance / end)

**DialogueOption needs:**
- The text shown to the player
- An optional condition (`WorldState` flag check, reputation tier check, character class check)
- The next node to go to
- An optional action to trigger (set a world flag, start a quest, give an item)

**NPC gets:**
- A root `DialogueNode` ID (entry point)
- A unique string ID (used as key in WorldState NPC relationships)
- A `DialogueRole` enum: Merchant, QuestGiver, Lore, Hostile, etc.

**How class-specific dialogue works:**
- A dialogue option has a condition: `RequiredClass = CharacterClass.Mage`
- That option only appears when your Mage is talking to the NPC
- The text can acknowledge it: "Ah, a mage. The scholar I sent word to."

**How reputation affects dialogue:**
- Option condition: `RequiredReputation = ReputationTier.Trusted`
- Below that tier → option is hidden or replaced with a hostile version

**TODO — feature/npc-dialogue:**
- [ ] Create `DialogueNode` and `DialogueOption` classes under `Characters/NPCs/Dialogue/`
- [ ] Create `DialogueCondition` class (flag check, reputation check, class check)
- [ ] Create `DialogueAction` class (set flag, give quest, give item, modify reputation)
- [ ] Add `RootDialogueNodeId` and `NpcId` to `NPC` base class
- [ ] Create `DialogueRunner` in `UI/` — walks the tree, displays options, processes actions
- [ ] Replace `BlackSmith.Interact()` stub with dialogue tree
- [ ] Build 2-3 example NPC dialogue trees that react to WorldState flags

---

### 5. World & Travel System
**Branch:** `feature/travel`

Depends on: WorldState (region lock/unlock), Combat (random encounters during travel).

**Location additions:**
- `RequiredLevel` — can't enter if active character is below this
- `RegionName` — which reputation bucket this location belongs to
- `IsUnlocked` — driven by WorldState flag (e.g., "AshlandsUnlocked")
- List of available actions when present (talk to NPC, explore, rest, enter combat)
- List of `Enemy` encounter pools for this location

**World additions:**
- A travel method: from location A to location B
- Travel may trigger random encounters (roll against encounter rate)
- Some routes are locked until a WorldState flag is set

**TODO — feature/travel:**
- [ ] Add `RequiredLevel`, `RegionName`, `IsUnlocked` to `Location`
- [ ] Add encounter pool to `Location` (list of possible enemy groups)
- [ ] Implement travel between locations with encounter rolls
- [ ] Build the Ashlands as the starter region: 3–4 locations, connected travel routes
- [ ] Hook into `GameMenu` — replace the Explore stub with the travel menu
- [ ] Show character location in the character view screen

---

### 6. Quest System
**Branch:** `feature/quests`

Depends on: WorldState (completion flags), NPC Dialogue (quest givers), Combat (kill objectives).

**Quest needs:**
- ID, title, description
- Giver NPC ID
- List of `QuestObjective`
- `QuestReward`
- Optional: `RequiredClass` — only a certain class can accept this quest
- Optional: prerequisite flags (other quests must be done first)
- Completion flag name (written to WorldState when done)

**QuestObjective needs:**
- Type: Kill / Collect / Reach / Talk / Deliver
- Target ID (enemy type name, item name, location name, NPC ID)
- Required count (how many)
- Progress is tracked as a counter in WorldState

**QuestReward needs:**
- XP amount
- Gold amount
- Optional item reward
- Optional world flag to set (e.g., unlock a new NPC)
- Optional reputation change in a region

**QuestTracker (new class):**
- Holds a list of all quests defined in the game
- Tracks which quests are active (started but not completed)
- Tracks which are completed (by reading WorldState flags)
- `StartQuest()`, `UpdateProgress()`, `CompleteQuest()`
- Quest progress is stored in WorldState counters so it persists and is shared

**Note on shared quests:** Most quests complete once — regardless of which character finishes them. The WorldState flag is set, and all characters see it as done. Some quests can be class-specific (only the Mage can complete it), but completion still affects the world.

**TODO — feature/quests:**
- [ ] Create `Quest`, `QuestObjective`, `QuestReward` classes under `Quests/`
- [ ] Create `QuestTracker` as a singleton/injected service
- [ ] Wire quest progress updates into combat (kill objectives) and dialogue (talk objectives)
- [ ] Wire quest completion into `WorldState` (set flag, log event, add rep)
- [ ] Add quest log to `GameMenu` (view active quests + progress)
- [ ] Build 3–5 starter quests in the Ashlands to test the system

---

### 7. Economy System
**Branch:** `feature/economy`

Depends on: Inventory (items need to be addable/removable), WorldState (prices can shift based on flags).

**Shop needs:**
- NPC owner (or standalone)
- List of items for sale, each with a price
- `Buy()` — remove gold from character, add item to inventory
- `Sell()` — remove item from inventory, add gold to character
- Optional: price modifier based on reputation (Trusted = 10% discount)

**Inventory additions** (completing the existing TODO in `CharacterFactory`):
- `Inventory` class with a list of `IEquipable` items
- Max capacity using existing `InventorySpaceAmount` (small=1 slot, large=2 slots)
- `AddItem()`, `RemoveItem()`, `HasSpace()`, `GetContents()`
- Replace the default weapons approach in `CharacterFactory` with proper inventory initialization

**TODO — feature/economy:**
- [ ] Create `Inventory` class under `Items/` — replaces ad-hoc item handling
- [ ] Wire `Inventory` into `Character` (replace current weapon fields)
- [ ] Update save system for `Inventory`
- [ ] Create `Shop` class under a new `Economy/` folder
- [ ] Add buy/sell menus to relevant NPC dialogues (BlackSmith is already there)
- [ ] Add gold drops to enemy loot tables
- [ ] Apply reputation-based price modifiers in `Shop.GetPrice()`

---

### 8. Spell System (expand existing)
**Branch:** `feature/spells` *(already exists)*

Depends on: Combat (spells are used in combat), WorldState (some spells interact with world flags).

**Spell additions:**
- `ManaCost` — deducted from caster's mana on cast
- `Cooldown` — turns before it can be used again
- `Element` — tie to existing enum, used for resistance calculations
- `StatusEffect` — optional effect applied on hit (Burn, Freeze, Poison, Stun)
- `TargetType` — Single / AoE / Self

**Mage gets:**
- A `SpellBook` (list of known spells) instead of just weapons
- Spells learned on level-up or found as loot
- Mana regenerates partially at the start of each combat turn

**Status effects:**
- Burn (Fire): damage each turn for N turns
- Freeze (Ice): skip a turn
- Poison (Poison): damage each turn, reduces max health
- Shock (Lightning): reduces enemy damage output

**TODO — feature/spells:**
- [ ] Add `ManaCost`, `Cooldown`, `Element`, `StatusEffect`, `TargetType` to `Spell` base class
- [ ] Implement `StatusEffect` enum and a per-entity active effects list
- [ ] Process status effects each combat turn in `CombatManager`
- [ ] Give `Mage` a `SpellBook` and wire `CastSpell()` into combat
- [ ] Implement mana regen per turn
- [ ] Add `Fireball` full implementation as the first complete spell
- [ ] Add 2–3 more spells (IceShard, LightningBolt, HealingLight) as examples

---

## Development Phases

Build in this order — each phase unlocks the next:

| Phase | Branch | What it enables |
|---|---|---|
| 1 | `feature/combat` | Fights, XP source, loot drops |
| 2 | `feature/leveling` | Progression, character roster slots |
| 3 | `feature/world-state` | Shared consequences, reputation |
| 4 | `feature/spells` | Mage depth, elemental combat |
| 5 | `feature/npc-dialogue` | Reactive NPCs, class-specific dialogue |
| 6 | `feature/travel` | Exploration, regions, random encounters |
| 7 | `feature/quests` | Structured goals, rewards, story beats |
| 8 | `feature/economy` | Shops, inventory depth, gold loop |

Phases 3 and 4 can be worked in parallel — they don't depend on each other.
