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

NPCs don't react to the character, they react to **the Keeper** (you). Dialogue, prices, quests, and hostility all change based on your collective reputation — not just what the current character has done.

### Character Unlock System

- You start with **1 character slot**.
- Slots unlock when you hit specific milestones (e.g., first character reaches level 10 → slot 2 unlocks, level 20 → slot 3, etc.).
- Each class is useful in different scenarios — a Warrior can't charm NPCs, a Mage can't pick locks, an Archer can't tank hits.
- You switch characters freely. Progress for each is independent (XP, inventory, quests taken), but world consequences are shared.

### Story Direction

**The world is Tavaryn.** You are the **Keeper of Destiny**, an immortal entity that bonds with mortal heroes and guides them. You act through your champions.

Tavaryn has no single great war. Instead it has many smaller problems: corrupt lords, monster migrations, factions competing for power, ancient ruins waking up. No class can handle all of it. That's why the Keeper bonds with multiple heroes.

The underlying antagonist is the **Hollow Court** — a secret society of nobles who oppose the Keeper's growing influence. They operate in the shadows across every region and are exposed piece by piece over the full game.

---

## System Plans

These are the systems that need to be built, listed in dependency order.

---

### 1. Combat System
**Branch:** `feature/combat`

The first system to build. Everything else (XP, quests, loot, reputation) depends on combat existing.

**Key structural hints:**

- **ICombatant interface:** Consider making both `Enemy` and `Character` implement a shared `ICombatant` interface (something like: has `Name`, `CurrentHealth`, `MaxHealth`, a `TakeDamage()` method, and an `IsAlive` property). That way `CombatManager` can hold a `List<ICombatant>` and treat players and enemies the same way in the loop — no special casing.

- **CombatManager as a state machine:** A simple enum like `CombatPhase { PlayerTurn, EnemyTurn, ResolvingEffects, Victory, Defeat }` with a switch statement works really well here. Each state decides what happens next and transitions to the next state. Much cleaner than a big if/else chain as combat gets more complex.

- **Enemy loot table:** I'd store it as a list of entries, each with an item and a drop chance (a float between 0 and 1). When combat ends, loop through the entries and for each one roll `Random.NextDouble()` — if the roll is below the drop chance, that item drops. This way a common item might have a 0.8 chance and a rare one might have 0.05.

- **Enemy behavior:** Even a simple `BehaviorType` enum (`Aggressive`, `Defensive`, `Random`) gives you meaningful variety without building real AI. Aggressive always attacks, Defensive heals or guards when below half health, Random picks a random valid action.

- **Status effects:** Don't bake effects directly into the combat loop from the start. It's much cleaner to have an `ActiveEffect` list on each `ICombatant` and process all effects at the end of each round in one pass. Each effect knows its duration and what it does per tick (damage, stat reduction, skip turn, etc.).

**TODO — feature/combat:**
- [ ] Create `ICombatant` interface
- [ ] Create `Enemy` class under `Characters/Enemies/` implementing `ICombatant`
- [ ] Update `Character` to also implement `ICombatant`
- [ ] Create `CombatManager` under `Combat/` with a `CombatPhase` state machine
- [ ] Implement player action menu (Attack, Spell, Item, Flee)
- [ ] Implement basic enemy AI using `BehaviorType` enum
- [ ] Implement loot table and drop resolution on victory
- [ ] Implement status effect list and per-round processing
- [ ] Log combat result to `WorldState` when it exists
- [ ] Hook `CombatManager` into `GameMenu` — replace the Fight stub

---

### 2. XP, Leveling & Character Roster
**Branch:** `feature/leveling`

Depends on: Combat (as the primary XP source).

**Key structural hints:**

- **XP thresholds as an array:** The simplest approach is a static `int[]` where the index is the level and the value is the total XP needed to reach the *next* level. So `thresholds[1] = 100` means you need 100 XP to go from level 1 to 2. To check if the character should level up: compare `character.Experience` against `thresholds[character.Level]`. Easy to tune, easy to understand.

