# RPG Manager

**A text-based RPG engine built in C# — where your heroes share a world.**

RPG Manager is a console RPG built on .NET 8. You play as the **Keeper of Destiny**, an entity that bonds with mortal heroes and guides them through the realm of Tavaryn. You don't control one character — you manage a roster. Each hero you create has their own class, level, and story. But they all exist in the same living world: what one hero does changes how NPCs treat the others, what paths open up, and how the world evolves.

---

## What Makes It Different

Most RPGs follow one character. RPG Manager tracks a **shared world state** across all of them:

- Your Warrior defeats the bandits terrorizing the Ashlands → merchants in the region now offer your Mage a discount
- Your Mage breaks a magical seal → a locked door opens that your Archer can now pass through
- Your Archer completes a smuggling quest for a shady NPC → that NPC becomes suspicious of your Warrior too

New character classes unlock as you progress. Each class is useful in different situations — no single hero can handle everything. Building the right roster matters.

---

## Current State

This project is actively in development. The core engine is complete and the game is playable in its current form.

**What works today:**
- Create and name a hero — choose Warrior, Mage, or Archer
- Per-class equipment selection and inventory management (4 slots, size-based)
- View your hero's stats, switch between heroes, and manage your roster
- JSON-based save system — save data persists across sessions
- Extensible architecture with clean seams for adding combat, quests, travel, and more

**What's coming:**
- Turn-based combat with enemies, loot, and elemental damage
- XP and leveling with per-class progression and character roster unlocks
- A full world with regions, travel, and random encounters
- NPC dialogue trees that react to your reputation and which class is talking
- A quest system with class-exclusive missions and shared world consequences
- Shops, gold, and an economy tied to your standing in each region

See [ROADMAP.md](ROADMAP.md) for the full feature board and milestone targets.

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

By default, saves are stored in a `Save/` folder in the working directory. To override:

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
2. Warriors let you choose your starting equipment. Mages and Archers start with class defaults.
3. Inventory has 4 slots — small items (Dagger, Sword, Bow, Quiver) use 1 slot; large items (Axe, Spear) use 2.
4. From the main menu: view your hero, switch characters, explore, fight, or quit.
5. Exploration and combat currently demonstrate the intended flow — they expand with each release.

---

## Building & Publishing

Single-file, self-contained binaries for distribution:

```bash
# Windows
dotnet publish RPGManager -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true

# macOS
dotnet publish RPGManager -c Release -r osx-arm64 -p:PublishSingleFile=true --self-contained true

# Linux
dotnet publish RPGManager -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained true
```

Supported targets: `win-x64`, `win-arm64`, `osx-x64`, `osx-arm64`, `linux-x64`, `linux-arm64`

Output: `RPGManager/bin/Release/<target>/publish/`

Pre-built binaries for all platforms are available on the [Releases](https://github.com/tombomeke-ehb/RPGManager/releases) page.

---

## Project Structure

```
RPGManager/             Console entry point (splash screen, calls GameMenu.Start)
RPGManagerLib/          Core library — all game logic, no external dependencies
Tools/RoadmapUpdater/   Dev tool — scans the library and auto-updates ROADMAP.md
```

Key entry points:
- `RPGManager/Program.cs` — splash screen and launch
- `RPGManagerLib/UI/GameMenu.cs` — main game loop
- `RPGManagerLib/UI/CharacterFactory.cs` — character creation
- `RPGManagerLib/Saves/SaveManager.cs` — save and load

---

## Troubleshooting

**Odd characters in the console banner:** ensure your terminal uses UTF-8 encoding.

**Save file issues:** verify `RPGMANAGER_SAVE_DIR` points to a writable folder, or remove the variable to use the default `Save/` directory.

**Build errors:** run `dotnet --info` to confirm .NET 8 SDK is installed.

---

## License

Code is licensed under the [MIT License](LICENSE). Game assets (art, audio, narrative content) will be licensed separately when added.

---

*Developed by Tombomeke Studios — [tombomeke.com](https://tombomeke.com)*
