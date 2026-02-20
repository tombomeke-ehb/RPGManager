# 🗺️ RPG Manager – Development Roadmap

Welcome to the official development roadmap for **RPG Manager**,  
a modular C# RPG engine by *Tombomeke Studios* ⚔️  

This section is **manually written** and reflects your development goals, vision,  
and progress — while the section below is automatically generated  
based on your actual C# codebase.

---

## 🎯 Vision
Build a fully modular, text-based RPG framework with:
- Expandable **World** and **Location** system 🌍  
- Deep **Character** and **Inventory** interactions 🧙‍♂️  
- Persistent **Save System** 💾  
- Scalable for future CLI and GUI support ⚙️  

---

## 🚀 Milestones

| Milestone | Description | Status | Target |
|------------|--------------|--------|---------|
| **v0.1 – Core Engine** | Character classes, weapons, save system | ✅ Done | 2025-10 |
| **v0.2 – World System** | Worlds, locations, and travel mechanics | ⚙️ In Progress | 2025-11 |
| **v0.3 – Combat System** | Turn-based battles, HP & weapon effects | 🧱 Planned | 2025-12 |
| **v0.4 – Economy System** | Currency, shops, crafting | 🧱 Planned | Q1 2026 |
| **v1.0 – CLI Launch** | Full release with save/load UI | 🪄 Planned | Mid 2026 |

---

## 🧩 Feature Board

### 🧙 Characters
- ✅ Character creation & naming  
- ✅ Warrior & Mage base classes  
- 🧱 Add leveling and XP system  
- 🧱 Add ability trees per class  

### ⚔️ Weapons
- ✅ Weapon rarity & upgrades  
- ✅ Melee & ranged system  
- 🧱 Add elemental modifiers  
- 🧱 Add durability-based breaking  

### 🌍 Worlds
- ✅ World & Location base system  
- 🧱 Add travel system between locations  
- 🧱 Add difficulty scaling  
- 🧱 Add random world events  

### 💾 Saves
- ✅ JSON save/load  
- 🧱 Auto-save after each event  
- 🧱 Add cloud save compatibility  

---

## 📅 Upcoming Priorities
1. Finalize **World travel system**
2. Add **location-based events**
3. Rework **CharacterFactory** to integrate world selection
4. Create **combat prototype**
5. Refactor **WeaponType** enum to support more classes

---


<!-- AUTO-GENERATED BELOW – DO NOT EDIT -->

# 🧮 Project Overview (auto-generated)

> Automatically generated from RPGManagerLib source files.

_Last updated: **2026-02-20 13:01**_

### 📊 Codebase Stats
- **Namespaces:** 14
- **Classes:** 34
- **Unique Methods:** 33
- **Pending TODOs:** 5


## 🧱 RPGManagerLib.Characters.Heroes

### [Archer.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Archer.cs)
*Inherits from: `Character`*  
_No unique public methods found._

### [Character.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Character.cs)
> Represents a character in the system with attributes such as name, health, creation date, and power level.

**Public Methods:**
- `Heal()`
- `Damage()`
- `TravelTo()`

**TODOs:**
- [ ] Add functionality to travel to different worlds, which may require additional properties and methods related to world management.
- [ ] Research need for all constructors or only those to create a character

### [Mage.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Mage.cs)
*Inherits from: `Character`*  
> A magic-focused character with an inherent mana boost.

**Public Methods:**
- `CastSpell()`

**TODOs:**
- [ ] Implement spells and mana system (ESP)

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

### [Staff.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Staffs/Staff.cs)
*Inherits from: `Weapon`*  
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
> Base type for bow-style ranged weapons.

_No unique public methods found._

### [SimpleBow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/SimpleBow.cs)
*Inherits from: `Bow`*  
> A basic bow with common stats suitable for early gameplay.

_No unique public methods found._


## 🏰 RPGManagerLib.Items.Weapons.Melee

### [Axe.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Axe.cs)
*Inherits from: `Weapon`*  
> A heavy melee weapon with strong base damage and slower cooldown.

_No unique public methods found._

### [Dagger.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Dagger.cs)
*Inherits from: `Weapon`*  
> A fast melee weapon with low damage and short cooldown.

_No unique public methods found._

### [Spear.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Spear.cs)
*Inherits from: `Weapon`*  
> A reach melee weapon with solid durability and moderate cooldown.

_No unique public methods found._

### [Sword.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Sword.cs)
*Inherits from: `Weapon`*  
> A balanced melee weapon with moderate damage and cooldown.

_No unique public methods found._


## 🧭 RPGManagerLib.Items.Weapons.Quivers

### [SmallQuiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/SmallQuiver.cs)
*Inherits from: `Quiver`*  
> A small quiver with limited capacity and minimal inventory footprint.

_No unique public methods found._


## 🪄 RPGManagerLib.Locations

### [Location.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Locations/Location.cs)
> Adds a non-player character (NPC) to the current list of NPCs.

**Public Methods:**
- `AddNPC()`
- `RemoveNPC()`
- `GetNPCs()`


## 🧰 RPGManagerLib.Saves

### [CharacterSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/CharacterSaveData.cs)
> Serializable snapshot of a character for saving and loading.

**Public Methods:**
- `ToCharacter()`

### [EquipableSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/EquipableSaveData.cs)
> Base class for all saveable equipable items. Uses polymorphic JSON serialization to handle different item types cleanly.

**Public Methods:**
- `ToEquipable()`

### [QuiverSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/QuiverSaveData.cs)
*Inherits from: `EquipableSaveData`*  
> Represents the data required to save and restore a quiver.

**Public Methods:**
- `ToEquipable()`

### [SaveManager.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/SaveManager.cs)
> Loads a list of characters from a saved file.

**Public Methods:**
- `LoadCharacters()`
- `SaveCharacters()`

### [WeaponSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/WeaponSaveData.cs)
*Inherits from: `EquipableSaveData`*  
> Represents the data required to save and restore the state of a weapon.

**Public Methods:**
- `ToEquipable()`


## 🎯 RPGManagerLib.Spells

### [Fireball.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Spells/Fireball.cs)
*Inherits from: `Spell`*  
_No unique public methods found._

### [Spell.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Spells/Spell.cs)
**Public Methods:**
- `Cast()`


## 🧱 RPGManagerLib.UI

### [CharacterFactory.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/CharacterFactory.cs)
> Provides functionality to create character instances based on user input.

**Public Methods:**
- `CreateCharacter()`
- `CreateDefaultWeaponsWarrior()`
- `CreateDefaultWeaponsArcher()`
- `CreateDefaultWeaponsMage()`

**TODOs:**
- [ ] Research for a more efficient way
- [ ] Change this to an inventory management system, where you can add and remove items from your inventory, and the inventory will have a maximum capacity.

### [GameMenu.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/GameMenu.cs)
> High-level console game loop and character management menu.

**Public Methods:**
- `Start()`

### [MenuSystem.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/MenuSystem.cs)
> Minimal console menu helper that maps string keys to actions.

**Public Methods:**
- `AddOption()`
- `Show()`


## ⚔️ RPGManagerLib.Weapons.Quivers

### [Quiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/Quiver.cs)
*Inherits from: `IEquipable`*  
> Base type for quivers that store ammunition for bows.

_No unique public methods found._


## 📜 RPGManagerLib.Worlds

### [World.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Worlds/World.cs)
> Unlocks the current instance, allowing access to its features.

**Public Methods:**
- `AddLocation()`
- `RemoveLocation()`
- `Unlock()`
- `Lock()`