- **Per-class level-up bonuses:** Rather than putting a big switch in `Character`, consider an abstract method `OnLevelUp()` that each subclass overrides. `Warrior.OnLevelUp()` boosts MaxHealth and Power, `Mage.OnLevelUp()` boosts MaxMana and spell damage, etc. `Character.LevelUp()` handles the shared logic (increment level, log the event) and then calls `OnLevelUp()`. Clean separation.

- **CharacterRoster:** Keep it as a single class holding a `List<Character>` with a reference to the active one. The unlock check is just a method called after every level-up: loop through the slot conditions and unlock any that are now satisfied. You could store unlock conditions as a `Dictionary<int, int>` mapping slot number to the level required to unlock it — simple and easy to extend.

- **Locking new character creation:** The "Create new character" option in `GameMenu` should be disabled (or show an explanation) when the roster is full or the next slot hasn't been unlocked yet. The roster itself should expose a `CanAddCharacter` property so the menu doesn't need to know the unlock logic.

**Save system impact:** `CharacterSaveData` needs `Experience` and `Level` fields. Roster max slots and unlock state need their own entry in the save file.

**TODO — feature/leveling:**
- [ ] Add `Experience` and `Level` to `Character`
- [ ] Define XP threshold table
- [ ] Add abstract `OnLevelUp()` to `Character`, implement in each subclass
- [ ] Create `CharacterRoster` under `Characters/`
- [ ] Implement slot unlock via `Dictionary<int, int>` (slot → required level)
- [ ] Add `CanAddCharacter` property to `CharacterRoster`
- [ ] Update `GameMenu` switch-character flow and creation lock
- [ ] Update save system: add `Experience`, `Level`, roster state

---

### 3. Shared World State
**Branch:** `feature/world-state`

Depends on: nothing. Can be started alongside combat — combat just needs to call `WorldState.LogEvent()` and `WorldState.IncrementCounter()` when it exists.

**Key structural hints:**

- **Flags as a Dictionary with constant keys:** Use a `Dictionary<string, bool>` for world flags, but define all the key names as constants in one place — a static class like `WorldFlags` with entries like `const string BanditCampCleared = "AshlandsBanditCampCleared"`. If you scatter string literals everywhere and make a typo, the flag silently never gets set and it's very hard to debug. Central constants prevent that.

- **Counters the same way:** `Dictionary<string, int>` with a matching `WorldCounters` static class for key constants. `TotalEnemiesKilled`, `GoldSpent`, etc.

- **Reputation as raw ints, tiers computed:** Store reputation per region as a `Dictionary<string, int>` (region name → score). Don't store the tier label — compute it on the fly with a method like `GetReputationTier(string region)`. That way you never get a save file where the score and the tier are out of sync. The tiers are just: < 10 = Unknown, 10–29 = Stranger, 30–59 = Known, 60–99 = Trusted, 100–199 = Honored, 200+ = Legendary (negative: Suspicious, Distrusted, Feared, Enemy).

- **Event log with a size cap:** A `List<string>` works fine, but add a cap (say, 200 entries) and trim the oldest when you exceed it. Without a cap, long play sessions will accumulate thousands of entries and bloat the save file noticeably.

- **WorldState as a singleton:** Since there's only ever one world state per save file, a singleton pattern (or a static class) is a reasonable choice. It makes it easy to access from anywhere — `WorldState.SetFlag(WorldFlags.BanditCampCleared)` — without having to pass the instance through every method call.

**Save system impact:** `WorldState` needs its own `WorldStateSaveData` class serialized alongside the character list. Easiest approach: add a `WorldState` property to the root save object that `SaveManager` already writes.

**TODO — feature/world-state:**
- [ ] Create `WorldState` class (singleton or static) under `Worlds/`
- [ ] Add flags, counters, regional reputations, NPC relationships, event log
- [ ] Create `WorldFlags` and `WorldCounters` static key-constant classes
- [ ] Create `ReputationTier` enum and `GetReputationTier()` method
- [ ] Cap event log at 200 entries
- [ ] Create `WorldStateSaveData` and wire into `SaveManager`
- [ ] Add helper methods: `SetFlag()`, `GetFlag()`, `AddReputation()`, `IncrementCounter()`, `LogEvent()`

---

