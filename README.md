# Bowling
This is an REST API that emulates a bowling game, Following an Onion Architecture.

## How to Play
Open the Url bellow, it will open an Swagger page.

```https://localhost:7121/swagger/```

### Create a new Game
To create a new Game move to the Game Section until the POST endpoint and insert this Json into the body section.

```
{
  "id": 0,
  "currentFrame": 0,
  "player": {
    "id": 0,
    "name": "Henry"
  }
}
```
You will recieve a notification of ***Game created succesfully***

### Start to roll points
To Start to roll points navigate into the Swagger page until the ***BowlingManager*** section and look for the POST endpoint and insert this Json into the body section.

```
{
  "pinsDown": "X",
  "gameId": 1
}
```
***Replace the gameId and pinsDown with your data***

We accept we follow characters as point!
```
1, 2, 3, 4, 5, 6, 7, 8, 9, X, /, -
```

- 'X' Are 10 points and it's an ***Strike!***
- '/' Are the rest of point of the Frame ***Spare.***
- '-' Is 0 points.

### Get your Score (Not implemented yet)
To Get your Score go to the section of Score and insert this JSON text into the route.

```
gameId = {your game Id}
```

Also feel free to explore the rest of endpoints.

Enjoy!