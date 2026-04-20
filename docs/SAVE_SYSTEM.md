# Save System

How the save system works and how to extend it when you add a new feature.

---

## How It Works Now

`SaveManager` writes and reads a JSON file at `Save/characters.json` (overridable via `RPGMANAGER_SAVE_DIR`). The current root of that file is a `List<CharacterSaveData>` — just a list of character snapshots.

The pattern for each saved type is always two methods:
- A **constructor** that takes the live object and snapshots its state into the save data class
- A **`ToX()`** method that reconstructs the live object from the snapshot

`CharacterSaveData` is a good example to read before adding your own. It captures name, health, gold, mana, and equipables — and `ToCharacter()` rebuilds the correct subclass based on the `CharacterType` string discriminator.

---

## The Root Save Object Problem

Right now the root of the save file is a plain `List<CharacterSaveData>`. This works while the game only saves characters — but `WorldState`, roster metadata, and future per-save data cannot fit into that structure.

At some point `SaveManager` needs to move to a root `SaveGame` object, something like:

```
SaveGame
├── List<CharacterSaveData> Characters
├── WorldStateSaveData WorldState
└── RosterSaveData Roster   (when leveling exists)
```

The `TODO` in `SaveManager` marks this. When you add `WorldState` and need to persist it, that is the right moment to make this change — not before.

---

## Extending the Save System

Every new system that has persistent state needs:

**1. A `*SaveData` class** — a plain snapshot of the data, no game logic. Only fields that cannot be recomputed at runtime. Properties that can be derived (like reputation tier labels, computed slot counts, alive/dead state) should not be saved — compute them on load instead.

**2. Either added to an existing save data class or wired into the root `SaveGame`** — character-level data like `Level` and `Experience` belongs on `CharacterSaveData`. World-level data like flags and reputation belongs on a `WorldStateSaveData` next to the character list.

**3. Populated in the constructor and restored in the `To*()` method** — the same pattern `CharacterSaveData` already uses. The constructor snapshots, `ToCharacter()` restores. Keep them in sync — if you add a field to one, add it to the other.

---

## Polymorphic Types and Discriminators

When you save a collection of objects where each item might be a different subclass — like `List<IEquipable>` containing both `Weapon` and `Quiver` — JSON needs to know what type to deserialize each entry back into. That is what the `CharacterType` string on `CharacterSaveData` does: it stores which subclass to reconstruct.

`EquipableSaveData` uses the same pattern with its own type discriminator. When you add a new equippable type (a new weapon, a new item category), you need to:
- Create a matching `*SaveData` subclass
- Handle it in the `ConvertEquipables` helper in `CharacterSaveData`
- Handle it in `ToEquipable()` on the save data base class

If you skip any of these, loading a save with the new item type will throw an exception.

---

## What To Keep Out of Save Data

- Computed values: reputation tier label (compute from score), used inventory slots (compute from items), `IsAlive` (compute from health)
- References to other objects: do not save a reference to a `World` or `Location` object — save its ID or name and look it up on load
- Temporary state: status effects mid-combat, current menu state, anything that resets naturally between sessions

---

## Known Upcoming Changes

| What | When | Where |
|---|---|---|
| Move root from `List<CharacterSaveData>` to `SaveGame` object | When WorldState is added | `SaveManager` |
| Add `Level` and `Experience` to `CharacterSaveData` | When leveling is added | `CharacterSaveData` |
| Add `WorldStateSaveData` | When world-state is added | New class + `SaveGame` root |
| Add full `Inventory` to `CharacterSaveData` | When inventory replaces loose weapon fields | `CharacterSaveData` |
