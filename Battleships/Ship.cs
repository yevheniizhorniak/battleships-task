using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Battleships;
using Battleships.Utils;

namespace Battleship
{
    public class Ship
    {
        public List<GameCell> Cells = new List<GameCell>();

        private void ThrowArgumentException(string ship, string message = "")
        {
            throw new ArgumentException("Invalid ship input: " + ship + Environment.NewLine + message);
        }
        //"3:2,3:5"
        public Ship(string ship)
        {
            if (!Regex.Match(ship, "\\d:\\d").Success && !Regex.Match(ship, "\\d:\\d,\\d:\\d").Success)
                ThrowArgumentException(ship);


            if (ship.Length == 3)
            {
                Cells.Add(new GameCell(ship));
            }
            else
            {
                // Add diagonal and size validation
                var coords = ship.Split(',');

                var c1 = new GameCell(coords[0]);
                var c2 = new GameCell(coords[1]);

                if (c1.IsSame(c2))
                {
                    // Special case, it can be just suppressed with warning
                    ThrowArgumentException(ship, ExceptionMessages.ShipWithTheSameCoords);
                }

                Cells.Add(c1);
                Cells.Add(c2);

                if (c1.X == c2.X)
                {
                    if (c1.Y > c2.Y && c1.Y - c2.Y <= 3)
                    {
                        for (int i = c2.Y + 1; i < c1.Y; i++)
                        {
                            Cells.Add(new GameCell(c1.X, i));
                        }
                    }
                    else if (c1.Y < c2.Y && c2.Y - c1.Y <= 3)
                    {
                        for (int i = c1.Y + 1; i < c2.Y; i++)
                        {
                            Cells.Add(new GameCell(c1.X, i));
                        }
                    }
                    else
                        throw new ArgumentOutOfRangeException(ExceptionMessages.LenghtOfShipIsOutOfRange + ship); // Lenght of ship is more than 4 
                }
                else if (c1.Y == c2.Y)
                {
                    if (c1.X > c2.X && c1.X - c2.X <= 3)
                    {
                        for (int i = c2.X + 1; i < c1.X; i++)
                        {
                            Cells.Add(new GameCell(c1.X, i));
                        }
                    }
                    else if (c1.X < c2.X && c2.X - c1.X <= 3)
                    {
                        for (int i = c1.X + 1; i < c2.X; i++)
                        {
                            Cells.Add(new GameCell(i, c1.Y));
                        }
                    }
                    else
                        throw new ArgumentOutOfRangeException(ExceptionMessages.LenghtOfShipIsOutOfRange + ship); // Lenght of ship is more than 4 
                }
                else
                {
                    ThrowArgumentException(ship, ExceptionMessages.DiagonalShip); // diagonal ship
                }
            }
        }

        public bool IsPart(GameCell point)
        {
            return Cells.FirstOrDefault(p => p.IsSame(point)) != null;
        }

        public IEnumerable<GameCell> GetRestrictedCells()
        {
            return Cells.SelectMany(p => p.GetRestrictedPoints()).DistinctGameCells().ToList();
        }

        public bool IsDestroyed()
        {
            return Cells.Where(p => !p.Shot).Count() == 0;
        }
    }
}
