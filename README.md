# RPG Manager

[![CI](https://github.com/tombomeke-ehb/RPGManager/actions/workflows/ci.yml/badge.svg)](https://github.com/tombomeke-ehb/RPGManager/actions/workflows/ci.yml)
[![Release](https://github.com/tombomeke-ehb/RPGManager/actions/workflows/release.yml/badge.svg)](https://github.com/tombomeke-ehb/RPGManager/actions/workflows/release.yml)
[![GitHub release](https://img.shields.io/github/v/release/tombomeke-ehb/RPGManager)](https://github.com/tombomeke-ehb/RPGManager/releases/latest)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/download)
[![License: Proprietary](https://img.shields.io/badge/license-Proprietary-red)](LICENSE)

**A console RPG where your heroes share a living world — what one does changes everything for the others.**

RPG Manager is a text-based RPG built on .NET 8. You play as the **Keeper of Destiny**, an entity that bonds with mortal heroes and guides them through the realm of **Tavaryn**. You don't control one character — you manage a roster. Each hero you create has their own class, level, and story. But they all exist in the same living world: what one hero does changes how NPCs treat the others, what paths open up, and how the world evolves.

---

## What Makes It Different

Most RPGs follow one character. RPG Manager tracks a **shared world state** across all of them:

- Your Warrior defeats the bandits terrorizing the Ashlands → merchants in the region offer your Mage a discount
- Your Mage breaks a magical seal → a locked door opens that your Archer can now pass through
- Your Archer completes a smuggling quest → that NPC becomes suspicious of your Warrior too

New character classes unlock as you progress. Each class opens different paths — no single hero can handle everything. Building the right roster matters.

---

## Demo

```
====================================================================
                        R P G   M A N A G E R
====================================================================
Version: v0.1.2
                     A Tombomeke Studios Production

The world stands at the edge of chaos.
Legends whisper of heroes long forgotten,
and dark powers rising beyond the misty mountains.

From the ashes of old kingdoms, new champions emerge.
Their fate lies in your hands — as the Keeper of Destiny,
you shall forge their path through war, magic, and time itself.

Raise your banners, summon your courage...
and let the tale begin.

====================================================================

                Developed by Tombomeke Studios © 2025
                     www.tombomeke.com

Press any key to enter the realm of Tavaryn...
```

```
--- Create Your Hero ---
Enter your hero's name: Aldric

Choose a class:
  [1] Warrior  — frontline fighter, high health, melee weapons
  [2] Mage     — arcane spellcaster, high mana, staff and spells
  [3] Archer   — ranged attacker, balanced stats, bow and quiver
> 1

Choose your starting equipment (4 inventory slots):
  [1] Sword     (1 slot)  — balanced melee weapon
  [2] Axe       (2 slots) — heavy melee, high damage
  [3] Dagger    (1 slot)  — fast, low damage
  [4] Spear     (2 slots) — reach weapon, solid durability
> 1 3

Hero Aldric created.

--- Aldric the Warrior ---
  Level    : 1          Health : 120 / 120
  Power    : 10         Mana   : 30 / 30
  Gold     : 50
  Equipment: Sword (Common), Dagger (Common)
```

---

## Current State

The core engine is complete and the game is playable today.

**What works:**
- Create and name a hero — choose Warrior, Mage, or Archer
- Per-class equipment selection with inventory slot management (4 slots, size-based)
- View hero stats, switch between characters, and manage your roster
- JSON-based save system — progress persists across sessions
- Extensible architecture with clean seams for combat, quests, travel, and more

**What's in development:**
- Turn-based combat with enemies, loot, and elemental damage
- XP and leveling with per-class progression and roster unlocks
- A full world with regions, travel, and random encounters
- NPC dialogue trees that react to your reputation and active class
- Quest system with class-exclusive missions and shared world consequences
- Shops, gold, and a regional economy

See [ROADMAP.md](ROADMAP.md) for the full feature board and milestone targets through v1.0.

---

## Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) or later

### Run the game

```bash
git clone https://github.com/tombomeke-ehb/RPGManager.git
cd RPGManager
dotnet run --project RPGManager
```

### Optional: custom save directory

Saves default to a `Save/` folder in the working directory. To override:

```powershell
# PowerShell
$env:RPGMANAGER_SAVE_DIR = "C:\path\to\saves"
dotnet run --project RPGManager
```

```bash
# Bash
export RPGMANAGER_SAVE_DIR=/path/to/saves
dotnet run --project RPGManager
```

---

## How to Play

1. On first launch, create your first hero — pick a name and a class.
2. Warriors let you choose starting equipment; Mages and Archers start with class defaults.
3. Inventory has 4 slots — small items use 1 slot, large items use 2.
4. From the main menu: view your hero, switch characters, explore, fight, or quit.
5. Exploration and combat demonstrate the intended flow and expand with each release.

---

## Building & Publishing

Self-contained single-file binaries for all platforms:

```bash
dotnet publish RPGManager -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true
```

Supported targets: `win-x64` `win-arm64` `osx-x64` `osx-arm64` `linux-x64` `linux-arm64`

Output: `RPGManager/bin/Release/<target>/publish/`

Pre-built binaries are available on the [Releases](https://github.com/tombomeke-ehb/RPGManager/releases) page.

---

## Project Structure

```
RPGManager/             Console entry point (splash screen, calls GameMenu.Start)
RPGManagerLib/          Core library — all game logic, no external dependencies
Tools/RoadmapUpdater/   Dev tool — scans RPGManagerLib and auto-updates ROADMAP.md
docs/lore/              World lore — regions, NPCs, factions, quest design
```

Key files:
- `RPGManager/Program.cs` — splash screen and launch
- `RPGManagerLib/UI/GameMenu.cs` — main game loop
- `RPGManagerLib/UI/CharacterFactory.cs` — character creation
- `RPGManagerLib/Saves/SaveManager.cs` — save and load

---

## Documentation

| Document                     | Description                                    |
| ---------------------------- | ---------------------------------------------- |
| [ROADMAP.md](ROADMAP.md)     | Feature board and milestone targets            |
| [CHANGELOG.md](CHANGELOG.md) | Version history                                |
| [CLAUDE.md](CLAUDE.md)       | System design plans and architectural guidance |
| [docs/lore](docs/lore)       | World lore — Tavaryn, regions, NPCs, factions  |
| [PRESS.md](PRESS.md)         | Press kit                                      |

---

## Contributing

This is a personal learning project — I write all the code myself. Bug reports and feature suggestions are welcome via [GitHub Issues](https://github.com/tombomeke-ehb/RPGManager/issues). See [CONTRIBUTING.md](CONTRIBUTING.md) for details.

---

## Troubleshooting

**Odd characters in the console banner:** ensure your terminal uses UTF-8 encoding.

**Save file issues:** verify `RPGMANAGER_SAVE_DIR` points to a writable folder, or remove the variable to use the default `Save/` directory.

**Build errors:** run `dotnet --info` to confirm .NET 8 SDK is installed.

---

Built with `.NET 8` · `C#` · `System.Text.Json`

*Developed by [Tombomeke Studios](https://tombomeke.com) · [GitHub](https://github.com/tombomeke-ehb)*

Source code is available to read and learn from. Copying, modifying, or redistributing this project — in whole or in part — is not permitted. See [LICENSE](LICENSE) for full terms.
