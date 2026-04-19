# The Ashlands

*The first region. Starter difficulty. The Keeper awakens here.*

---

## Overview

The Ashlands are the scarred remnants of the **Emberveil Kingdom**, consumed two centuries ago in a war between fire mages and a dragon cult. Neither side won. The mage academies burned. The dragon cult collapsed. What was left was a landscape of volcanic rock, perpetual ash-fall, and pockets of magical fire that have been burning ever since.

The rest of Tavaryn largely forgot the Ashlands after the war. No one wants the land. No one trades with it in meaningful volume. The people who live here — maybe a few thousand across the whole region — are the stubborn, the desperate, and those with nowhere else to go.

This is where new Keepers awaken. The Veil between worlds is thinnest where destruction was greatest, and nothing in Tavaryn was more thoroughly destroyed than the Emberveil Kingdom.

**Level range:** 1–10
**First character slot unlocks at:** Start of game
**Region completion unlocks:** Travel to the Mistwood, second character slot

---

## Locations

### Cinder's Rest
*Starter hub. Level 1–3 area.*

A small settlement built around a natural hot spring — warm water, warm rock, and the smell of sulfur. Population roughly forty. It is the closest thing the Ashlands has to a safe town.

The buildings are made of volcanic stone and salvaged metal. The streets are always dusted in grey ash. At night, the distant glow of the Emberveil Ruins is visible on the horizon.

**What's here:**
- Korin Ashwell's smithy (shop, quest giver)
- The Dustfall Inn (Sera Voss, information hub, quest board)
- A small market with basic supplies

**Enemy encounters:** Ash Scavengers near the town gates (patrol range). No serious threats inside town.

**World flags set here:**
- `AshlandsScavengerBounty` — first bounty quest completed
- `CindersRestReputation` — tracks regional reputation score

---

### The Scorched Path
*Travel corridor. Level 2–5 area.*

The old trade road that once connected the Emberveil Kingdom to the western territories. Now mostly abandoned. The canyon walls on either side are charred black. Bandit camps dot the route. Ambushes are common in the narrow sections.

Travelling here triggers random encounter rolls. The further along the path, the tougher the enemies.

**What's here:**
- Three bandit camps (clearable)
- Dren Morrow's location (found injured on the road after quest 2)
- A broken-down merchant wagon (loot opportunity)

**Enemy encounters:** Scorched Path Bandits, Bandit Enforcers
**World flags set here:**
- `ScorchedPathPatrolCleared` — first bandit patrol defeated
- `DrenMorrowFound` — player encounters the injured traveler
- `DrenMorrowAllied` / `DrenMorrowBetrayed` — branching outcome

---

### Emberveil Ruins
*Mid-region dungeon. Level 5–8 area.*

What remains of the largest mage academy in the old kingdom. The surface level is a maze of collapsed walls and open sky. The underground chambers are intact — preserved by the same magic that destroyed the kingdom above.

Fire elementals nest in the volcanic vents. Ash Wraiths haunt the old lecture halls. In the deepest chamber, behind a magical seal, is an artifact the Hollow Court has been trying to reach.

**What's here:**
- Lira the Ashen's camp at the ruin's edge
- Three dungeon levels: collapsed surface, intact underground, sealed chamber
- Class-gated entry to the sealed chamber:
  - Mage: Lira teaches you the counter-spell to break the seal
  - Warrior: A collapsed east wall can be smashed through (requires Power 15+)
  - Archer: A ventilation shaft in the north tower provides a back route (requires Perception 12+)
- Evidence of Hollow Court activity in the sealed chamber (Court tools, a sigil)

**Enemy encounters:** Ember Sprites, Ash Wraiths, Hollow Court Sentinels (deepest level)
**World flags set here:**
- `EmberSealBroken` — player accessed the sealed chamber
- `AshlandsCourtFound` — player found evidence of the Court
- `EmberArtifactSecured` — player retrieved the artifact before the Court

---

### The Ashen Throne
*End-of-region content. Level 8–10 area.*

A volcanic plateau at the heart of the Ashlands. An ancient dragon's skeleton sits at the peak, wings spread as if still in flight. The locals call the stone seat between its claws the Ashen Throne and say whoever sits there inherits the dragon's curse.

In reality, the Hollow Court uses the plateau as a meeting point. It is their most secure location in the Ashlands — easily defensible, isolated, and avoided by superstitious locals. Their elite Sentinels patrol it in force.

