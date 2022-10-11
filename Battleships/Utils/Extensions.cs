using Battleship;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Utils
{
    internal static class Extensions
    {
        public static IEnumerable<GameCell> DistinctGameCells(this IEnumerable<GameCell> cells)
        {
            return cells.GroupBy(c => new { c.X, c.Y}).Select(g => 
            {
                var collisionNotAllowed = g.FirstOrDefault(c => c.CollisionIsNotAllowed);
                if (collisionNotAllowed == null)
                    return g.First();
                return collisionNotAllowed;
            }).ToList();
        }

        public static bool IsCollision(this IEnumerable<GameCell> cells)
        {
            var sameCells = cells.GroupBy(c => new { c.X, c.Y });
            foreach(var sc in sameCells)
            {
                var collisionNotAllowed = sc.FirstOrDefault(c => c.CollisionIsNotAllowed);

                if (collisionNotAllowed != null && sc.Count() > 1)
                    return true;
            }
            return false;
        }
    }
}
