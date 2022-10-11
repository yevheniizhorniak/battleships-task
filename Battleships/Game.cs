using Battleship;
using Battleships.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    // Imagine a game of battleships.
    //   The player has to guess the location of the opponent's 'ships' on a 10x10 grid
    //   Ships are one unit wide and 2-4 units long, they may be placed vertically or horizontally
    //   The player asks if a given co-ordinate is a hit or a miss
    //   Once all cells representing a ship are hit - that ship is sunk.
    public class Game
    {
        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses
        public static int Play(string[] ships, string[] guesses)
        {
            var celledShips = GetShipsFromString(ships);
            var cellGuesses = GetGuessesFromString(guesses);

            if (!ValidateCollisions(celledShips))
                throw new ArgumentException(ExceptionMessages.ShipsCollision);

            foreach (var guess in cellGuesses)
            {
                foreach (var ship in celledShips)
                {
                    foreach (var cell in ship.Cells)
                    {
                        if (cell.IsSame(guess))
                        {
                            cell.Shot = true;
                        }
                    }
                }
            }

            return celledShips.Where(s => s.IsDestroyed()).Count();
        }

        private static bool ValidateCollisions(IEnumerable<Ship> ships)
        {
            var cells = ships.SelectMany(s => s.GetRestrictedCells()).ToList();

            return !cells.IsCollision();
        }

        private static IEnumerable<Ship> GetShipsFromString(string[] ships)
        {
            return ships.Select(s => new Ship(s)).ToList();
        }

        private static IEnumerable<GameCell> GetGuessesFromString(string[] guesses)
        {
            return guesses.Select(g => new GameCell(g)).ToList();
        }
    }
}
