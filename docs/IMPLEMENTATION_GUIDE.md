# Implementation Guide

A planning companion, not a script. The goal is to help you think about what to build and why certain structures fit this kind of game — not to tell you exactly how to write it. For the full technical system reference, see `CLAUDE.md`.

---

## Where You Are Now

The foundation is solid: characters with stats, a save system, a working main menu, a spell base class, and stubs for Explore and Fight. The codebase compiles and runs.

The next frontier is making the game actually playable. Right now nothing in the game loop has real consequences — no fight can happen, no world changes. The question to ask yourself is: what is the smallest thing I could build that the player can actually feel?

Combat is the natural answer. It is the engine that makes XP, loot, spells, world events, and quests all meaningful. Without it, everything else is scaffolding with nothing to hang on it.

---

## Open Feature Branches

These branches exist on the remote and are ready to work on:

- `feature/combat`
- `feature/leveling`
- `feature/world-state`
- `feature/spells`
- `feature/npc-dialogue`
- `feature/travel`
- `feature/quests`
- `feature/economy`

`feature/spells` also has a local branch. The rest only exist remotely — check one out when you are ready to start on that system.

---

## Core Concepts

### Interfaces — expressing "can do X" without forcing a relationship

C# inheritance says "is a". An interface says "can do". The difference matters when two very different things need to behave the same way in one context. A `Warrior` and an `Ash Scavenger` are nothing alike — but inside a combat loop, both need to take damage, both have health, both have a turn. An interface lets you express that shared contract without forcing enemies to inherit from `Character` or characters to inherit from `Enemy`. The combat loop can then work with both without knowing or caring which it is talking to.

The rule of thumb: reach for an interface when you want to say "anything that can do this", and reach for a base class when you want to share actual implementation across related types.

### Dictionary vs List — choosing the right container

Both hold collections of things, but they answer different questions.

A `List<T>` answers: "give me everything, in order." You loop over it, you display it, you process each item. Inventory contents, quest objectives, active status effects, encounter pools — these are all things you mainly iterate.

A `Dictionary<TKey, TValue>` answers: "give me the thing with this ID, right now." You look things up by key. World flags, cooldown tracking per spell, dialogue nodes by ID, reputation by region — these are all things where you arrive with a specific key and want the matching value instantly. The tell is when you catch yourself writing `list.Find(x => x.Id == someId)` repeatedly. That is a list doing a job a dictionary should do.

One thing to watch: if you scatter the key strings as literals across the codebase (`"AshlandsBanditCampCleared"` here, `"AshlandsBanditCampCleared"` there), a single typo means a flag silently never gets set and you cannot reproduce the bug. Define the keys as constants in one place. Then the compiler catches typos instead of you hunting them at runtime.

### Wrapper classes — when a collection needs rules

A plain `List<Item>` just stores things. It does not protect any rules. The moment you need "you can only add an item if there is space" or "you cannot start a quest that is already active" or "you cannot create a new character until a slot is unlocked", you need something that owns that logic. A wrapper class is the gatekeeper. The rest of the code asks the wrapper — it does not reimplement the rules itself. This keeps those rules in exactly one place, which means changing them later is one change, not a search across every menu and factory.

### Computed vs stored state

If a value can always be calculated from other data you already have, storing it separately creates a new problem: keeping it in sync. Every time the underlying data changes, you have to remember to update the derived value too. Miss it once and they drift out of sync — and that kind of bug is hard to reproduce.

The most common example here: used inventory slots. If you store a `usedSlots` counter and update it in add and remove, you are betting that every code path remembers to do so correctly. If instead you compute it by summing the sizes of the items currently in the list, it is always correct and costs almost nothing. 

A second example: reputation tier labels. If you store both the raw score (72) and the label ("Trusted"), they can desync. Compute the label from the score on demand — there is one source of truth.

The test: if the underlying data changes, does this value automatically stay correct? If yes, it is fine to store. If no, compute it.

### State machines — modeling flows with phases

