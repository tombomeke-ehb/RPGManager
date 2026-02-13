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

_Last updated: **2026-02-11 11:33**_

🧩 **12 Namespaces · 31 Classes · 34 Methods · 6 TODOs**


## 🧱 RPGManagerLib.Characters.Heroes

### [Archer.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Archer.cs)
**Public Methods:**
- `ToString()`

### [Character.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Character.cs)
**Public Methods:**
- `Heal()`
- `Damage()`
- `TravelTo()`
- `ToString()`

**TODOs:**
- [ ] Add functionality to travel to different worlds, which may require additional properties and methods related to world management.
- [ ] Research need for all constructors or only those to create a character

### [Mage.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Mage.cs)
**Public Methods:**
- `ToString()`

**TODOs:**
- [ ] Implement spells and mana system (ESP)

### [Warrior.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/Heroes/Warrior.cs)
**Public Methods:**
- `ToString()`


## ⚔️ RPGManagerLib.Characters.NPCs

### [BlackSmith.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/NPCs/BlackSmith.cs)
**Public Methods:**
- `Interact()`
- `Trade()`

### [NPC.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Characters/NPCs/NPC.cs)
**Public Methods:**
- `Interact()`
- `Trade()`


## 📜 RPGManagerLib.Exceptions

### [CharacterException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/CharacterException.cs)
_No public methods found._

### [InvalidWeaponException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/InvalidWeaponException.cs)
_No public methods found._

### [NegativeDamageException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/NegativeDamageException.cs)
_No public methods found._

### [NegativeHealException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/NegativeHealException.cs)
_No public methods found._

### [OverhealException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/OverhealException.cs)
_No public methods found._

### [OverkillException.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Exceptions/OverkillException.cs)
_No public methods found._


## 🧙 RPGManagerLib.Items.Weapons

### [Weapon.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Weapon.cs)
**Public Methods:**
- `GetRarityMultiplier()`
- `GetEffectiveDamage()`
- `GetEffectiveDurability()`
- `UpgradeWeapon()`


## 🏹 RPGManagerLib.Items.Weapons.Bows

### [Bow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/Bow.cs)
_No public methods found._

### [SimpleBow.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Bows/SimpleBow.cs)
_No public methods found._


## 🐉 RPGManagerLib.Items.Weapons.Melee

### [Axe.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Axe.cs)
_No public methods found._

### [Dagger.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Dagger.cs)
_No public methods found._

### [Spear.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Spear.cs)
_No public methods found._

### [Sword.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Melee/Sword.cs)
_No public methods found._


## 🏰 RPGManagerLib.Items.Weapons.Quivers

### [SmallQuiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/SmallQuiver.cs)
_No public methods found._


## 🧭 RPGManagerLib.Locations

### [Location.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Locations/Location.cs)
**Public Methods:**
- `AddNPC()`
- `RemoveNPC()`
- `GetNPCs()`


## 🪄 RPGManagerLib.Saves

### [CharacterSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/CharacterSaveData.cs)
**Public Methods:**
- `ToCharacter()`

### [EquipableSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/EquipableSaveData.cs)
**Public Methods:**
- `ToEquipable()`

### [QuiverSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/QuiverSaveData.cs)
**Public Methods:**
- `ToEquipable()`

### [SaveManager.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/SaveManager.cs)
**Public Methods:**
- `LoadCharacters()`
- `SaveCharacters()`

### [WeaponSaveData.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Saves/WeaponSaveData.cs)
**Public Methods:**
- `ToEquipable()`


## 🧰 RPGManagerLib.UI

### [CharacterFactory.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/CharacterFactory.cs)
**Public Methods:**
- `CreateCharacter()`
- `CreateDefaultWeaponsWarrior()`
- `CreateDefaultWeaponsArcher()`

**TODOs:**
- [ ] Research for a more efficient way
- [ ] Implement Mage Creation
- [ ] Implement inventory System

### [GameMenu.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/GameMenu.cs)
**Public Methods:**
- `Start()`

### [MenuSystem.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/UI/MenuSystem.cs)
**Public Methods:**
- `AddOption()`
- `Show()`


## 🎯 RPGManagerLib.Weapons.Quivers

### [Quiver.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Items/Weapons/Quivers/Quiver.cs)
_No public methods found._


## 🧱 RPGManagerLib.Worlds

### [World.cs](https://github.com/tombomeke-ehb/RPGManager/main/RPGManagerLib/Worlds/World.cs)
**Public Methods:**
- `AddLocation()`
- `RemoveLocation()`
- `Unlock()`
- `Lock()`

