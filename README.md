# Valorant Custom Game Tracker

## HTTP-5204 Passion Project

Valorant Custom Game Tracker is designed to help valorant players keep track of the custom games that they play.

Users can:

### Team Database Table
-View all teams, and details about a specific team (eg. which players are on that team)\
-Create a new team\
-Update an existing team's name. (updating players on team functionality is not complete)\
-Delete a team

### Player Database Table
-View all players, and details about a specific player\
-Create a new player\
-Update an existing player's information.\
-Delete a player

## Challenges Faced
- So much to do, so little time
- Getting the Many-to-Many relationship between the Team table and Player table was trickier than expected
- Unable to get the M-to-M relationship working for update functionality on Team Table

## Functionality to Add
- Update functionality for Team table to update players on each team.
- Games & Maps databases
- CRUD for Game & Maps
- Views to display a list of games, the associated teams, the score of the game, and the map
- Login Feature
- Filter for games
- More....


## References
To make this web application, I referenced Christine Bittle's [Varsity Project MVP](https://github.com/christinebittle/varsity_mvp). It was invaluable in providing a structure to set up my project. I also used Christine Bittle's [Many-To-Many example](https://github.com/christinebittle/EF_Many_Many_Explicit) to add a many-to-many relationship to my players and teams tables.
