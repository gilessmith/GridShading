# Grid Shading

A program to solve grid shading puzzles ([nonograms](https://en.wikipedia.org/wiki/Nonogram)). An example grid shading puzzle can be found in the project as a file named GCHQ-grid-shading-puzzle.jpg.

## Algorithm approach	
The current solution generates all possible valid combinations of black & white squares for each row and column. It then 
recursively attempts to solve the grid, with each pass having extra known squares that are used as filters against the 
possible valid row / column options from step 1. If a pass fails to solve any squares then, then a guess is made setting 
the next unsolved square to black, and then continues to solve recursively as in the previous step. If an invalid situation 
is found, then the program unwinds until the last guess, and changes it's guess.