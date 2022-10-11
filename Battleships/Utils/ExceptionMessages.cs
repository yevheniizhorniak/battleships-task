using System;

namespace Battleships
{
    public static class ExceptionMessages
    {

        public const string ShipsCollision = "Ships can't be closer to each other that one cell";


        public const string CellIndexIsOutOfRange = "Coordinate can't be less than 0 or more than 9";
        public const string InvalidCellFormat = "Cell have to be in \'1:2\' format";


        public const string ShipWithTheSameCoords = "Two coordinates of ship can't be the same.";

        public const string LenghtOfShipIsOutOfRange = "Lenght of ship is out of range (1-4): ";

        public static string DiagonalShip = "Ship can't be diagonal.";
    }
}