Some flows have distinct phases that need to transition cleanly. Combat is the clearest example: it is the player's turn, then the enemy's turn, then effects tick, then you check for a winner, and then the cycle repeats — with special cases (frozen player, calling reinforcements) that only apply in certain phases.

If you model that as a chain of `if/else` statements, every new special case makes the whole thing harder to follow. A state machine — an enum of phases and a switch that decides what happens in each — keeps each phase self-contained. Adding "enemy calls reinforcements at 40% HP" is a check inside the enemy's phase, not a new condition woven through everything else.

### Inheritance vs composition

Inheritance is powerful but becomes a trap when the shared base class starts asking "what type am I actually?" If you ever write `if (this is Mage)` inside `Character`, the hierarchy is working against you. That logic belongs in `Mage`.

A better sign that inheritance is working: every subclass uses what it inherits, every override adds something specific to that type, and the base class never needs to know what its subclasses are.

When the hierarchy starts feeling forced — when you are adding properties that only make sense for one subclass but have to live in the base — consider whether an interface or a separate component would fit better.

---

## Per System — Key Questions

These are not steps. They are the architectural questions worth thinking through before you start each system.

### Combat

The central design question is: how do you write a loop that treats your heroes and enemies the same way, even though they are very different classes? Think about what a fight actually requires from both sides, and whether an interface is the right way to express that contract.

The second question is about flow. A turn-based fight has phases that transition from one to the next. What happens when it is the player's turn? What transitions to the enemy's turn? What do you check between turns? A state machine is one way to model this cleanly. How would you decide what the current phase is and what comes next?

Enemy behavior is a third interesting question. You have three behavior types in the lore: aggressive, defensive, random. How would you represent that as data on an enemy rather than hardcoding a series of checks in the combat loop?

And finally: status effects. Burn, Freeze, Poison are all things that happen at the end of a round rather than immediately. How would you store and process them in a way that does not require special-casing each one inside the main loop?

### XP and Leveling

The key question is where level-up bonuses belong. `Warrior` gaining max health and `Mage` gaining max mana are class-specific behaviors. Does that logic belong in `Character`, or does each subclass own its own growth? Think about what happens when you add a new class: how much do you have to change?

For XP thresholds, think about the tradeoff between a mathematical formula (level² × 100) and a plain array of values. A formula is elegant but inflexible — one number in the formula affects every level. An array is more verbose but lets you tune each transition independently. Which matters more for a game you want to balance by feel?

The roster is a collection with rules. What rules does it need? Who is responsible for enforcing them — the menu, or the roster itself?

### World State

World state is the shared memory of the game. The first question is access: this state is read and written by combat, dialogue, quests, travel, and the economy. How do you make it accessible from all of those without passing it as a parameter through every method call? What are the tradeoffs of a singleton or static class?

The second question is key management. You will eventually have dozens of flags across all regions. What is the risk of writing flag names as string literals in code? How would you protect against that?

Reputation is a good design exercise. You need to store a score and display a tier label. Should you store both? If not, which one is the source of truth and which is derived?

### NPC Dialogue

A dialogue tree is navigation. You are always at a node, and player choices move you to a different node. The structure question is: how do you model a tree where jumping between nodes needs to be fast? A list, a tree of objects, or something indexed by ID?

Conditions are another good design question. An NPC might show one option to a Mage but not a Warrior, another option only after a certain flag is set, and a third only once reputation is high enough. If all of those checks live inside `DialogueOption` as nested `if` statements, what happens when you have twenty NPCs with complex dialogue? Is there a structure that would let you add new condition types without touching the option class itself?

Same question for actions: setting flags, granting quests, adding reputation. Where does that logic belong?

### Inventory

The core question is: who enforces the rules? Right now `CharacterFactory` adds weapons in a somewhat ad-hoc way. When you build `Inventory`, think about what it means for the inventory itself to be the gatekeeper. What should `AddItem()` actually check before adding? What happens when the check fails?

