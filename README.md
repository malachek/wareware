
# wareware
this is a summary of how to not fuck up the project for us programmers!\
includes general guidelines, what to do, and what NOT to do

1. **DO NOT TOUCH `GameStateManager`**
	- If you touch it, shit will break.
	- If you REALLY need to touch it, contact `@malachek` (probably on discord)
2. **For organization's sake:**
	- Each game is its own scene - place ALL SCENES in the scenes folder
	- For each game, make a separate folder which will store everything related to that game - whether that be art, scripts, music/sounds, etc.
3. You are able to test your game via running them on their own scene
4. **If you need to add your game to the main game, contact `@malachek`**
	- Implementation itself involves touching `GameStateManager`, which you prob shouldn't touch
5. **NAMING CONVENTIONS**:
	- For anything related to your own game, nobody ain't gon give a shit just do what u want
		- just make sure like ur name don't conflict with anyone else's name
	- **BUT** if your game is a series game (as in, playing a game requires the completion of another game):
		- Each game scene name must have the same name, followed by a number that tells what comes next
		- xxxx1 is the last game of the series, xxxx2 is played before xxxx1, and xxxx3 plays before xxxx2.
			- DO NOT go past xxxx9!
		- EXAMPLE
			- If you must play "game 1" before "game 2", game 1 should be named something like `Game2`, and game 2 should be named `Game1`
			- It's kinda like a ladder - the number goes down
6. Other stuff
	- DO NOT mess with `Time.timeScale`
	- Pls don't touch other ppl's crap
	- Once again, DO NOT MESS WITH `GameStateManager`

for more nitty gritty stuff [visit here](https://docs.google.com/presentation/d/1vIjF57sc7jkjwu0rifCEWk9eGBuCtRijw-2tifI1-xg/edit?usp=sharing)
## what to keep in mind when making your game
1. Use at most 4 buttons (and mouse controller)
2. Game should be 10 seconds at most
3. Cutscenes should be at max 5 seconds
4. Game should be simple enough for anyone to complete in 10 seconds
5. When the game time eventually speeds up, your game duration will go to about 1/4 of whatever original time you had
	- So if you have movable characters in the game, you probably need to scale that. (i honestly have no idea what this means either so like ask mary or smth? (and update this readme after with the correct info))
