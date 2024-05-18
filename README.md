# OOP_ICT
This repository is an Object-Oriented-Programming project.

The idea of a project is to realize a casino, using different patterns, so let's look at each laboratory work in detail. For each laboratory work there's a folder with unittests for each method iv each class.

### OOP-ICT.First
Here are basic entities for creating a casino (no patterns at this point):
```
* Card (with used enums for value and suit)
* CardDeck (a basic deck that consist of 52 cards)
* Dealer - entity that has a hand (realization for BlackJack) and that has ability to:
  ^ shuffle deck
  ^ deal cards
  ^ put cards on table (poker realization)
```

### OOP-ICT.Second
At this point we start to create more interesting entities of casino and to use generating design patterns. 

New entities at this point:
```
* Bank (a model of a real bank) in which we can:
  ^ create a bank account - for this method was used Factory (pattern)
  ^ check if bank account exists by player id
  ^ deposit money
  ^ withdraw money
    * money is realized in class PayUnit to make it more interesting (not to use just a decimal)
  ^ get reward
  ^ check possibility to bet

* BlackjackCasino in which we can:
  ^ create a casino account - for this method was used Factory (pattern)
  ^ check if casino account exists by player id
  ^ buy chips (exchange money to chips) - for this method was used Facade (pattern)
  ^ check if it's possible to sell chips
  ^ sell chips (exchange chips to money) - for this method was used Facade (pattern)
  ^ pay winnings to players
  ^ charge loss to players
  ^ handle blackjack

* Player with which we can:
  ^ set a bank account
  ^ set a casino account
  ^ receive card (if dealer deals this card)
  ^ set best hand (method for evaluating winner in poker)
```

### OOP-ICT.Third
In this laboratory work was created a blackjack game.

New entities at this point:
```
* Blackjack game in which we can:
  ^ initialize all the needed entities (bank, casino, dealer)
  ^ add player to the game
  ^ deal cards to players
  ^ bet (each player has an opportunity to bet through the facade)
  ^ give more cards to players if needed to
  ^ calculate points of players and dealer
  ^ get winner
```

### OOP-ICT.Fourth
In this laboratory work was realized a poker game using new pack of patterns like chain of responsibilities.

New entities at this point:
```
* Card converter, that lets us to get an integer instead of a card value
* Combinations where we can:
  ^ get names of all the available in poker combinations
  ^ get integer-value of a combination by its name
  ^ get string-name of a combination by its value
  ^ get value of a combination that particular player has
  ^ get the best combination out of all the combinations that a player has
* Hand evaluator in which we have algorthms to evaluate each combination that exists in poker - all the methods are boolean (a player has a particular combination or not)
* Poker game in which we can:
  ^ get sum of bets that the players have made
  ^ get table (cards that are opened for all the players)
  ^ add player to game
  ^ deal cards to all the players
  ^ get blind bets
  ^ let player call the bet
  ^ let player fold their cards
  ^ lay cards on table like: three cards, then one more, then one more again
  ^ determine winner according to the highest combination among the players
```

### OOP-ICT.Fifth
In this laboratory work was realized a console-version of poker in "god mode" - you can add players yourself, deposit their bank accounts, buy chips, make bets. Also was realized a logging of all the choices for all the players and saving the results of games to database. For database was used PostgreSQL and Entity-Framework for interaction with database. For console version was used Spectre.Console library, so there is a "UI" for every "choice window". Also for more effective work was used a CQRS pattern.