The slot count question is a good test of computed vs stored state. Should `Inventory` keep a running counter of used slots, or compute it on demand? What could go wrong with each approach?

### Travel

A location is mostly data. Its name, what level is required, whether it is currently accessible, which enemies can appear there. The question is: who reads that data and makes decisions from it? Does a location decide whether you can enter, or does the travel system check the location's data and decide?

Encounter pools are another design question. An encounter is not a single enemy — it is a group configuration. How would you model "3 bandits" as something different from "1 bandit captain + 2 bandits"? And how does travel decide which one to use?

### Quests

The most important design question for quests is: where does progress live? If a quest stores its own "killed 3 of 5 bandits" counter, that counter is isolated to one character and resets if the quest object is recreated. If progress lives in `WorldState` as a named counter, it is persistent, shared across characters, and written by combat without needing to know anything about quests. Think about which design keeps the systems more loosely coupled.

The second question is about the `Quest` object itself: should a quest contain logic, or just describe what needs to happen and what the reward is? What is the difference between a quest that knows how to complete itself versus a `QuestTracker` that manages completion for all quests?

### Economy

The relationship between shops and NPCs is worth thinking about before you build it. Does a shop need an NPC? Does an NPC need a shop? Which direction should the dependency point, and why does it matter?

Price calculation is another good design question. The price of an item changes based on the buyer's reputation in the current region. Where does that calculation live — on the item, on the shop, or somewhere else? What needs to be passed in for the calculation to work?

---

## What Order Makes Sense

**Combat first.** Not because it is required, but because it is the one system that makes everything else in the game meaningful. XP needs a source. Loot needs a moment to drop. Spells need a context. World state needs something to react to.

**World state early and small.** It depends on nothing — you can start it at any time alongside combat. The point of world state is that it is the shared memory of the game. Keep the first version narrow: flags, counters, a small event log. Its value becomes obvious once two or three systems start writing to it.

**Everything else after the loop feels real.** Dialogue, travel, quests, and economy are much more satisfying to build once there is an actual game loop to slot them into. Building an economy before there is a fight to earn gold from is building for a game that does not exist yet.

---

## When Is A Feature Ready To Move On?

Not perfect — just solid enough that the next system can be built on top of it without it collapsing. The question to ask is: does the core loop work? Not every edge case, not every polish item. The one thing the feature must do.

| Feature | The one thing that must work |
|---|---|
| Combat | Enter a fight, take turns, one side wins or loses |
| Leveling | Gain XP from a fight, level up, stat changes apply |
| World State | Set a flag, read it back, survive a save/load cycle |
| Spells | Mage casts a spell during combat, mana is spent |
| NPC Dialogue | Walk through a tree, a condition filters an option, a flag gets set |
| Travel | Move between two locations, encounter triggers a fight |
| Quests | Accept a quest, make progress, complete it, get a reward |
| Economy | Buy an item from a shop, gold is deducted, item is in inventory |

If the thing in the right column works, the feature is ready enough. Everything else — edge cases, polish, rare scenarios — can come back to it later once the surrounding systems exist and show you what actually needs fixing.

---

## What To Avoid Too Early

- Building a fully generic engine before one region works end to end
- Loading systems from JSON or external files before the hardcoded version feels right — you do not know what the data shape needs to be until the system works
- Deep inheritance hierarchies where a small interface or data object would do
- Persisting values that can be recomputed — inventory slot counts, reputation tier labels, alive/dead state
- Building multiple regions before the Ashlands proves the full gameplay loop
- Designing for future systems that do not exist yet — three similar cases is not a pattern worth abstracting until you have three actual cases

---

## Questions To Ask Yourself Before Adding Something

- Can the player feel this yet, or is it invisible scaffolding?
- Is this protecting a real rule, or just renaming a list?
- Will this be saved, or can it be derived at runtime?
- Am I solving a problem I actually have, or one I imagine having later?
- If I add this now, does it make the next system easier or harder to slot in?
- Who owns this rule — is the logic in one place, or will I end up copying it?