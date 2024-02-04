
# wareware
guidelines for programmers

1. **DO NOT TOUCH `GameStateManager`**
	- Don't edit it.
	- If you REALLY need to touch it, contact `@malachek` (probably on discord)
2. **For organization's sake:**
	- Each game is its own scene - place ALL SCENES in the scenes folder
	- For each game, make a separate folder which will store everything related to that game - whether that be art, scripts, music/sounds, etc.
3. You are able to test your game via running them on their own scene. A runtime error will happen upon completion.
4. **If you need to add your game to the main game, contact `@malachek`**
	- Implementation itself involves touching `GameStateManager`, which you prob shouldn't touch
5. **NAMING CONVENTIONS**:
	- For standalone games, name them whatever FITTING name you want
	- **BUT** if your game is a series game (as in, playing a game requires the completion of another game):
		- Each game scene name must have the same name, followed by a number that tells what comes next
		- xxxx1 is the last game of the series, xxxx2 is played before xxxx1, and xxxx3 plays before xxxx2.
			- DO NOT go past xxxx9!
		- EXAMPLE
			- If you must play "game 1" before "game 2", game 1 should be named something like `Game2`, and game 2 should be named `Game1`
			- It's kinda like a ladder - the number goes down
6. Other stuff
	- DO NOT mess with `Time.timeScale`
	- Pls don't touch other ppl's things
	- Once again, DO NOT MESS WITH `GameStateManager`

for more nitty gritty stuff [visit here](https://docs.google.com/presentation/d/1vIjF57sc7jkjwu0rifCEWk9eGBuCtRijw-2tifI1-xg/edit?usp=sharing)
## what to keep in mind when making your game
1. Use at most WASD (and mouse controller)
2. Game should be **10 seconds** at most, and playable at **2.5x speed**
	- for all time-based and physics-based events/actions in you game, multiply by `Time.deltaTime` or `Time.fixedDeltaTime` (in `FixedUpdate()`)
	- for testing, you can incorporate your own timeScaling variable - make sure this variable is set to `1f` upon committing
	- ex. (in `FixedUpdate()`) `timer -= Time.fixedDeltaTime * m_personalTimeScaler`
4. Cutscenes should be at max 5 seconds
5. When the game time eventually speeds up, your game duration will go to about 1/4 of whatever original time you had
