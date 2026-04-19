# Changelog

All notable changes to RPG Manager are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/). Versioning follows [Semantic Versioning](https://semver.org/).

---

## [Unreleased]

### Added
- CLAUDE.md — full system design plan and world lore for future Claude Code sessions
- ROADMAP.md rewrite with realistic milestones through v1.0 (Q4 2027)
- 8 feature branches created for upcoming systems: combat, leveling, world-state, npc-dialogue, travel, quests, economy, spells
- CI workflow — build check on every push and pull request
- CONTRIBUTING.md
- docs/lore/ — world lore documents for Tavaryn and the Ashlands

### Changed
- README.md — professional rewrite reflecting the shared world mechanic and Keeper of Destiny concept
- PRESS.md — rewritten to match the current vision

---

## [v0.1.2] — 2026-03-22

### Added
- XML documentation across all public APIs (weapons, save data, characters, UI)
- Spell system foundation: abstract `Spell` base class with `Cast()`, `Fireball` implementation
- `Mage` character now supports `CastSpell()` and starts with class-default staff

### Changed
- `Bow` updated to parameterless constructor
- `Staff` mapped to `MagicType` by element name
- `CharacterFactory` updated with Mage weapon initialization
- ROADMAP.md revised for clarity

---

## [v0.1.1] — 2025-12-15

### Added
- `Mage` character class with mana boost and spell casting stub
- `Archer` character class
- `Staff` equipable item class under `Items/Staffs/`
- `MagicType` enum for elemental magic categories
- `EquipableType` updated to include STAFF
- Polymorphic JSON save/load for all equipable types (weapons, quivers, staffs)
- Robust error handling in `SaveManager.LoadCharacters()`

### Changed
- Refactored character and weapon save/load pipeline
- Simplified hero constructors with sensible defaults
- Removed `CooldownTime` from weapon model (moved to future spell/combat system)
- NPC and Location methods renamed to PascalCase

---

## [v0.1.0] — 2025-10-01

### Added
- Initial release
- Character system: abstract `Character` base with health, mana, gold, power level
- `Warrior` character class with melee weapon selection
- Melee weapons: `Sword`, `Dagger`, `Axe`, `Spear`
- Ranged weapons: `Bow`, `SimpleBow`, `Quiver`, `SmallQuiver`
- `IEquipable` interface and `Weapon` abstract base with rarity, element, and upgrade logic
- Inventory slot system (small items = 1 slot, large items = 2 slots, max 4 slots)
- `Rarity` enum: COMMON → LEGENDARY, with damage/durability multipliers
- `Element` enum for elemental weapon affinity
- JSON save system via `System.Text.Json` with polymorphic deserialization
- `RPGMANAGER_SAVE_DIR` environment variable for custom save location
- NPC foundation: `NPC` base class and `BlackSmith` with `Interact()` and `Trade()` stubs
- World and location system: `World` and `Location` classes with NPC management
- Console game loop: character creation, switch, view stats, explore (stub), fight (stub)
- Interactive `CharacterFactory` with equipment selection
- Splash screen with game intro and Tombomeke Studios branding
- GitHub Actions release workflow: builds and uploads self-contained binaries for 6 platforms
- RoadmapUpdater dev tool: auto-generates API overview section of ROADMAP.md
- Proprietary source-available license (view and play; copying and redistribution not permitted)
