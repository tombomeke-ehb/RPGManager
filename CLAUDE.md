# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

**Important:** The user codes everything themselves to learn C#. Do NOT write or suggest C# code unless explicitly asked. Help with planning, system design, story, save/load data, and tradeoff explanations only.

This file is meant as a reference and design aid, not as a strict implementation script. Prefer explaining options, tradeoffs, and why one structure fits this project better than another. If a suggestion is made, explain when it is a good fit and what problem it avoids.

For a more human-readable planning companion, see [docs/IMPLEMENTATION_GUIDE.md](docs/IMPLEMENTATION_GUIDE.md).

## How To Use This File

- Use it to understand the current architecture, planned systems, and data model direction.
- Treat the TODO sections as design checkpoints, not mandatory code-generation instructions.
- Prefer the simplest implementation that keeps the next system easy to add later.
- Avoid solving future problems too early. Build one clean vertical slice first, then generalize.
- When comparing structures, explain the reason behind the choice. Example: a `Dictionary` is useful when lookup by ID matters more than preserving insertion order.

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

## World Lore: Tavaryn

Full lore documents live in `docs/lore/`. This section is the implementation-oriented summary — focusing on world flags, NPC IDs, and design details needed when building the systems. For the narrative version, read the docs.

### The Ashlands — First Region

The starter region. Level 1–10. Built on the ruins of the Emberveil Kingdom (fire mage nation that destroyed itself in a war two centuries ago). Players begin here. Completing the arc unlocks the second character slot and travel to the Mistwood.

**4 locations:**

| Name | Level | Role |
|---|---|---|
| Cinder's Rest | 1–3 | Starter hub — blacksmith, inn, bounty board |
| The Scorched Path | 2–5 | Travel corridor — bandit camps, random encounters |
| Emberveil Ruins | 5–8 | Mid-region dungeon — class-gated sealed chamber |
| The Ashen Throne | 8–10 | End-of-region — Hollow Court stronghold, volatile plateau |

**World flags for the Ashlands:**
- `AshlandsScavengerBounty` — first bounty quest done
- `ScorchedPathPatrolCleared` — first bandit patrol defeated
- `DrenMorrowFound` — injured traveler encountered
- `DrenMorrowAllied` / `DrenMorrowBetrayed` — branching NPC outcome (mutually exclusive)
- `EmberSealBroken` — sealed chamber accessed (any class)
- `AshlandsCourtFound` — player found evidence of the Hollow Court
- `AshlandsCourtExposed` — player learned the Court's name
- `EmberArtifactSecured` — artifact retrieved before the Court
- `ThreshDefeated` / `ThreshNegotiated` / `ThreshAlly` — bandit leader outcome (mutually exclusive)
- `AshlandsComplete` — major objectives done, unlocks next region

---

### NPCs

Five named NPCs. Full profiles in `docs/lore/npcs.md`.

| NPC ID | Name | Role | Location |
|---|---|---|---|
| `korin_ashwell` | Korin Ashwell | Blacksmith, shop, minor quest giver | Cinder's Rest |
| `sera_voss` | Sera Voss | Innkeeper, information hub, first quest giver | Cinder's Rest |
| `dren_morrow` | Dren Morrow | Injured traveler, failed Court operative, branching choice | Scorched Path |
| `lira_the_ashen` | Lira the Ashen | Hermit mage, ruins gate, lore source | Emberveil Ruins edge |
| `commander_thresh` | Commander Thresh | Bandit leader, multiple resolution paths | Scorched Path / Ashen Throne |

**Class-specific access notes:**
- Lira only speaks to Mage on first encounter; other classes need ≥ 30 reputation (Known tier) or `DrenMorrowAllied`
- Korin is suspicious of Archers until Known reputation
- Thresh is more open to negotiation with Warriors (soldier-to-soldier respect)
- Dren reveals more to Mages (assumes educated = trustworthy)

---

### Enemies

Six enemy types for the Ashlands. Full details in `docs/lore/ashlands.md`.