### 4. NPC Dialogue System
**Branch:** `feature/npc-dialogue`

Depends on: WorldState (conditions read flags and reputation).

**Key structural hints:**

- **Store the tree in a Dictionary:** Use `Dictionary<string, DialogueNode>` where each key is a node ID string (like `"blacksmith_intro"` or `"blacksmith_post_quest"`). Then jumping between nodes is just a dictionary lookup by ID. This also makes it easy to serialize the whole dialogue tree later if you want to load it from a file instead of hardcoding it.

- **Conditions as an interface:** An `IDialogueCondition` interface with a single `bool IsMet(WorldState state, Character character)` method is the cleanest approach here. Then you create separate small classes: `FlagCondition` (checks a world flag), `ReputationCondition` (checks tier in a region), `ClassCondition` (checks character type). Sounds like more work upfront, but once you have four or five NPCs with complex dialogue, you'll be very glad you did it this way instead of nesting if/else everywhere.

- **Actions as an interface too:** Same idea for things that happen when a dialogue option is picked — `IDialogueAction` with an `Execute(WorldState state, Character character)` method. Concrete actions: `SetFlagAction`, `AddReputationAction`, `GiveQuestAction`, `GiveItemAction`. Each option can have a list of actions to fire.

- **Start simple, hardcode first:** Don't try to load dialogue from JSON files or external data right away. Build the first few NPC trees as plain C# objects (just constructing nodes and linking them by ID in code). Get it working and feeling right first. You can always move to data-driven dialogue later — the interface-based conditions and actions will still work either way.

- **DialogueRunner in UI:** The `DialogueRunner` class should live in `UI/` and just walk the tree: display the current node's text, show available options (filtered by conditions), wait for input, fire actions, move to the next node. Keep it decoupled from any specific NPC.

**TODO — feature/npc-dialogue:**
- [ ] Create `DialogueNode` and `DialogueOption` under `Characters/NPCs/Dialogue/`
- [ ] Create `IDialogueCondition` interface + `FlagCondition`, `ReputationCondition`, `ClassCondition`
- [ ] Create `IDialogueAction` interface + `SetFlagAction`, `AddReputationAction`, `GiveQuestAction`
- [ ] Add `RootNodeId` and a node dictionary to the `NPC` base class
- [ ] Create `DialogueRunner` in `UI/`
- [ ] Rewrite `BlackSmith.Interact()` using the dialogue tree
- [ ] Build 2–3 NPC trees that react to WorldState flags to prove the system works

---

### 5. Inventory System
**Note:** Build this before Economy (shops need a working inventory). Can be done as part of `feature/economy` or as a prerequisite step.

**Key structural hints:**

- **Wrap a List, don't expose it raw:** An `Inventory` class that internally holds a `List<IEquipable>` and enforces capacity is much safer than letting `Character` hold a plain list. The class is the gatekeeper — `AddItem()` checks space before adding, `RemoveItem()` handles "item not found" gracefully.

- **Slot count as computed property:** Don't store the used slot count separately — compute it from the items themselves: sum up the `InventorySpaceAmount` value of each item in the list. That way it's always accurate and you can never have a desync between the stored count and reality. `HasSpace(IEquipable item)` just checks if `UsedSlots + item.InventorySpaceAmount <= MaxSlots`.

- **Replace CharacterFactory's default-weapons approach:** Right now weapons are added in a fairly ad-hoc way. Once `Inventory` exists, `CharacterFactory` should just create the character, then call `character.Inventory.AddItem(...)` for each starting item. Simple, consistent.

**TODO — Inventory:**
- [ ] Create `Inventory` class under `Items/`
- [ ] Computed `UsedSlots` property (sum of item sizes)
- [ ] `AddItem()` with capacity check, `RemoveItem()`, `HasSpace()`, `GetContents()`
- [ ] Wire `Inventory` into `Character` (replace current item fields)
- [ ] Update `CharacterFactory` to use the new inventory
- [ ] Update save system: `CharacterSaveData` saves/loads the full inventory

---

### 6. World & Travel System
**Branch:** `feature/travel`

Depends on: WorldState (region locks), Combat (random encounters en route).

**Key structural hints:**

