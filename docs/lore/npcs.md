# NPC Profiles — The Ashlands

*Five named NPCs for the Ashlands region. Each has a role, a personality, and class-specific dialogue hooks.*

---

## Korin Ashwell
**Role:** Blacksmith of Cinder's Rest. Shop owner, minor quest giver.
**Location:** Cinder's Rest (smithy)
**NPC ID:** `korin_ashwell`

**Background:** Korin has run the only smithy in the Ashlands for twenty years. He came here after a failed business in the west — won't say what happened, doesn't like being asked. He's built a decent life in Cinder's Rest and is deeply protective of it. Gruff, honest, not warm but not cruel. He lost his apprentice to a bandit ambush on the Scorched Path six months ago. He hasn't replaced the boy.

**Personality:** Practical. Measures people by what they do, not what they say. Doesn't trust strangers until they've proven themselves.

**Dialogue hooks by class:**
- **Warrior:** Korin respects fighters. After the player clears the Scavenger bounty, he offers to upgrade a weapon for free. After the Scorched Path bandits are dealt with, he mentions his apprentice — if the player agrees to look for the boy's hammer in the bandit camp, Korin rewards them with a rare weapon.
- **Mage:** Korin is cautious around magic-users — too many bad memories from the old kingdom. He'll trade but won't offer extra quests until Trusted reputation. At Trusted, he confides that a cloaked figure has been buying weapons in bulk "for a mining operation" — which is suspicious, because there are no active mines in the Ashlands.
- **Archer:** Suspicious from the start. "Sneaky types always bring trouble." He'll sell to an Archer but charges slightly higher prices until Known reputation. At Known, he softens — "You're still here. Most sneaky types aren't."

**Reputation reactions:**
- Stranger: Neutral service, no extras
- Known: Acknowledges the player's deeds, slight price discount
- Trusted: Shares the cloaked-buyer information (Hollow Court lead), offers rare stock
- Honored: Treats the player like a friend, maximum discount, tells his full backstory

**Quest hooks:**
- "Teeth at the Gate" — rewards the player after Sera sends them (acknowledges the bounty was cleared)
- "The Apprentice's Hammer" — optional side quest at Known reputation: find the apprentice's hammer in the bandit camp, reward is a rare melee weapon

---

## Sera Voss
**Role:** Innkeeper of the Dustfall Inn. Primary information source. First quest giver.
**Location:** Cinder's Rest (Dustfall Inn)
**NPC ID:** `sera_voss`

**Background:** Sera was an adventurer for fifteen years — explored the Mistwood, fought in the border skirmishes on the eastern frontier, survived things she won't describe in detail. A bad injury to her left leg ended that life. She came to the Ashlands to be somewhere quiet, bought the inn, and has been running it ever since. She knows the region better than anyone and has contacts in every corner of it.

**Personality:** Sharp, observant, dry humour. She has seen too much to be surprised by anything. She talks to everyone who passes through and remembers all of it.

**Dialogue hooks:**
- Serves as the player's information board — knows the region's history, the factions, the dangers
- First quest giver for "Teeth at the Gate" and "The Scorched Road"
- After Quest 2, tells the player about Dren Morrow being found on the road
- At Known reputation: starts hinting that the bandit operation feels too organized for desperate outlaws
- At Trusted: confirms "someone from outside is funding them — I've seen this pattern before, in the east"
- If `DrenMorrowAllied`: Sera can identify the Court Sigil if shown one — "I've seen this before. The Hollow Court. I didn't know they were here."
- If `AshlandsCourtExposed`: her tone shifts — she becomes more urgent, less casual. She's worried.

**Class reactions:**
- No strong class preference. Treats all classes professionally.
- Slightly warmer to Archers (shared background in scouting and mobility)
- Will ask a Mage about the ruins at some point — she's curious, not hostile

**Quest hooks:**
- Main quest giver for quests 1 and 2
- Optional: "The Old Patrol Route" — mapping quest at Known reputation, opens up a faster travel path through the Ashlands

---

## Dren Morrow
**Role:** Injured traveler. Hollow Court low-level operative. Branching choice NPC.
**Location:** The Scorched Path (found during Quest 3)
**NPC ID:** `dren_morrow`
**Flags:** `DrenMorrowFound`, `DrenMorrowAllied`, `DrenMorrowBetrayed`

**Background:** Dren is twenty-six and has made a lot of bad decisions. The most recent was accepting a courier job from a well-paying anonymous client. The job was fine for three months — pick up a sealed letter here, deliver it there, don't open it. Then his handler stopped responding and bandits ambushed his cart on the Scorched Path. His horse is dead. He has a broken rib and a gash on his leg. Nobody is coming for him.

He knows he was working for someone called the Hollow Court. He knows his handler's codename. He doesn't know what's in the letters he carried, what the Court wants, or who they are beyond being "organized and wealthy."

**Personality:** Scared and trying to hide it. Talks too much when nervous. Not brave but not without conscience — he knew the work felt wrong, he just needed the money.

