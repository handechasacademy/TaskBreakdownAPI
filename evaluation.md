# Evaluation of AI-Generated Content Quality

## Quality Criteria

| Criteria | Description |
|---|---|
| Relevance | Are the microsteps relevant to the goal? |
| Correctness | Are the steps accurate and actionable? |
| Hallucinations | Does the model invent false information? |
| Consistency | Are responses consistent across similar prompts? |
| Language | Is the language clear and encouraging? |

---

## Test 1: Clean my room

**Input:**
- Goal: "Clean my room"
- Barriers: "Don't know where to start, feels overwhelming"
- Scare Factor: 7

**Output (microsteps):**
1. Gather all clothes and place them in the middle of the room.
2. Sort the clothes into piles: keep, donate, and throw away.
3. Empty the trash can and recycling bin.
4. Dust one piece of furniture.
5. Make the bed.

**Encouragement:** "You've got this! Let's tackle one small corner at a time..."

**Analysis:**
- Relevant and concrete steps
- ADHD-friendly: small, specific actions
- Encouraging tone
- Steps assume the room has specific items (e.g. recycling bin) -- not always true
- Only 5 steps -- could be more granular for high scare factor (7/10)

---

## Test 2: Start working out

**Input:**
- Goal: "Start working out"
- Barriers: "No motivation, don't know where to begin"
- Scare Factor: 8

**Output (microsteps):**
1. Choose a type of workout you enjoy.
2. Set a specific time for your workout.
3. Find a workout buddy or join an online community.
4. Start with a 5-minute warm-up.
5. Do a simple 10-minute workout routine.

**Encouragement:** "You've got this! Every small step brings you closer to your goal..."

**Analysis:**
- Steps are logical and progressive
- Addresses the barrier (no motivation) by suggesting a buddy
- Encouragement is nearly identical to Test 1 -- low consistency/variation
- "Find a workout buddy" ignores the barrier "no motivation" -- social steps can feel overwhelming for ADHD/autism users

---

## Test 3: Learn to code

**Input:**
- Goal: "Learn to code"
- Barriers: "Too much to learn, don't know where to start"
- Scare Factor: 9

**Output (microsteps):**
1. Choose a programming language (e.g., Python).
2. Find an online tutorial or course for beginners.
3. Set 30 minutes daily for coding practice.
4. Complete the first lesson of your chosen course.
5. Build a simple "Hello, World!" program.

**Encouragement:** "You've got this! Every small step you take..."

**Analysis:**
- Classic and correct beginner path
- Very relevant to the goal and barriers
- Encouragement is almost identical across all three tests -- model lacks variation
- Slight hallucination risk: assumes Python is always the best choice without context

---

## Limitations

**Repetitive encouragement:** All three responses used near-identical encouragement phrases ("You've got this! Every small step..."). The model lacks variation in tone.

**Prompt sensitivity ("Garbage in, garbage out"):** The quality of microsteps depends heavily on how specific the barriers and scare factor are. Vague input produces generic output.

**Bias:** The model defaults to common Western productivity advice (workout buddies, Python for coding). May not suit all users.

**No memory:** Each request is stateless. The model cannot build on previous responses or adapt over time.

**Hallucination risk:** Low in these tests, but the model can confidently suggest steps that don't apply to the user's specific situation (e.g. assuming a recycling bin exists).

---

## Conclusion

The model performs well for straightforward, common goals. Microsteps are generally relevant, actionable, and ADHD-friendly. The main weaknesses are repetitive encouragement, sensitivity to vague input, and occasional assumptions about the user's context. For production use, prompt engineering improvements (e.g. forcing varied encouragement, requesting more than 5 steps for high scare factors) would significantly improve output quality.