- **Location as a data bag:** Each `Location` should know: its name, which region it belongs to, what the minimum level to enter is, whether it's currently unlocked (driven by a WorldState flag), which NPCs are present, and what enemy encounter pool it can draw from. It doesn't need to *do* much — it's mostly data that other systems read.

- **Travel as a method with a chance roll:** `World.TravelTo(Location destination)` should: check the character meets the level requirement, then roll a random number against the encounter rate for the route. If it triggers, kick off a `CombatManager` encounter with enemies drawn from the destination's pool. If not, arrive without a fight.

- **Encounter pools:** A `List<EnemyGroup>` where each `EnemyGroup` is a named set of enemies that can appear together. "3 bandits" is one group, "1 bandit captain + 2 bandits" is another. When an encounter triggers, pick randomly from the pool.

- **The Ashlands as your test bed:** Build just one region first — the Ashlands. Three or four locations, a couple of enemy pools, and a travel route between them. Get the whole loop working (travel → encounter → fight → arrive) before building other regions.

**TODO — feature/travel:**
- [ ] Add `RequiredLevel`, `RegionName`, `IsUnlocked`, `EncounterPool` to `Location`
- [ ] Create `EnemyGroup` class (named group of enemies)
- [ ] Implement `World.TravelTo()` with encounter roll
- [ ] Build Ashlands region: 3–4 locations + enemy pools + travel routes
- [ ] Replace the Explore stub in `GameMenu` with the travel menu
- [ ] Show current character location on the stat screen

---

### 7. Quest System
**Branch:** `feature/quests`

Depends on: WorldState (completion flags + progress counters), NPC Dialogue (quest givers), Combat (kill objectives).

**Key structural hints:**

- **Progress lives in WorldState:** Don't store quest progress inside the `Quest` object itself. Store it as counters in `WorldState`, with a naming convention like `"Quest_BanditHunt_Kill_Bandit"`. This automatically makes progress shared across all characters and persistent across sessions. When a bandit dies in combat, the combat system just calls `WorldState.IncrementCounter("Quest_BanditHunt_Kill_Bandit")` if that quest is active.

- **IQuestObjective interface:** An interface with `bool IsComplete(WorldState state)` and `string GetProgressText(WorldState state)` (for the quest log display) makes adding new objective types easy without touching `Quest`. `KillObjective` reads a counter, `ReachObjective` reads a flag, `TalkObjective` checks an NPC dialogue flag.

- **Quests as data, not logic:** The `Quest` class should be mostly data (title, description, objectives, reward, giver NPC, prerequisites) with no game logic inside it. `QuestTracker` holds the active/completed state and handles `StartQuest()`, `CheckCompletion()`, and `CompleteQuest()`. Separation of data from behavior.

- **Class-exclusive quests:** A nullable `RequiredClass` property on `Quest`. `QuestTracker.CanAccept(Quest quest, Character character)` checks it before offering the quest. The NPC dialogue condition `ClassCondition` already handles only showing those dialogue options to the right class — the quest system just needs to enforce it on accept too.

- **Completion is a world event:** When a quest completes, `QuestTracker` writes the completion flag to `WorldState`, logs the event, and hands out the reward (XP, gold, item, reputation change). No special casing needed — everything flows through the same systems.

**TODO — feature/quests:**
- [ ] Create `Quest`, `IQuestObjective`, `QuestReward` under `Quests/`
- [ ] Implement `KillObjective`, `ReachObjective`, `TalkObjective`
- [ ] Create `QuestTracker` — holds active/completed state, start/check/complete logic
- [ ] Wire kill counting into `CombatManager` (increment WorldState counters)
- [ ] Wire quest completion into `WorldState` (set flag, log event, add reputation)
- [ ] Add quest log screen to `GameMenu`
- [ ] Build 3–5 starter quests for the Ashlands to test the full pipeline

---

### 8. Economy System
**Branch:** `feature/economy`

Depends on: Inventory (items need to be addable/removable properly), WorldState (reputation affects prices).

**Key structural hints:**

