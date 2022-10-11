using System;
using FluentAssertions;
using Xunit;

namespace Battleships.Test
{
    public class GameTest
    {
        [Fact]
        public void TestPlay()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "7:0", "3:3" };
            Game.Play(ships, guesses).Should().Be(0);
        }

        [Fact]
        public void TestInvalidInputShips()
        {
            var ships = new[] { "invalid data" };
            var guesses = new[] { "7:0", "3:3" };

            Assert.Throws<ArgumentException>(() => Game.Play(ships, guesses));
        }

        // Spaces are not allowed
        [Fact]
        public void TestInvalidGuess()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "invalid data", "3:3" };

            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }

        [Fact]
        public void TestNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => Game.Play(null, null));
        }

        [Fact]
        public void TestShipWithOneCoordinate()
        {
            var ships = new[] { "2:1" };
            var guesses = new[] { "2:0", "2:1" };

            Game.Play(ships, guesses).Should().Be(1);
        }

        [Fact]
        public void TestDiagonalShip()
        {
            var ships = new[] { "1:5,3:7" };
            var guesses = new[] { "2:0", "2:1" };

            Assert.Throws<ArgumentException>(() => Game.Play(ships, guesses));
        }

        [Fact]
        public void TestDoubleGuessShip()
        {
            var ships = new[] { "2:1,2:3" };
            var guesses = new[] { "2:2", "2:2" };
            Game.Play(ships, guesses).Should().Be(0);
        }

        [Fact]
        public void TestOutOfRangeCoord()
        {
            var ships = new[] { "2:1,2:3" };
            var guesses = new[] { "2:10", "-1:2" };

            Assert.Throws<ArgumentOutOfRangeException>(() => Game.Play(ships, guesses));
        }

        [Fact]
        public void TestOutOfRangeShip()
        {
            var ships = new[] { "2:9,2:3" }; // Ships can't be more than 4 cells
            var guesses = new[] { "2:2", "2:2" };

            Assert.Throws<ArgumentOutOfRangeException>(() => Game.Play(ships, guesses));
        }

        // Diagonal collision is prohibited
        [Fact]
        public void TestShipsCollision()
        {
            var ships = new[] { "4:5,4:6", "2:7,3:7" };
            var guesses = new[] { "2:2", "2:2" };

            Assert.Throws<ArgumentException>(() => Game.Play(ships, guesses));
        }

        [Fact]
        public void TestShipsPartialDestroing()
        {
            var ships = new[] { "4:5,4:6", "2:9,2:7" };
            var guesses = new[] { "2:8", "4:5" };

            Game.Play(ships, guesses).Should().Be(0);
        }

        [Fact]
        public void TestComplexGame()
        {
            var ships = new[] { "1:1,4:1", "1:6,1:8", "9:0,9:2", "6:3,7:3", "4:5,4:6", "2:3", "6:6", "6:8", "9:9" };
            var guesses = new[] { "1:4", "6:6", "6:6", "4:5", "4:6", "1:7" };

            Game.Play(ships, guesses).Should().Be(2);
        }

    }
}
