using Battleships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Battleship
{
    public class GameCell
    {
        public int X { get; }
        public int Y { get; }
        public bool Shot { get; set; } = false;
        public bool CollisionIsNotAllowed { get; set; } = true; // For + 1/ -1 cells is false 

        public GameCell(int x, int y, bool collisionIsNotAllowed = false)
        {
            X = x;
            Y = y;
            CollisionIsNotAllowed = collisionIsNotAllowed;

            ValidateCell();
        }
        public GameCell(string cell)
        {
            if (!Regex.Match(cell, "\\d:\\d").Success)
                throw new FormatException();

            var xy = cell.Split(':');

            X = Convert.ToInt32(xy[0]);
            Y = Convert.ToInt32(xy[1]);

            ValidateCell();
        }

        private void ValidateCell()
        {
            if (X > 9 || Y > 9 || X < 0 || Y < 0)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.CellIndexIsOutOfRange);
            }
        }

        public bool IsSame(GameCell p)
        {
            return X == p.X && p.Y == Y;
        }

        public IEnumerable<GameCell> GetRestrictedPoints()
        {
            var result = new List<GameCell>
            {
                GetGameCellForRestrictedPoints(X, Y, true),
                GetGameCellForRestrictedPoints(X + 1, Y),
                GetGameCellForRestrictedPoints(X - 1, Y),
                GetGameCellForRestrictedPoints(X, Y + 1),
                GetGameCellForRestrictedPoints(X, Y - 1),
                GetGameCellForRestrictedPoints(X + 1, Y + 1),
                GetGameCellForRestrictedPoints(X - 1, Y + 1),
                GetGameCellForRestrictedPoints(X + 1, Y - 1),
                GetGameCellForRestrictedPoints(X - 1, Y - 1),
            };

            return result.Where(gc => gc != null).ToList();
        }

        private GameCell GetGameCellForRestrictedPoints(int x, int y, bool collisionIsNotAllowed = false)
        {
            try
            {
                return new GameCell(x, y, collisionIsNotAllowed);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }
}
