# RPG Manager

Status: Work in Progress (actively developed)

RPG Manager is a .NET 8 console RPG framework and playable demo. It opens with a cinematic splash screen and drops you into a simple, text‑driven loop where you create a hero, equip items, and choose to explore or enter combat. The goal is to grow this into a small, story‑lite adventure while keeping the code clean and extensible for future systems (inventory, encounters, skills, locations, etc.).

Project page (soon): https://tombomeke.com

## What’s This?

Short version: a foundation for a small, single‑player RPG you can run in the terminal. It’s also a clean learning project for object‑oriented C# patterns, with clear seams to add features safely as the game grows.

## Current Gameplay Snapshot

- Start the game and create your first hero.
- Pick a class (Warrior or Mage). Warriors can select equipment; Mages start with class defaults.
- Navigate a main menu to view your hero, switch heroes, explore, fight, or quit.
- Exploration and combat currently demonstrate flow via messages; they are designed to be expanded.

## Features (Current)

- Interactive game loop – menu to create/switch heroes, explore, and fight.
- Character system – Warrior and Mage with shared behavior (heal/damage) and per‑class flavor.
- Items and equipment – melee weapons, bows, and quivers via a simple equipable interface.
- Inventory capacity – 4 slots; small items use 1, large use 2; validation prevents overflow.
- Save management – JSON saves; override directory via `RPGMANAGER_SAVE_DIR`.
- Extensible design – add heroes, weapons, locations, and menu actions without churn.

## Project Structure

```
RPGManager.sln          Solution
RPGManager/             Console front end and entry point
RPGManagerLib/          Library: characters, items, UI, and save logic
```

Key entry points:
- `RPGManager/Program.cs` – splash screen, calls `GameMenu.Start()`.
- `RPGManagerLib/UI/GameMenu.cs` – main loop and menu actions.
- `RPGManagerLib/UI/CharacterFactory.cs` – interactive character creation and equipment selection.
- `RPGManagerLib/Saves/SaveManager.cs` – JSON serialization, save/load.

## Prerequisites

- .NET SDK 8.0 or later

## Run The Game

Build and run from the repo root:

```bash
dotnet build
dotnet run --project RPGManager
```

Set a custom save directory (optional):
- PowerShell (Windows):
  ```powershell
  $env:RPGMANAGER_SAVE_DIR = "C:\\path\\to\\saves"
  dotnet run --project RPGManager
  ```
- Bash (Linux/macOS):
  ```bash
  export RPGMANAGER_SAVE_DIR=/path/to/saves
  dotnet run --project RPGManager
  ```

## How To Play

- First launch loads or creates your hero. If none exist, you’ll create one.
- Creating a Warrior allows selecting equipment; a Mage starts with class defaults.
- Inventory capacity is 4 slots total:
  - Small items (e.g., Dagger, Sword, Bow, Quiver) use 1 slot.
  - Large items (e.g., Axe, Spear) use 2 slots.
- Main menu options include: create new character, switch active character, view current character, explore, fight, or quit.
- Explore and Fight currently demonstrate flow with simple messages; extend them for encounters and combat.

## Configuration and Saves

- Save directory: defaults to `Save` in the working directory, or override with `RPGMANAGER_SAVE_DIR`.
- Save file: `characters.json` inside the save directory.
- Moving saves: copy or point `RPGMANAGER_SAVE_DIR` to an existing folder to continue a campaign.

## Build and Publish

Create self‑contained, single‑file binaries for distribution:

- Windows x64:
  ```powershell
  dotnet publish RPGManager -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true
  ```
- macOS x64:
  ```bash
  dotnet publish RPGManager -c Release -r osx-x64 -p:PublishSingleFile=true --self-contained true
  ```
- Linux x64:
  ```bash
  dotnet publish RPGManager -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained true
  ```

Artifacts will be in `RPGManager/bin/Release/<target>/publish/`.

## Extending The Game

- Add a new hero class:
  - Create a class under `RPGManagerLib/Characters/Heroes/` inheriting from `Character`.
  - Expose a `CharacterType` and any custom behaviors.
  - Update `CharacterFactory` to construct it from user input.

- Add a weapon or item:
  - Implement `IEquipable` under `RPGManagerLib/Items/` (or extend `Weapon` under `Items/Weapons`).
  - Decide inventory size via `InventorySpaceAmount`.
  - Add to the selection in `CharacterFactory.EquipableSelectionMenu()`.

- Add a menu action:
  - Add a new case in `RPGManagerLib/UI/GameMenu.cs` within the main loop.
  - Keep output text‑only for simplicity; consider extracting logic into a new class.

## Roadmap

Near‑term
- Encounters: basic exploration events with outcomes and rewards.
- Combat: turn order, abilities, status effects, loot.
- Progression: XP/leveling, skills, equipment rarities.
- Saves: multiple save slots and last‑played tracking.

Later
- World: locations, simple NPC interactions, and quests.
- UX: settings menu, accessibility tweaks.
- Content: item tiers, unique abilities, lightweight story beats.

## Contributing

This project is actively evolving. Suggestions and lightweight PRs are welcome, but the design may change rapidly. If proposing bigger changes, please open an issue or discussion first to align on direction.

## Troubleshooting

- Console glyphs: if you see odd characters in the console banner, ensure your terminal encoding supports UTF‑8.
- Save issues: verify the `RPGMANAGER_SAVE_DIR` exists and is writable, or remove it to use the default `Save` folder.
- Build errors: run `dotnet --info` to ensure .NET 8 SDK is installed and selected.

## License

Code is licensed under the MIT License (see `LICENSE`). Game assets (art, audio, narrative content) may be licensed separately when added.

