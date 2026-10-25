# Tjugoett (Twenty-One)

A console Blackjack game in C# (.NET 10) for one to five players against the dealer. It was built by group 6 for course MAI26, Kunskapskontroll 3. The code is in English and everything the player sees is in Swedish.

Players can be humans or computers (with two different strategies). Results (wins, losses and pushes) are saved to a file, so returning players keep their statistics the next time the program starts.

## How to start

Requirements: the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/WatermelonQuokka/maima26-grupp6-21
cd maima26-grupp6-21
dotnet run
```

Run the program from the repository root so that `rules.txt` is found.

### How a game goes
1. Choose the number of players (1-5).
2. For each seat choose human (`m`) or computer (`d`).
   - Humans enter a name. Names must be unique (case-insensitive). A name that is already in the save file gets its saved statistics back.
   - Computers are called "Dator 1", "Dator 2", ... and you choose their strategy: simple (`s`) or basic strategy (`g`).
3. Every round each player gets two open cards and the dealer gets one open card and one hidden card (the hole card).
4. On their turn a human chooses hit (`k`, "köpa") or stand (`s`, "stanna"). A hand over 21 is bust.
5. The dealer reveals the hole card and draws while the hand is 16 or lower (see `DealerHitsOnSoft17` for soft 17).
6. The result for each player is shown, followed by a statistics table. Answer `j` to play another round or `n` to quit.

Wrong input never crashes the program, the player is asked again. If input ends (Ctrl+Z), the program prints a message and exits.

## Class overview

`Program` starts a `Game`, and `Game` owns everything that lives for a whole session: the `RuleSet` (read from `rules.txt`), the `Deck`, the `Dealer`, and the players. Every round `Game` creates a `Round`.

```
Program -> Game -> Round -> RoundEvaluator
             |        |
             |        +-> Player.MoveDecider (IMoveDecider) -> hit or stand
             |        +-> Deck, Dealer, Hand
             +-> GameStorage (load and save players)

