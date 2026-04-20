# Repository Guidelines

## Project Structure & Module Organization
`RPGManager/` contains the console entry point and startup flow. `RPGManagerLib/` holds the core game logic, organized by domain areas such as `Characters/`, `Items/`, `Saves/`, `Spells/`, `UI/`, and `Worlds/`. `Tools/RoadmapUpdater/` is a small developer utility that scans the library and updates `ROADMAP.md`. Worldbuilding notes live under `docs/lore/`.

Keep new features aligned with the existing boundaries: gameplay systems belong in `RPGManagerLib`, while `RPGManager` should stay thin and focused on bootstrapping and console flow.

## Build, Test, and Development Commands
Use `.NET 8`.

`dotnet restore` restores solution dependencies.

`dotnet build --configuration Release` builds the solution; building `RPGManager` also runs the roadmap updater before compile/publish.

`dotnet run --project RPGManager` launches the game locally.

`dotnet run --project Tools/RoadmapUpdater` runs the roadmap sync tool directly.

`dotnet publish RPGManager -c Release -r win-x64 -p:PublishSingleFile=true --self-contained true` creates a self-contained release build in `RPGManager/bin/Release/.../publish/`.

## Coding Style & Naming Conventions
Follow the existing C# style: 4-space indentation, file-scoped organization by namespace folder, `PascalCase` for types and members, `camelCase` for locals and parameters. Keep nullable reference types enabled and avoid magic values when an enum or shared constant fits better. Public types and members should include XML documentation comments. Match existing patterns such as one primary type per file, for example `Characters/Heroes/Warrior.cs`.

When extending persistence or equipment systems, follow the existing save-model pattern: add a matching `*SaveData` type and register any required JSON discriminator mappings so saves remain backward-compatible.

## Testing Guidelines
There is no dedicated test project yet; CI currently runs restore and release build only. Until tests are added, validate changes with `dotnet build --configuration Release` and a manual run of `dotnet run --project RPGManager`. When adding tests, place them in a separate `*.Tests` project and use names like `CharacterFactoryTests.cs`.

## Commit & Pull Request Guidelines
Recent history uses short imperative commit subjects such as `Revamp console intro and welcome flow` and `Refactor character display and UI menus`. Keep commits focused and descriptive. Branches should follow `feature/<system-name>` when applicable. Open PRs against `master`, summarize gameplay or architecture impact, link related issues, and include screenshots or terminal output when UI flow changes.

## Configuration Notes
Save files default to `Save/` in the working directory. Override with `RPGMANAGER_SAVE_DIR`, for example in PowerShell: `$env:RPGMANAGER_SAVE_DIR = "C:\temp\rpg-saves"`.
