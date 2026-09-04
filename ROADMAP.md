# RPG Manager – Development Roadmap

![Status: Active](https://img.shields.io/badge/Status-Active-success)
![Version: v0.1.2](https://img.shields.io/badge/Version-v0.1.2-blue)
![Platform: .NET 8](https://img.shields.io/badge/Platform-.NET%208-512BD4)

Welcome to the official development roadmap for **RPG Manager**, a text-based RPG engine built in C# by Tombomeke Studios.

---

## Vision

RPG Manager is built around one core idea: you manage a **roster of heroes**, not a single character. Each hero has their own class, level, and inventory — but they all share the same living world. What one hero does changes how NPCs talk to the others, unlocks paths, and shifts reputations across every region of Tavaryn.

The long-term goal is a fully playable, story-driven console RPG where:
- Multiple hero classes each have unique strengths that open different paths
- The world remembers what you've done — across all your characters
- Combat, quests, and exploration grow in depth with each release
- The architecture stays clean enough to extend indefinitely

---

## Milestones

| Version | Milestone | Status | Target |
|:---|:---|:---|:---|
| **v0.1** | Core Engine — classes, weapons, save system | Done | Oct 2025 |
| **v0.2** | World & Travel — locations, regions, travel loop | In Progress | Oct 2026 |
| **v0.3** | Combat System — turn-based fights, enemies, loot | Planned | January 2026 |
| **v0.4** | Spells & Leveling — mana system, XP, character roster | Planned | March 2027 |
| **v0.5** | Shared World State — reputation, world flags, NPC memory | Planned | May 2027 |
| **v0.6** | NPCs & Dialogue — dialogue trees, class reactions, quests | Planned | July 2027 |
| **v0.7** | Quests & Economy — quest system, shops, gold loop | Planned | August 2027 |
| **v1.0** | Full CLI Release — complete first story arc, polished UI | Planned | Ocober 2027 |

---

## Feature Board

### Currently Working On

- [ ] **Spell System:** Mana costs, spell casting in combat, elemental damage
- [ ] **World & Location Travel:** Travel between locations, encounter rolls on the road
- [ ] **NPC Foundation:** Interaction structure ready for dialogue trees
- [x] **Weapons Refactor (`feature/weapons`):** generalized weapon categories + per-family variants (bow/staff/sword/axe/dagger/quiver)

### Active Branches

- `feature/combat`
- `feature/leveling`
- `feature/world-state`
- `feature/npc-dialogue`
- `feature/travel`
- `feature/quests`
- `feature/economy`
- `feature/spells`
- `feature/weapons`

### Characters & Classes

- [x] Character creation and naming
- [x] Warrior, Mage, and Archer base classes
- [x] Per-class default equipment
- [ ] Leveling and XP system
- [ ] Per-class stat bonuses on level-up
- [ ] Character roster with unlock conditions (e.g., reach level 10 → unlock slot 2)
- [ ] Ability trees per class

### Combat

- [ ] Enemy class with health, damage, element, and loot table
- [ ] Turn-based combat loop (player turn / enemy turn)
- [ ] Player actions in combat: attack, cast spell, use item, flee
- [ ] Elemental resistances and weakness multipliers
- [ ] Status effects: burn, freeze, poison, stun
- [ ] Win/lose outcomes with XP and loot rewards

### Weapons & Equipment

- [x] Weapon rarity and upgrade scaling
- [x] Melee and ranged weapon separation
- [x] Inventory slot capacity enforcement
- [ ] Proper `Inventory` class replacing ad-hoc item handling
- [ ] Elemental damage modifiers in combat
- [ ] Durability degradation during combat

### Spells & Magic

- [x] Base `Spell` class with `Cast()`
- [x] `Fireball` prototype
- [ ] Mana cost and cooldown per spell
- [ ] Mage spell book (list of learned spells)
- [ ] Mana regeneration per combat turn
- [ ] Full implementations: Fireball, IceShard, LightningBolt, HealingLight
- [ ] Status effects tied to spell elements

### World & Exploration

- [x] World and Location base architecture
- [ ] Travel system between locations
- [ ] Random encounter rolls during travel
- [ ] Level-gated region entry
- [ ] Starter region: the Ashlands (3–4 connected locations)
- [ ] Random world events during exploration

### Shared World State

- [ ] Global flag system (world events, quest completions)
- [ ] Per-region reputation system with tiers
- [ ] Per-NPC relationship tracking
- [ ] World event log (human-readable history)
- [ ] All character actions feed into shared world state

### NPCs & Dialogue

- [ ] Dialogue tree system (nodes with conditions and actions)
- [ ] NPC reactions based on reputation tier
- [ ] Class-specific dialogue options
- [ ] Dialogue actions: give quest, set world flag, modify reputation
- [ ] Quest giver NPCs

### Quests

- [ ] Quest class with objectives and rewards
- [ ] Objective types: Kill, Collect, Reach, Talk, Deliver
- [ ] Quest progress tracked in world state (shared across all characters)
- [ ] Class-exclusive quests
- [ ] Quest log in the game menu
- [ ] Starter quests in the Ashlands

### Economy

- [ ] Gold drops from enemies
- [ ] Shop class with buy/sell
- [ ] Reputation-based price discounts
- [ ] BlackSmith fully wired to trade system

### Save System

- [x] JSON polymorphic save/load
- [x] Overridable save directory via `RPGMANAGER_SAVE_DIR`
- [ ] Save `WorldState` alongside character data
- [ ] Save quest progress
- [ ] Auto-save after major events

---

### Refactors

- [X] Refactor of Weapons
- [ ] Refactor Elements/Type and how they work (There are currently double fields)
 

<!-- AUTO-GENERATED BELOW – DO NOT EDIT -->

# 🧮 Project Overview (auto-generated)

> Automatically generated from RPGManagerLib source files.

_Last updated: **2026-06-17 11:50**_

### 📊 Codebase Stats
- **Namespaces:** 17
- **Classes:** 48
- **Unique Methods:** 33
- **Pending TODOs:** 23


## 🧱 RPGManagerLib.Characters.Heroes

### [Archer.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Archer.cs)
*Inherits from: `Character`*  
> Returns the archer's equipped weapons as a comma-separated list with rarity.

_No unique public methods found._

### [Character.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Character.cs)
> Represents a character in the system with attributes such as name, health, creation date, and power level.

**Public Methods:**
- `Heal()`
- `Damage()`
- `TravelTo()`

**TODOs:**
- [ ] Introduce Level/Experience and shared combat-facing members here when Character implements the combat system contract.
- [ ] Replace this with region travel rules that consult WorldState, unlock flags, and travel requirements.
- [ ] Add functionality to travel to different worlds, which may require additional properties and methods related to world management.
- [ ] Research need for all constructors or only those to create a character

### [Mage.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Mage.cs)
*Inherits from: `Character`*  
> A magic-focused character with an inherent mana boost.

**Public Methods:**
- `CastSpell()`

**TODOs:**
- [ ] Implement spells and mana system (ESP)
- [ ] Route casting through the combat turn system so spell selection, mana spend, and status effects are resolved consistently.

### [Warrior.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Warrior.cs)
*Inherits from: `Character`*  
> Represents a warrior character with a collection of weapons.

_No unique public methods found._


## ⚔️ RPGManagerLib.Characters.NPCs

### [BlackSmith.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/NPCs/BlackSmith.cs)
*Inherits from: `NPC`*  
**Public Methods:**
- `Interact()`
- `Trade()`

### [NPC.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/NPCs/NPC.cs)
**Public Methods:**
- `Interact()`
- `Trade()`


## 📜 RPGManagerLib.Exceptions

### [CharacterException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/CharacterException.cs)
*Inherits from: `Exception`*  
> Base exception type for character-related validation issues.

_No unique public methods found._

### [InvalidWeaponException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/InvalidWeaponException.cs)
*Inherits from: `Exception`*  
> Thrown when an input or selection does not correspond to any known weapon type.

_No unique public methods found._

### [NegativeDamageException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/NegativeDamageException.cs)
*Inherits from: `CharacterException`*  
> Thrown when a negative value is supplied for damage.

_No unique public methods found._

### [NegativeHealException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/NegativeHealException.cs)
*Inherits from: `CharacterException`*  
> Thrown when a negative value is supplied for healing.

_No unique public methods found._

### [OverhealException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/OverhealException.cs)
*Inherits from: `CharacterException`*  
> Thrown when healing would exceed the maximum allowed health.

_No unique public methods found._

### [OverkillException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/OverkillException.cs)
*Inherits from: `CharacterException`*  
> Thrown when damage would drop health beyond the permitted lower bound.

_No unique public methods found._


## 🧙 RPGManagerLib.Items.Staffs

### [BasicStaff.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Staffs/BasicStaff.cs)
*Inherits from: `Staff`*  
> Represents a staff weapon that can be used in combat, providing basic damage and durability attributes.

_No unique public methods found._

### [Staff.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Staffs/Staff.cs)
*Inherits from: `Weapon`*  
> Represents a staff weapon that can be used in combat, providing basic damage and durability attributes.

_No unique public methods found._

### [WindStaff.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Staffs/WindStaff.cs)
*Inherits from: `Staff`*  
_No unique public methods found._


## 🏹 RPGManagerLib.Items.Weapons

### [Weapon.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Weapon.cs)
*Inherits from: `IEquipable`*  
> Base type for all weapons that can be equipped by a character.

**Public Methods:**
- `GetRarityMultiplier()`
- `GetEffectiveDamage()`
- `GetEffectiveDurability()`
- `UpgradeWeapon()`


## 🐉 RPGManagerLib.Items.Weapons.Bows

### [Bow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/Bow.cs)
*Inherits from: `Weapon`*  
> Represents an abstract base class for all bow weapons, providing common properties and behaviors for derived bow types.

_No unique public methods found._

### [HuntingBow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/HuntingBow.cs)
*Inherits from: `Bow`*  
_No unique public methods found._

### [SimpleBow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/SimpleBow.cs)
*Inherits from: `Bow`*  
> A basic bow with common stats suitable for early gameplay.

_No unique public methods found._

### [WarBow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/WarBow.cs)
*Inherits from: `Bow`*  
_No unique public methods found._


## 🏰 RPGManagerLib.Items.Weapons.Melee.Axes

### [Axe.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Axes/Axe.cs)
*Inherits from: `Weapon`*  
> Represents a melee weapon of type axe with predefined damage, durability, rarity, and inventory space attributes.

_No unique public methods found._

### [BasicAxe.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Axes/BasicAxe.cs)
*Inherits from: `Axe`*  
> Initializes a new instance of the Axe class with predefined attributes for a basic axe weapon.

_No unique public methods found._

### [BattleAxe.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Axes/BattleAxe.cs)
*Inherits from: `Axe`*  
_No unique public methods found._

### [GreatAxe.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Axes/GreatAxe.cs)
*Inherits from: `Axe`*  
_No unique public methods found._


## 🧭 RPGManagerLib.Items.Weapons.Melee.Daggers

### [BasicDagger.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Daggers/BasicDagger.cs)
*Inherits from: `Dagger`*  
> Initializes a new <see cref="Dagger"/> with default values.

_No unique public methods found._

### [Dagger.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Daggers/Dagger.cs)
*Inherits from: `Weapon`*  
> A fast melee weapon with low damage and short cooldown.

_No unique public methods found._


## 🪄 RPGManagerLib.Items.Weapons.Melee.Spears

### [BasicSpear.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Spears/BasicSpear.cs)
*Inherits from: `Weapon`*  
> A basic spear with balanced damage and durability, suitable for close combat.

_No unique public methods found._

### [Spear.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Spears/Spear.cs)
*Inherits from: `Weapon`*  
> A reach melee weapon with solid durability and moderate cooldown.

_No unique public methods found._


## 🧰 RPGManagerLib.Items.Weapons.Melee.Swords

### [BasicSword.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Swords/BasicSword.cs)
*Inherits from: `Sword`*  
> Initializes a new <see cref="Sword"/> with default values.

_No unique public methods found._

### [BroadSword.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Swords/BroadSword.cs)
*Inherits from: `Sword`*  
_No unique public methods found._

### [GreatSword.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Swords/GreatSword.cs)
*Inherits from: `Sword`*  
_No unique public methods found._

### [Sword.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Swords/Sword.cs)
*Inherits from: `Weapon`*  
> A versatile melee weapon with balanced damage and durability, suitable for close combat.

_No unique public methods found._


## 🎯 RPGManagerLib.Items.Weapons.Quivers

### [BigQuiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/BigQuiver.cs)
*Inherits from: `Quiver`*  
_No unique public methods found._

### [MediumQuiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/MediumQuiver.cs)
*Inherits from: `Quiver`*  
_No unique public methods found._

### [SmallQuiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/SmallQuiver.cs)
*Inherits from: `Quiver`*  
> Represents a small quiver designed to hold arrows, suitable for basic inventory needs.

_No unique public methods found._


## 🧱 RPGManagerLib.Locations

### [Location.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Locations/Location.cs)
> Adds a non-player character (NPC) to the current list of NPCs.

**Public Methods:**
- `AddNPC()`
- `RemoveNPC()`
- `GetNPCs()`


## ⚔️ RPGManagerLib.Saves

### [CharacterSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/CharacterSaveData.cs)
> Serializable snapshot of a character for saving and loading.

**Public Methods:**
- `ToCharacter()`

**TODOs:**
- [ ] Add Level and Experience here when the leveling system replaces the current PowerLevel-only progression.
- [ ] Extend this conversion when full inventory, spellbook, and other per-character systems need persistence.
- [ ] Restore Level/Experience and other new subsystems here as CharacterSaveData grows.

### [EquipableSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/EquipableSaveData.cs)
> Base class for all saveable equipable items. Uses polymorphic JSON serialization to handle different item types cleanly.

**Public Methods:**
- `ToEquipable()`

### [QuiverSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/QuiverSaveData.cs)
*Inherits from: `EquipableSaveData`*  
> Represents the save data for a quiver, including its capacity and other equipable properties.

**Public Methods:**
- `ToEquipable()`

### [SaveManager.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/SaveManager.cs)
> Provides static methods for saving and loading character data in JSON format.

**Public Methods:**
- `LoadCharacters()`
- `SaveCharacters()`

**TODOs:**
- [ ] Promote the root save payload from List<CharacterSaveData> to a SaveGame object when WorldState and roster data are added.
- [ ] Load schema version, WorldState, and roster metadata here once the save format expands beyond character snapshots.
- [ ] Serialize WorldState, roster unlocks, and future save-version metadata alongside characters.

### [WeaponSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/WeaponSaveData.cs)
*Inherits from: `EquipableSaveData`*  
> Represents the data required to serialize and reconstruct a weapon, including its type, damage, durability, level, and elemental affinity.

**Public Methods:**
- `ToEquipable()`


## 📜 RPGManagerLib.Spells

### [Fireball.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Spells/Fireball.cs)
*Inherits from: `Spell`*  
_No unique public methods found._

### [Spell.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Spells/Spell.cs)
**Public Methods:**
- `Cast()`

**TODOs:**
- [ ] Move spell metadata toward unlockable/progression-aware content once leveling and spell acquisition are added.
- [ ] Integrate combat logging, target validation, and status-effect resolution with CombatManager instead of direct console-only flow.


## 🧙 RPGManagerLib.UI

### [CharacterFactory.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/CharacterFactory.cs)
> Provides functionality to create character instances based on user input.

**Public Methods:**
- `CreateCharacter()`
- `CreateDefaultWeaponsWarrior()`
- `CreateDefaultWeaponsArcher()`
- `CreateDefaultWeaponsMage()`

### [GameMenu.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/GameMenu.cs)
> High-level console game loop and character management menu.

**Public Methods:**
- `Start()`

**TODOs:**
- [ ] Replace the raw character list with a CharacterRoster once slot unlocks and active-hero tracking are implemented.
- [ ] Bootstrap the starting region from saved WorldState/region data instead of creating a one-off world here.
- [ ] Prevent creation when the roster is full and surface the next slot unlock requirement in the menu.
- [ ] Persist the active character in save data so the last played hero is restored on launch.
- [ ] Route Explore into the travel/location loop once regions, encounters, and quest hooks exist.
- [ ] Replace this stub with CombatManager once combat, loot, and post-fight rewards are implemented.
- [ ] Present region/location choices here and resolve travel, random encounters, dialogue, and world-flag updates.
- [ ] Start CombatManager here and replace this placeholder once enemies, actions, and rewards exist.

### [MenuSystem.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/MenuSystem.cs)
> Minimal console menu helper that maps string keys to labeled actions with optional hints.

**Public Methods:**
- `AddOption()`
- `Show()`


## 🏹 RPGManagerLib.Weapons.Quivers

### [Quiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/Quiver.cs)
*Inherits from: `IEquipable`*  
> Represents an abstract base class for quivers, which are used to store and manage arrows in an inventory system.

_No unique public methods found._


## 🐉 RPGManagerLib.Worlds

### [World.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Worlds/World.cs)
> Unlocks the current instance, allowing access to its features.

**Public Methods:**
- `AddLocation()`
- `RemoveLocation()`
- `Unlock()`
- `Lock()`

**TODOs:**
- [ ] Evolve this into region/world data that can participate in travel routes, encounter tables, and reputation/world-flag checks.