| Name | Element | Behavior | Level | Notes |
|---|---|---|---|---|
| Ash Scavenger | None | Aggressive | 1–2 | Pack animals, teaches basic combat |
| Scorched Path Bandit | None | Aggressive | 2–4 | Basic melee/ranged, drops weapons + gold |
| Bandit Enforcer | None | Defensive (heals at <50% HP) | 4–6 | Always with regular bandits |
| Ember Sprite | Fire | Random | 3–5 | 30% Burn on hit, weak to Ice |
| Ash Wraith | Fire | Defensive (phases at <30% HP) | 5–8 | Resistant to physical; Mages deal 2× damage |
| Hollow Court Sentinel | None | Defensive (calls reinforcements at <40% HP) | 7–10 | Always drops Court Sigil on first kill |

---

### First Quest Chain

Four quests that teach each major system. Full details in `docs/lore/ashlands.md`.

| Quest | Giver | Type | Teaches | Flag set |
|---|---|---|---|---|
| "Teeth at the Gate" | Sera Voss | Kill 5 Ash Scavengers | Combat basics | `AshlandsScavengerBounty` |
| "The Scorched Road" | Sera Voss | Travel + defeat bandit patrol | Travel + random encounters | `ScorchedPathPatrolCleared` |
| "The Stranger's Burden" | Auto-trigger | Find Dren Morrow + choice | World flags + shared consequences | `DrenMorrowAllied` or `DrenMorrowBetrayed` |
| "Echoes in the Ruins" | Lira the Ashen | Class-gated dungeon | Class-specific mechanics + Court intro | `EmberSealBroken`, `AshlandsCourtFound` |

---

### The Hollow Court — Ashlands Summary

Full profile in `docs/lore/hollow-court.md`.