Everything that is printed or read goes through ConsoleUi
(which uses CardRenderer to draw cards).
```

| Folder | Classes | Responsibility |
|---|---|---|
| `Cards/` | `Card`, `Rank`, `Suit`, `Hand`, `Deck` | A card, a hand that can calculate its own value (soft/hard, bust, natural), and a deck/shoe that is shuffled with Fisher-Yates and rebuilt when few cards are left |
| `Participants/` | `Participant`, `Player`, `Dealer` | `Participant` has a name and a hand. `Player` also has a win/loss/push count and an `IMoveDecider`. `Dealer` has the hole card and follows the fixed dealer rule |
| `Deciders/` | `IMoveDecider`, `HumanMoveDecider`, `SimpleMoveDecider`, `BasicStrategyMoveDecider`, `ComputerStrategy` | Decides hit or stand for a player. The same call gives different behaviour (human input, simple rule, basic strategy table) |
| `Gameplay/` | `Game`, `Round`, `RoundEvaluator`, `RoundResult`, `Move`, `RuleSet` | The session loop, one round, the decision of who wins, and the house rules |
| `Graphics/` | `CardRenderer` | Returns the lines (`string[]`) of one card, face up or face down, and never prints |
| `Storage/` | `GameStorage`, `PlayerRecord` | Saves and loads players as JSON in `Data/PlayersData.json` (`Data` is in `.gitignore`) |
| `UI/` | `ConsoleUi` | All input and output: prompts, validation, tables, colours |

Design decisions:
- **No Console in the rule classes.** `Card`, `Deck`, `Hand`, `Participant`, `Player`, `Dealer`, `Round` and the rest of the game logic never print or read. Only `ConsoleUi` talks to the user.
- **Who decides a player's move?** `Player` holds an `IMoveDecider`, so a human and the computer players play through exactly the same `Round` code.
- **The dealer is a `Participant`, not a `Player`.** They share a name and a hand, but the dealer has no choice, only a fixed rule.
- **`RoundEvaluator`** compares the hands and the `RuleSet` and returns a `RoundResult` (Win, Lose, Push, Blackjack), so all result rules are in one place.
- **`Game` keeps two lists**, `_savedPlayers` (everyone in the save file) and `_players` (this session's players), so a saved player who sits out a session is not removed from the file.


## Who did what

| Member (GitHub) | Optional feature (tillval) | Class(es) | Other parts |
|---|---|---|---|
| Ahmad AlSibai (noxsaid) | 1. Card graphics in the console | `CardRenderer`, `ConsoleUi.ShowCards` | Input helpers, `RoundEvaluator`, `Hand.IsSoft`, statistics, `GameStorage` |
| Daniel Chis (WatermelonQuokka) | 2. Computer players with strategy | `SimpleMoveDecider`, `BasicStrategyMoveDecider`, `ComputerStrategy` | `IMoveDecider`, `HumanMoveDecider`, `Card`, `Deck`, `Participant`, `Player`, `Dealer`, `Game`, `Round`, the hit/stand turn |
| _name_ | _tillval_ | _classes_ | _fill in_ |

Tillval 1 (card graphics): a card is drawn as a framed box with the value in two corners and the suit in the middle. The dealer's hole card is drawn face down (▒) and the cards of a hand are shown side by side. `CardRenderer` only returns the lines of one card, and `ConsoleUi` weaves them together and sets the colour.

Tillval 2 (computer players with strategy): `IMoveDecider` with `SimpleMoveDecider` (hit below 17) and `BasicStrategyMoveDecider` (looks up hit or stand in a basic strategy table for hard and soft hands against the dealer's up card).

Tillval 3 (house rules from a file): `RuleSet` is read from `rules.txt` at start-up and used by `Game`, `Round`, `Dealer`, `Deck` and `RoundEvaluator`.

## Rules

The rules follow the assignment. The game is played with these details and deviations:

- **Natural 21** (ace plus a ten-value card as the first two cards) beats a 21 made of three or more cards, and natural against natural is a push. A player with a natural does not get a turn (no hit).
- **The dealer** reveals the hole card, hits on 16 or lower and stands on 17 or higher. The dealer never looks at the hole card in advance, and the round is always played to the end.
- **Statistics:** a natural counts as a win. The table after each round only shows this session's players.
- **Deviations from the base version in the assignment:**
  - **Reshuffle threshold:** the assignment says to reshuffle before a round when fewer than 15 cards remain. With 4–5 players, a round can use more than that, so the deck could run out mid-round. The assignment also says to only reshuffle between rounds and that the deck must never run out, and all three can't hold at once. We therefore reshuffle when fewer than (players + 1) × 6 cards remain, or fewer than ReshuffleThreshold, whichever is higher, still only between rounds. A simulation (2000 sessions × 200 rounds, 1–5 players, 1 and 6 decks) never ran out. Before the fix, 5 players ran out in every session.
  - The default `rules.txt` uses **6 decks**, the base version has one deck of 52 cards.
  - Clubs and spades are drawn in **grey** instead of black, because black text is hard to read on a dark console.
- Computer players are not saved to the file, only human players.


### `rules.txt`

The house rules are read from `rules.txt` in the repository root (one `Key = value` per line):

| Key | Value in the file | Meaning |
|---|---|---|
| `DealerHitsOnSoft17` | `false` | If `true`, the dealer draws on a soft 17 |
| `NumberOfDecks` | `6` | Number of decks in the shoe (more than 0) |
| `ReshuffleThreshold` | `15` | Number of cards left in the shoe. If fewer remain at the start of a round, or fewer than (players + 1) × 6, whichever is higher, the whole shoe (all decks) is rebuilt and shuffled. This is never done in the middle of a round. |
| `NaturalBlackjackBeatsThreeCard21` | `true` | If `false`, a natural against a 21 of three or more cards is a push |

If the file is missing, the game starts with default rules and tells the user. An invalid value gives a message and a default is used for that setting.

## Known limitations

- `GameStorage.Save` has no error handling for file errors (for example a locked file), and `GameStorage.Load` only handles `JsonException`.
- Two commits (explicit types in `GameStorage` and the call to `ShowStatistics`) show the author "trial" because of a mistake in Ahmad's Git configuration. They are his.
- If `ReshuffleThreshold` in `rules.txt` is lower than the table needs, it's raised without telling the user.
- 6 cards per hand is a practical limit verified by simulation, not a guarantee: a hand can, very rarely, use more.