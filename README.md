# RPG Manager

RPG Manager is a .NET 8 console application and class library that demonstrates a lightweight role-playing game management experience. The app boots into a cinematic splash screen and hands off to an interactive menu that lets you create, view, and manage heroes, experiment with items, and explore or battle through simple text-driven events. It serves as a foundation for experimenting with RPG mechanics or as a teaching aid for object-oriented patterns in C#.

## Features

- **Interactive game loop** – Launches a main menu with options to create heroes, switch between them, explore, or enter combat sequences.
- **Character system** – Includes hero archetypes like warriors and mages, each inheriting shared behaviors such as healing and taking damage while introducing their own stats.
- **NPC and location scaffolding** – Provides example non-player characters and location models that can be extended for richer encounters.
- **Item and weapon framework** – Supplies melee weapons, bows, and quivers with equipable interfaces and validation logic, ready for expansion.
- **Save management** – Persists hero rosters to JSON so your party survives across sessions. Supports overriding the save directory via the `RPGMANAGER_SAVE_DIR` environment variable.

## Project structure

```
RPGManager.sln          Solution file
RPGManager/             Console front end and entry point
RPGManagerLib/          Reusable library with characters, items, UI, and save logic
```

## Prerequisites

- .NET SDK 8.0 or later

## Getting started

1. Restore and build the solution:
   ```bash
   dotnet build
   ```
2. Run the console front end:
   ```bash
   dotnet run --project RPGManager
   ```
3. Optionally set a custom save directory before running:
   ```bash
   export RPGMANAGER_SAVE_DIR=/path/to/saves
   ```

When you are ready to extend the experience, start by adding new menu options in `RPGManagerLib/UI/GameMenu.cs` or creating additional hero classes under `RPGManagerLib/Characters/Heroes/`.

## License

This project is provided as-is for learning and experimentation. Adapt or extend it to suit your campaign.
