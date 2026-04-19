# Contributing to RPG Manager

RPG Manager is a personal learning project. I build everything myself to develop my C# skills, so the contribution model is a bit different from a typical open source project.

## Reporting Bugs

Open a [GitHub Issue](https://github.com/tombomeke-ehb/RPGManager/issues) with:
- What you expected to happen
- What actually happened
- Steps to reproduce
- Your OS and .NET SDK version

## Suggesting Features

Open a [GitHub Issue](https://github.com/tombomeke-ehb/RPGManager/issues) describing the feature and why it would fit the game. Check [ROADMAP.md](ROADMAP.md) first — it may already be planned.

## Code Style

If you do submit a pull request, please follow these conventions so it matches the rest of the codebase:

- All public types and members must have XML doc comments (`<summary>`, `<param>`, `<returns>`, `<exception>` where applicable)
- Follow existing naming conventions: PascalCase for types and members, camelCase for locals and parameters
- Enums for game constants — no magic strings or numbers
- Branch naming: `feature/<system-name>` (e.g., `feature/combat`, `feature/quests`)
- All changes go through a pull request to `master` — no direct pushes

## A Note on Pull Requests

Pull requests with code are welcome, but I may rewrite them in my own style before merging. This is intentional — understanding every line of code in this project is part of the goal. I will always credit contributions in the commit message or CHANGELOG.

By contributing, you agree that any accepted code becomes part of this project and is subject to its license terms. See [LICENSE](LICENSE).

Thank you for taking an interest in the project.
