# CricketWorldCupTable
CricketWorldCupTable list teams position based on points. Team with highest points stays at top followed by teams with less points at second, third positions and so on.

Points are categorized base on win, draw, loss and no result.

| Match Result | Points  |
|--------------|---------|
| Win          |   2     |   
| Draw         |   1     |
| Loss         |   0     |
| No Result    |   0     |

Each team plays against other team once. The final tally of teams position is completed when each and every team plays with one another.

There are multiple cases to consider that determines the team position.

<u>Case 1:</u> 

| Team  | Played | Win | Draw | Points |
|-------|--------|-----|------|--------|
|   A   |   2    |  2  |   0  |   4    | 
|   B   |   3    |  1  |   2  |   4    |

Here, team A and B have same points.However, team A has played less number of matches than team B so, team A is ahead in position. Here, played number of matches has priority.

<u>Case 2:</u> 

| Team  | Played | Win | Draw | Loss | Points |
|-------|--------|-----|------|------|--------|
|   A   |   3    |  2  |   0  |   1  |    4   |
|   B   |   3    |  1  |   2  |   0  |    4   |

Here, team A and B both have same points and played same number of matches. However, team A is ahead in position because team A has 2 wins whereas team B has 1 win.Here, win has priority.

To do :
<u>Case 3:</u>
Scenario with same points, same number of played matches, same number of win, loss, draw and no result matches. Head to head match winner team has priority.