**What's here:**
- The dragon skeleton (lore object, origin of the Emberveil War)
- Hollow Court Sentinel patrols (heaviest concentration in the region)
- A Court operations tent (readable documents, reveals the Court's name and regional commander)
- Commander Thresh's fate resolves here if he was not dealt with on the Scorched Path

**Enemy encounters:** Bandit Enforcer + Hollow Court Sentinel (mixed patrols), elite Sentinels
**World flags set here:**
- `AshlandsCourtExposed` — player learned the Hollow Court's name
- `AshlandsComplete` — all major objectives done, unlocks next region

---

## Enemy Roster

### Ash Scavenger
- **Element:** None
- **Behavior:** Aggressive — always attacks, no special actions
- **Level range:** 1–2
- **Description:** Feral wild dogs that roam the ash wastes in packs. Survive by scavenging ruins and harassing travellers. Attack in groups of 2–3. Low health, low damage. Exist to teach the player basic combat.
- **Loot:** Scraps (crafting material), 2–5 gold per kill
- **XP:** 15 per kill

---

### Scorched Path Bandit
- **Element:** None
- **Behavior:** Aggressive — attacks immediately, no tactics
- **Level range:** 2–4
- **Description:** Desperate outlaws in mismatched armour. Former farmers, miners, or soldiers who ran out of options. They fight with rusty swords and clubs. Some carry shortbows. They're not skilled — they're just hungry and scared.
- **Loot:** Rusty Sword or Dagger (common rarity), 10–25 gold per kill
- **XP:** 30 per kill

---

### Bandit Enforcer
- **Element:** None
- **Behavior:** Defensive — attacks normally above half health; uses a healing potion when below half health (once per fight). Always appears alongside 1–2 Scorched Path Bandits.
- **Level range:** 4–6
- **Description:** Commander Thresh's lieutenants. Better equipped, more experienced, and smart enough to carry supplies. They lead the patrol groups and are the main obstacle on the Scorched Path.
- **Loot:** Sword or Axe (uncommon rarity), 30–60 gold per kill, occasional Healing Potion
- **XP:** 60 per kill

---

### Ember Sprite
- **Element:** Fire — deals fire damage; has a 30% chance per hit to apply **Burn** (damage over time for 3 turns)
- **Behavior:** Random — attacks, defends, or does nothing each turn (unpredictable feel)
- **Level range:** 3–5
- **Weakness:** Ice element deals 1.5× damage
- **Description:** Small fire elementals that flicker like embers in wind. They dart in, strike, and dart away. Not intelligent — they act on instinct. Found near volcanic vents and the outer ruins.
- **Loot:** Fire Essence (crafting material, used later), 8–15 gold per kill
- **XP:** 40 per kill

---

### Ash Wraith
- **Element:** Fire — deals fire damage; resistant to Physical (0.5× damage from non-elemental weapons)
- **Behavior:** Defensive — fights normally until below 30% health, then phases out (skips a turn, untargetable, regenerates 10% health). Can phase out once per fight. **Mages deal 2× damage** — the wraiths are vulnerable to directed magical force.
- **Level range:** 5–8
- **Description:** The ghosts of mages and students who died when the academy burned. They are not evil — they are trapped, reliving their last moments, unable to move on. They cast fire in the same patterns they used in life. Found in the deeper sections of the ruins.
- **Loot:** Spell Scroll (random common spell, usable by Mage), 15–30 gold per kill
- **XP:** 80 per kill

---

### Hollow Court Sentinel
- **Element:** None
- **Behavior:** Defensive — attacks normally until below 40% health, then retreats (runs to edge of encounter zone and calls reinforcements: 1 additional Sentinel joins the fight after 1 turn). Can call reinforcements once per fight.
- **Level range:** 7–10
- **Description:** Masked soldiers in black and gold armour. They do not speak. They do not negotiate. They are professional, disciplined, and very well equipped compared to anything else in the Ashlands. Their presence here means the Court has been operating in the region for a long time.
- **Loot:** Quality Sword or Spear (rare rarity), 50–100 gold per kill, **Court Sigil** (key item — always drops from the first Sentinel killed)
- **XP:** 120 per kill
- **Note:** The Court Sigil can be shown to Sera Voss or Lira the Ashen to learn the Hollow Court's name, setting `AshlandsCourtFound`.

---

## Quest Chain

### Quest 1: "Teeth at the Gate"
- **Giver:** Sera Voss (Dustfall Inn, Cinder's Rest)
- **Type:** Kill quest
- **Objective:** Kill 5 Ash Scavengers near Cinder's Rest
- **Reward:** 50 gold, basic weapon upgrade (Korin sharpens one weapon for free), +10 Ashlands reputation
- **Purpose:** Teaches combat. Enemies are trivial, close to town, no travel required.
- **Flag set:** `AshlandsScavengerBounty`
- **World reaction:** Korin acknowledges it next visit — "You're the one who dealt with those dogs? Good."
- **Unlocks:** Quest 2

---

### Quest 2: "The Scorched Road"
- **Giver:** Sera Voss (after Quest 1)
- **Type:** Travel + kill quest
- **Objective:** Travel to the Scorched Path and defeat the bandit patrol (3 Scorched Path Bandits + 1 Bandit Enforcer)
- **Reward:** 100 gold, +15 Ashlands reputation
- **Purpose:** Teaches the travel system and random encounters. Slightly harder combat.
- **Flag set:** `ScorchedPathPatrolCleared`
- **World reaction:** Sera tells the player about a man found injured on the road — sets up Quest 3.
- **Unlocks:** Quest 3 (auto-triggers on next visit to the Scorched Path)

---

### Quest 3: "The Stranger's Burden"
- **Giver:** Auto-triggers when the player reaches the Scorched Path after Quest 2
- **Type:** Dialogue + choice quest
- **Objective:** Find Dren Morrow, injured by the road. Decide what to do.

**Choice A — Help Dren:**
- Spend a healing item (or have a Mage cast a healing spell)
- Escort him back to Cinder's Rest
- Reward: Dren becomes an informant; on subsequent conversations he reveals the existence of "the Court" and their operations in the region. Sets `DrenMorrowAllied`. +20 reputation.
- Side effect: Dren appears in Cinder's Rest on all future character visits.

**Choice B — Turn Dren in:**
- Bring him to Korin as a suspicious outsider
- Reward: 150 gold from Korin, +10 reputation with Korin specifically
- Side effect: Dren disappears (Court retrieves him). The `DrenMorrowBetrayed` flag delays access to the Court's name until the Emberveil Ruins arc.
- All characters are affected — if your second character visits the path, Dren is either at the inn or gone.

- **Purpose:** Teaches branching world flags and shows the shared-world mechanic in action. The player sees that their choice affected the world for all their heroes.

---

### Quest 4: "Echoes in the Ruins"
- **Giver:** Lira the Ashen (edge of the Emberveil Ruins)
- **Unlock condition:** Ashlands reputation ≥ 30 (Known tier) OR `DrenMorrowAllied` is set (Dren mentioned Lira)
- **Note:** Lira initially only speaks to Mage characters. Other classes need Known reputation before she'll engage.
- **Type:** Dungeon exploration + class-gated quest
- **Objective:** Enter the Emberveil Ruins and reach the sealed chamber.

**Class-specific paths:**
- **Mage:** Lira teaches you the counter-spell. Enter through the front gate and cast it on the seal.
- **Warrior:** Find the collapsed east wall in the ruins and smash through it (requires Power ≥ 15).
- **Archer:** Find the ventilation shaft in the north tower (requires Perception ≥ 12). Discovered by examining a gap in the tower wall.

**Inside the sealed chamber:** A collection of old artifacts, research notes from the pre-war academy, and — if the player examines the workbench — tools and a Court Sigil left by a Hollow Court operative. The artifact itself (an Emberveil Focus) can be taken.

- **Reward:** Class-specific item from the sealed chamber (Mage: Emberveil Staff scroll; Warrior: Dragonshard Armour fragment; Archer: Ash-carved Bow grip), +30 Ashlands reputation, `EmberSealBroken` flag set.
- **If Court Sigil found:** Sets `AshlandsCourtFound`. Showing it to Sera or Lira reveals the name "Hollow Court."
- **Purpose:** Teaches class-specific mechanics and demonstrates the value of the roster. Each class gets a unique interaction — players are motivated to return with a different hero.
- **Unlocks:** Travel to the Ashen Throne (final Ashlands area)

---

## The Hollow Court in the Ashlands

The Court has been present in the Ashlands for years, but they do not want to be known here. They operate through two proxies:

**Proxy 1 — Commander Thresh:** Thresh receives funding and logistical support through a contact he knows only as "the Guild." He believes he's working for a merchant consortium that wants to control the Scorched Path trade route. He does not know the Court exists. If the player exposes this to Thresh (via Dren's information), he reacts with fury and becomes a potential ally rather than an enemy — though the Court will send Sentinels to deal with him regardless.

**Proxy 2 — Dren Morrow:** A low-ranking Court courier. His job was to carry a report on the Emberveil Ruins artifact to his handler. He was ambushed by bandits and his handler abandoned him. He knows the Court exists and knows the codename "the Hollow Court" — but not much more. He is not a loyal operative; he was mostly doing it for the gold.

**Direct presence:** Hollow Court Sentinels patrol the Ashen Throne and the deepest level of the Emberveil Ruins. They are the first time the player fights Court soldiers directly. At the Ashen Throne, a tent containing operational documents names the Court, describes the artifact they want, and mentions "the broader network across Tavaryn."

**Arc conclusion:** The Ashlands arc ends with the player learning:
1. The Hollow Court exists and has a name
2. They are interested in the old Emberveil artifact
3. They operate through proxies across Tavaryn (this is not just a local problem)

This sets up the same pattern repeating in every subsequent region — the Court is always present, always pursuing something, always a layer underneath the surface.

---

*Last updated: April 2026*