**Dialogue — Help path:**
- Grateful, desperate, honest once he feels safe
- Tells the player about the Court's name and that they're interested in something in the Emberveil Ruins
- After being brought to Cinder's Rest, becomes a contact at the Dustfall Inn
- On future visits: provides scraps of Court intelligence picked up from his courier days (activates quest hooks in later regions)

**Dialogue — Betray path:**
- Never seen again after being handed to Korin
- Korin receives payment from "guild representatives" the next day — the Court retrieved their asset quietly
- The Court Sigil becomes the only route to learning the faction name (from the Emberveil Ruins)

**Class reactions:**
- Responds more openly to Mage characters (assumes mages are educated and won't just kill him)
- Nervous around Warriors ("please don't hurt me — I'll tell you everything")
- Wary of Archers ("you're not going to shoot me, are you?")

---

## Lira the Ashen
**Role:** Hermit mage. Survivor of the Emberveil War. Gate to the ruins questline.
**Location:** Edge of the Emberveil Ruins
**NPC ID:** `lira_the_ashen`

**Background:** Lira was sixteen when the academy burned. She survived because she was in the sub-basement collecting herbs when the fire began. She has lived near the ruins ever since — ninety years now, sustained by old Emberveil magic she won't fully explain. She is the last living person who was there during the war. She has watched the ruins for decades, cataloguing what remains, keeping the most dangerous things contained.

She is sharp-minded but speaks in a way that assumes the listener already knows the context. She jumps topics. She references people who died before the player was born as if they're still around.

**Personality:** Erratic, intensely focused, occasionally frightening. Not malicious — just very old and not fully oriented to the present.

**Access conditions:**
- Only speaks to Mage characters on first encounter (recognises the magic, feels safer)
- Other classes need Ashlands reputation ≥ 30 (Known tier) before she'll engage
- Exception: if `DrenMorrowAllied`, Dren mentions her name and she'll speak to anyone the Keeper sends

**Dialogue hooks:**
- The gate to Quest 4 ("Echoes in the Ruins")
- Teaches the Mage the counter-spell for the seal
- Knows the history of the Emberveil War in full detail — extensive optional lore dialogue
- If the player brings her the Court Sigil: "The Hollow Crown. Yes. They've been trying to get inside for two years. They want the Focus." Sets `AshlandsCourtFound`.
- If `EmberArtifactSecured`: "Good. Keep it away from them. I don't know what they want it for, but anything they want that badly is something they should not have."

**Quest hooks:**
- Quest 4 giver
- Optional: "What the Academy Knew" — translate old Emberveil research notes for her, rewards a spell scroll and deeper lore about the original Compact

---

## Commander Thresh
**Role:** Bandit leader. Regional antagonist. Potential ally.
**Location:** Scorched Path (bandit camp), later the Ashen Throne
**NPC ID:** `commander_thresh`
**Flags:** `ThreslDefeated`, `ThreshNegotiated`, `ThreshAlly`

**Background:** Thresh was a soldier in the eastern border guard for twelve years. When the garrison was disbanded and the pay stopped — no warning, no reason given — he and forty of his men were just left in the field. He led them west. The Ashlands were the only place no one would come looking for deserters. He didn't plan to become a bandit. He just needed to keep his men fed.

Three months after arriving, a contact found him — well-dressed, too clean for the Ashlands, offering gold for a simple service: control the Scorched Path, keep other traders off it. He took the money. He didn't ask who was paying.

He knows what he's doing is wrong. He's made peace with it. Or he tells himself he has.

**Personality:** Military bearing that hasn't faded. Direct. Doesn't enjoy cruelty but will use it when necessary. Genuinely cares about the men under his command.

**Encounter paths:**

**Path A — Combat:** The player fights Thresh in his camp or at the Ashen Throne. He is a hard fight (Bandit Enforcer level stats + above). If defeated, the bandit operation collapses, Cinder's Rest is safer, and the player receives significant gold and reputation. World flag: `ThreshDefeated`.

**Path B — Negotiation:** Accessible if the player has Known reputation with Thresh's camp (achieved by completing bounties without killing everyone) and does not attack on sight. Thresh is willing to talk. If the player shares Dren's information — that his "Guild" contact is the Hollow Court and he's being used — Thresh is furious. He agrees to pull his men off the Scorched Path in exchange for safe passage out of the Ashlands. World flag: `ThreshNegotiated`. NPCs in Cinder's Rest react differently depending on the outcome — some prefer a dead bandit leader; others appreciate the bloodless resolution.

**Path C — Alliance:** Accessible only if `ThreshNegotiated` and the player has Trusted reputation. Thresh agrees to keep an eye on Hollow Court movements in the Ashlands. He becomes an intelligence contact, providing information once per session. World flag: `ThreshAlly`. The Court sends a Sentinel team to deal with him shortly after — the player can optionally protect him.

**Class reactions:**
- Respects Warriors as equals (soldier to soldier)
- Distrusts Mages ("academy types think they're above all of us")
- Surprisingly respectful of skilled Archers ("I had one like you. Best scout I ever had.")

---

*Last updated: April 2026*