- **Shop doesn't own an NPC — NPC optionally owns a Shop:** Don't make `Shop` depend on a specific NPC class. Instead, add a nullable `Shop` property to `NPC`. Some NPCs have a shop, some don't. The `DialogueRunner` checks `npc.Shop != null` when deciding whether to show a "Trade" option.

- **Price calculation as a method:** `Shop.GetPrice(IEquipable item, Character character)` takes the item's base price and applies a reputation discount. Something like: each reputation tier above Stranger gives a 5% discount, to a maximum of 25%. Keeps pricing logic in one place and easy to tune.

- **Sell price is always lower:** A simple convention like "sell price = 40% of buy price" works well. The player always takes a hit selling — that's standard RPG economy and prevents exploits.

- **Gold on enemies:** Add a `GoldReward` range (min/max) to `Enemy` or its loot table. Combat resolves a random value in that range on win and adds it directly to the character's gold. Simple.

**TODO — feature/economy:**
- [ ] Create `Shop` class under `Economy/`
- [ ] Add nullable `Shop` to `NPC` base class
- [ ] Implement `GetPrice()` with reputation modifier
- [ ] Implement buy and sell flows
- [ ] Add gold reward range to `Enemy` loot table
- [ ] Update `BlackSmith` to use the new `Shop`
- [ ] Add buy/sell to `DialogueRunner` when NPC has a shop
- [ ] Apply reputation-based discounts in `Shop.GetPrice()`

---

### 9. Spell System (expand existing)
**Branch:** `feature/spells` *(already exists)*

Depends on: Combat (spells are cast during combat turns).

**Key structural hints:**

- **Spell is data + one method:** Each spell should carry its stats (mana cost, cooldown, damage range, element, target type, status effect it applies) and a `Cast(ICombatant caster, List<ICombatant> targets)` method. The heavy lifting (damage calculation, resistance lookup, status effect application) happens in `CombatManager` — the spell just describes what it wants to do and `CombatManager` resolves it. This keeps spells simple to add.

- **SpellBook as a list with cooldown tracking:** `Mage` gets a `SpellBook` — a list of known spells. But you also need to track how many turns remain on each spell's cooldown. A `Dictionary<Spell, int>` mapping each known spell to its remaining cooldown works well. At the start of the Mage's turn, decrement all values. A spell is castable if its cooldown value is 0.

- **Mana regen:** The simplest version is: at the start of each of the Mage's turns, add a fixed mana regen amount (maybe 10–15% of max mana). This means the player has to think about when to cast — they can't open with all spells every fight.

- **Status effects:** Tie directly to the `Element` enum you already have. Fire → Burn (damage per turn), Ice → Freeze (skip turn), Lightning → Shock (reduced damage output), Poison → Poison (damage per turn + reduced max health). Use the `ActiveEffect` list from the combat system — spells just add an effect to the target's list on hit.

**TODO — feature/spells:**
- [ ] Add `ManaCost`, `Cooldown`, `Element`, `TargetType`, `StatusEffect` to `Spell` base class
- [ ] Give `Mage` a `SpellBook` (`List<Spell>`) and a cooldown tracker (`Dictionary<Spell, int>`)
- [ ] Implement mana regen at the start of each Mage turn in `CombatManager`
- [ ] Implement status effect application on hit via the ActiveEffect list
- [ ] Complete `Fireball` as the first full implementation
- [ ] Add `IceShard`, `LightningBolt`, `HealingLight` as further examples
- [ ] Wire `CastSpell()` on `Mage` into the player action menu in `CombatManager`

---

## Development Phases

Build in this order — each phase unlocks the next:

| Phase | Branch | What it enables |
|---|---|---|
| 1 | `feature/combat` | Fights, XP source, loot drops |
| 2 | `feature/leveling` | Progression, character roster slots |
| 3 | `feature/world-state` | Shared consequences, reputation (can start alongside phase 1) |
| 4 | `feature/spells` | Mage depth, elemental combat (can run alongside phase 3) |
| 5 | `feature/npc-dialogue` | Reactive NPCs, class-specific dialogue |
| 6 | `feature/travel` | Exploration, regions, random encounters |
| 7 | `feature/quests` | Structured goals, rewards, story beats |
| 8 | `feature/economy` | Shops, inventory depth, gold loop |