In the Ashlands, the Court operates through two proxies — Commander Thresh (doesn't know who funds him) and Dren Morrow (a courier who was abandoned). They want the Emberveil Focus artifact from the ruins. Their Sentinels patrol the Ashen Throne and the deep ruins. The arc ends with the player learning the Court's name but not much more — establishing the pattern that repeats in every region.

The Court's long-game goal: repurpose the ancient Compact's magical seals to concentrate power rather than preserve stability. This unfolds across all regions. The player won't understand the full picture until the late game.

---

## System Plans

Design notes per system, listed roughly in dependency order. These are not instructions — they are explanations of why certain structures fit this game better than others, and what each system needs to connect to.

---

### Combat
**Branch:** `feature/combat`

Everything meaningful in this game depends on combat: XP needs a source, loot needs a moment to drop, spells need a context, world flags need something to react to. It is the natural first system to build.

**Why an interface for combatants:** At some point your combat loop needs to treat heroes and enemies the same way — same `TakeDamage()`, same health check, same turn logic. If you put that logic in `Character`, enemies have to inherit from it, which makes no sense. An interface lets both sides of a fight implement the same contract without forcing a shared class hierarchy. Each type keeps its own structure; the interface just guarantees they both speak the same language to the combat loop.

**Why a state machine for the combat flow:** A fight has distinct phases — player acts, enemy acts, effects resolve, check for death, repeat. If you model those as an enum and a switch statement, each phase only decides what happens in that phase and where to go next. Compare that to a chain of nested `if` statements: it works for a simple loop, but the moment you add "enemy calls reinforcements at 40% HP" or "player is frozen and skips their turn", nested conditions become very hard to follow. A state machine stays clean regardless of how many special cases you add.

**Why a loot table as a list of chances instead of a fixed drop:** A fixed drop means every kill gives the same item. A list of entries — each pairing an item with a probability — means you control rarity without writing special-case logic. Roll once per entry, compare against the probability, drop or skip. This way a common item might drop 80% of the time and a rare one 5%, and adding a new drop to an enemy is just adding one entry to its list.

**Why enemy behavior as an enum instead of hardcoded logic:** Even three behavior types — aggressive (always attacks), defensive (heals when low), random (picks from available actions) — make fights feel meaningfully different without building real AI. The behavior enum also makes it easy to add new types later without touching the combat loop itself.

**Why status effects on a shared list instead of baked into the loop:** If you handle Burn inside the attack logic and Freeze inside the turn logic and Poison somewhere else, you end up with effects scattered across the whole combat flow. A list of active effects on each combatant, processed in one pass at the end of each round, keeps all effect logic in one place. Each effect knows its own duration and what it does per tick — the loop just iterates and asks each one to tick.

**Connects to:** `GameMenu` Fight stub, `WorldState` (log the result), leveling (award XP on win), spells (Mage's actions during their turn).

---

### XP, Leveling & Character Roster
**Branch:** `feature/leveling`

**Why an array for XP thresholds instead of a formula:** A formula like `level² × 100` is clean but inflexible — if level 5 to 6 feels too fast, you have to change the formula and rebalance everything. An array lets you tune each individual threshold independently. Index is the level, value is the total XP to reach the next level. Easy to read, easy to change.

**Why an abstract level-up method instead of a switch in `Character`:** If `Character.LevelUp()` has `if (this is Warrior) ... else if (this is Mage) ...`, you have violated the reason subclasses exist. Each subclass should know how it grows. An abstract `OnLevelUp()` means `Warrior` defines Warrior growth, `Mage` defines Mage growth, and the base class handles only what is truly shared: incrementing the level, checking the threshold, logging the event. Adding a new class later does not touch the base class at all.

**Why `CharacterRoster` as a wrapper instead of a raw list:** The roster has rules: you cannot add a character until a slot is unlocked, and the active character needs to be tracked. A raw `List<Character>` enforces nothing. A wrapper class is the gatekeeper — it owns the unlock logic, it knows which character is active, and it decides whether adding another hero is currently allowed. `GameMenu` asks the roster; it does not reimplement the rules itself.

**Why store unlock conditions as `Dictionary<int, int>` (slot → required level):** Unlock conditions are a mapping. Slot 2 unlocks at level 10, slot 3 at level 20. A dictionary expresses that directly. If you hardcode `if (level >= 10) unlockSlot2` in multiple places, adding a fourth slot means hunting down every hardcoded check. With a dictionary, you add one entry.

**Connects to:** `Character` (needs Level and Experience), `SaveManager` (needs to persist them), `GameMenu` (character creation needs to check roster state).

---

### Shared World State
**Branch:** `feature/world-state`

This is the core identity of the game. It can be started alongside combat — combat just needs somewhere to report results when it exists.

**Why `Dictionary<string, bool>` for flags instead of individual properties:** The Ashlands alone has ten flags. Across all regions you will have dozens. Storing each as a named property on a class means recompiling the save model every time the world grows. A dictionary lets the world expand without touching the class structure. The only risk is typos — which is why you define all the key names as constants in one place rather than writing string literals in every system that sets or reads a flag.

**Why store reputation as a raw `int` and compute the tier:** If you store both the score (72) and the label ("Trusted"), you have two pieces of data describing the same truth. Sooner or later they get out of sync — a bug updates the score but not the label, and now your NPC reacts as "Trusted" when the score says "Known". The tier is always computed from the score. There is one source of truth.

**Why cap the event log:** Without a cap, a long play session generates thousands of entries. The log exists to let systems observe what happened recently, not to record the full history of the game. 200 entries is enough context; beyond that you are just growing the save file for no benefit.

**Why a singleton or static class:** World state is not per-character — it is per save file, shared across everyone. If you pass a `WorldState` instance through every method call, you are plumbing it through combat, dialogue, quests, and travel simultaneously. A singleton or static class makes it accessible from anywhere without that overhead. The tradeoff is testability, but for a single-player console game the tradeoff is worth it.

**Connects to:** virtually every other system eventually writes to it; `SaveManager` needs to serialize it alongside characters.

---

### NPC Dialogue
**Branch:** `feature/npc-dialogue`

**Why store the tree in a `Dictionary<string, DialogueNode>`:** A dialogue tree is navigation by node ID. "After the player chooses option 2, go to node `blacksmith_post_quest`." If nodes are stored in a list, finding the next node means searching the whole list. A dictionary makes that lookup instant. It also makes the whole tree serializable later if you want to load dialogue from a data file — the structure maps cleanly to JSON.

**Why conditions and actions as interfaces instead of if/else:** You will need at least three kinds of conditions: flag checks, reputation tier checks, class checks. If all of that logic lives in `DialogueOption`, the class becomes a tangle of special cases. An `IDialogueCondition` interface with a single `IsMet()` method means each condition type is one small, focused class. Adding a new condition type later means adding a class, not editing existing logic. Same reasoning for actions: `SetFlagAction`, `AddReputationAction`, `GiveQuestAction` each do one thing and are composable per dialogue option.

**Why hardcode first, data-drive later:** You do not know what the dialogue structure needs to feel like until you have played through a conversation. Build the first NPC tree in plain C# — just objects linked by ID. Once it works and feels right, you have a clear picture of the data shape. Then you can decide whether it needs to be in a file. Designing the file format before you have a working tree is designing blind.

**Connects to:** `WorldState` (conditions read flags and reputation), `NPC` base class (needs to carry a tree), `UI/` (a runner that walks the tree for any NPC), quests (dialogue options that start quests).

---

### Inventory
**Note:** No dedicated branch — can be done as part of `feature/economy` or as a standalone step before it.

**Why a wrapper class instead of a raw `List<IEquipable>` on `Character`:** A raw list has no rules. Any code anywhere can add items without checking capacity. An `Inventory` class owns the rules: `AddItem()` only succeeds if there is space, `RemoveItem()` handles missing items gracefully, and the rest of the game does not need to know how any of that works. The class is the gatekeeper.

**Why compute `UsedSlots` instead of storing it:** If you store a `usedSlots` counter and update it in `AddItem()` and `RemoveItem()`, you need to remember to update it correctly in every code path. If you ever forget once, the counter drifts from reality and you get a bug that is hard to reproduce. Summing the sizes of the items currently in the list always gives the correct answer. The computation is cheap; the synchronization problem is not worth creating.

**Connects to:** `Character` (replaces loose weapon fields), `CharacterFactory` (starting items), `SaveManager` (save the full contents), economy/shops (buy and sell need a working inventory to operate on).

---

### World & Travel
**Branch:** `feature/travel`

**Why `Location` as a data bag, not a behavior class:** A location does not *do* things — it describes a place. Its name, what level is required to enter, whether it is currently accessible, which NPCs are there, what enemies can appear. Other systems read that data and decide what to do with it. Keeping a location as mostly-data makes it easy to add new locations without worrying about behavior.

**Why an encounter pool as a list of groups instead of individual enemies:** A single Ash Scavenger encounter and a group of three Ash Scavengers are different experiences. A pool of `EnemyGroup` entries — each representing a coherent encounter — gives you that variety. When travel triggers an encounter, pick one group at random from the pool. Easy to add new encounter configurations without changing the travel logic.

**Why the Ashlands first:** The Ashlands is a complete, self-contained loop: starter hub, travel corridor, dungeon, end boss area. Building it end to end proves the travel system, encounter logic, and region unlocking all work together. Building three regions simultaneously before any of them is playable is building scaffolding with nothing to hang on it.

**Connects to:** `WorldState` (lock/unlock locations based on flags), combat (encounters trigger a fight), `GameMenu` Explore stub (replace with a real location menu).

---

### Quests
**Branch:** `feature/quests`

**Why quest progress lives in `WorldState` instead of the `Quest` object:** Progress like "killed 3 of 5 bandits" is a counter in the world — it belongs in `WorldState`, not inside the quest. This means the counter persists automatically, is shared across all characters, and is written by combat (which already interacts with `WorldState`) rather than by some separate quest-notification system. The quest just checks the counter to know if its objective is complete.

**Why `IQuestObjective` as an interface:** Kill objectives read a counter. Reach objectives check a location flag. Talk objectives check an NPC dialogue flag. If all of that logic is inside `Quest`, you eventually get a class full of mixed concerns. An interface with `IsComplete()` and `GetProgressText()` lets each objective type handle its own logic. Adding a new type means adding a class, not editing `Quest`.

**Why separate `Quest` (data) from `QuestTracker` (behavior):** A quest describes what needs to be done and what the reward is. It does not decide whether it can be accepted, whether it is complete, or what happens when it finishes. That is `QuestTracker`'s job. Keeping them separate means `Quest` objects are simple and reusable; `QuestTracker` is the single place where quest state is managed.

**Connects to:** `WorldState` (progress counters and completion flags), dialogue (quest givers), combat (kill objectives), `GameMenu` (quest log screen).

---

### Economy
**Branch:** `feature/economy`

**Why `NPC` optionally owns a `Shop` instead of `Shop` depending on an NPC:** Not every NPC has a shop. If `Shop` requires an NPC, you cannot have a shop without one. If `NPC` has an optional `Shop` property, a shopkeeper NPC just has a shop attached, while a quest giver or traveler does not. The dependency points in the right direction.

**Why price calculation takes the character as an argument:** Reputation discounts need to know who is buying. A method that takes the character can look up their reputation in the current region and apply the appropriate discount. If price were a static property on the item, you would need to recalculate it every time reputation changed — or keep a separate "current price" somewhere that can drift out of sync.

**Why sell price is always lower than buy price:** Standard RPG economy. The player always loses value selling — that is the friction that makes gold feel meaningful. Without it, players can exploit price differences between locations or buy-sell the same items for profit with no downside.

**Connects to:** `Inventory` (buy adds, sell removes), `WorldState` (reputation affects prices), dialogue (Trade option only appears when `npc.Shop != null`).

---

### Spells (expand existing)
**Branch:** `feature/spells`

**Why `Dictionary<Spell, int>` for cooldowns instead of a field on `Spell`:** A cooldown on the `Spell` object itself would be global — every Mage casting Fireball shares the same cooldown counter. A dictionary on the `Mage` maps each known spell to *that Mage's* remaining cooldown. Two Mages can each track their own state independently.

**Why mana regen per turn instead of fixed mana:** If mana does not regenerate, the Mage casts all spells immediately and then fights with basic attacks for the rest of the fight — there is no decision to make. Regen that is less than a full spell cost forces the player to think about timing: save mana for a big hit, or spend it early and regenerate into smaller casts?

**Why spells describe intent and `CombatManager` resolves it:** If each spell calculates its own damage, applies its own status effects, and checks its own resistances, adding a new spell means reimplementing all of that. If instead each spell says "I want to do fire damage to one target with a chance of Burn" and `CombatManager` resolves what that means given resistances and active effects, spell creation becomes much lighter. The complexity lives in one place.

**Connects to:** combat (spells are player actions during their turn), `Element` enum already in the codebase (maps to status effects), `Mage` class (spell list and cooldown tracking).

---

## Development Phases

This table shows a natural dependency order. It is not a mandate — you can start `feature/world-state` at any time since it depends on nothing, and `feature/spells` can run alongside combat once the basic loop exists.

| Branch | What it enables |
|---|---|
| `feature/combat` | Fights, XP source, loot drops |
| `feature/leveling` | Progression, character roster slots |
| `feature/world-state` | Shared consequences, reputation |
| `feature/spells` | Mage depth, elemental combat |
| `feature/npc-dialogue` | Reactive NPCs, class-specific dialogue |
| `feature/travel` | Exploration, regions, random encounters |
| `feature/quests` | Structured goals, rewards, story beats |
| `feature/economy` | Shops, inventory depth, gold loop |
