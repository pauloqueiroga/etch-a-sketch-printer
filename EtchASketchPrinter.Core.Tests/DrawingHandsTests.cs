using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PQ.EtchASketchPrinter.Core;

namespace EtchASketchPrinter.Core.Tests
{
    [TestClass]
    public class DrawingHandsTests
    {
        protected internal const int MaxPixels = 5000;
        protected internal const int MaxResolution = 100;
        protected internal const int MinResolution = MinPixels;
        protected internal const int MinPixels = 1;
        private readonly Random _random = new Random((int) DateTime.Now.Ticks);

        [TestMethod]
        public void ConstructorShouldSetProperResolutionValues()
        {
            // Arrange
            var verticalResolution = _random.Next(MinResolution, MaxResolution);
            var horizontalResolution = _random.Next(MinResolution, MaxResolution);
            var verticalHand = new InopServoController();
            var horizontalHand = new InopServoController();

            //Act
            var target = new DrawingHands(verticalHand, horizontalHand, verticalResolution, horizontalResolution);

            //Assert
            Assert.AreEqual(verticalResolution, target.VerticalResolution);
            Assert.AreEqual(horizontalResolution, target.HorizontalResolution);
        }

        [TestMethod]
        public void MoveUpShouldUpdateCurrentPositionY()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveUp(amount);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y + amount, result.Y);
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void StraightLineUpShouldUpdateCurrentPositionY()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.StraightLine(0, amount);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y + amount, result.Y);
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void MoveDownShouldUpdateCurrentPositionY()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveDown(amount);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y - amount, result.Y);
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void StraightLineDownShouldUpdateCurrentPositionY()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.StraightLine(0, -amount);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y - amount, result.Y);
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void StraightLineRightShouldUpdateCurrentPositionX()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.StraightLine(amount, 0);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X + amount, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void MoveRightShouldUpdateCurrentPositionX()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveRight(amount);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X + amount, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void MoveLeftShouldUpdateCurrentPositionY()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveLeft(amount);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X - amount, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void StraightLineLeftShouldUpdateCurrentPositionY()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.StraightLine(-amount, 0);
            var resultingPosition = target.CurrentPosition;

            //Assert
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X - amount, result.X);
            Assert.AreEqual(result.Y, resultingPosition.Y);
            Assert.AreEqual(result.X, resultingPosition.X);
        }

        [TestMethod]
        public void MoveUpShouldActivateVerticalServoClockwise()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var verticalHand = new InopServoController();
            var horizontalHand = new InopServoController();
            var target = new DrawingHands(horizontalHand, verticalHand);

            // Act
            var result = target.MoveUp(amount);

            //Assert
            Assert.AreEqual(amount * verticalHand.Resolution, verticalHand.AccumulatedClockwiseDuration);
            Assert.AreEqual(0, verticalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(0, horizontalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(0, horizontalHand.AccumulatedClockwiseDuration);
        }

        [TestMethod]
        public void MoveDownShouldActivateVerticalServoCounterCounterClockwise()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var verticalHand = new InopServoController();
            var horizontalHand = new InopServoController();
            var target = new DrawingHands(horizontalHand, verticalHand);

            // Act
            var result = target.MoveDown(amount);

            //Assert
            Assert.AreEqual(0, verticalHand.AccumulatedClockwiseDuration);
            Assert.AreEqual(amount * verticalHand.Resolution, verticalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(0, horizontalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(0, horizontalHand.AccumulatedClockwiseDuration);
        }

        [TestMethod]
        public void MoveRightShouldActivateVerticalServoClockwise()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var verticalHand = new InopServoController();
            var horizontalHand = new InopServoController();
            var target = new DrawingHands(horizontalHand, verticalHand);

            // Act
            var result = target.MoveRight(amount);

            //Assert
            Assert.AreEqual(0, verticalHand.AccumulatedClockwiseDuration);
            Assert.AreEqual(0, verticalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(0, horizontalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(amount * horizontalHand.Resolution, horizontalHand.AccumulatedClockwiseDuration);
        }

        [TestMethod]
        public void MoveLeftShouldActivateHorizontalServoCounterCounterClockwise()
        {
            // Arrange
            var amount = _random.Next(MinPixels, MaxPixels);
            var verticalHand = new InopServoController();
            var horizontalHand = new InopServoController();
            var target = new DrawingHands(horizontalHand, verticalHand);

            // Act
            var result = target.MoveLeft(amount);

            //Assert
            Assert.AreEqual(0, verticalHand.AccumulatedClockwiseDuration);
            Assert.AreEqual(0, verticalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(amount * horizontalHand.Resolution, horizontalHand.AccumulatedCounterClockwiseDuration);
            Assert.AreEqual(0, horizontalHand.AccumulatedClockwiseDuration);
        }

        [TestMethod]
        public void MoveUpShouldNotDoAnythingForZeroPixels()
        {
            // Arrange
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveUp(0);
            var resultingPosition = target.CurrentPosition;

            // Assert
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X, resultingPosition.X);
            Assert.AreEqual(origin.Y, resultingPosition.Y);
        }

        [TestMethod]
        public void MoveDownShouldNotDoAnythingForZeroPixels()
        {
            // Arrange
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveDown(0);
            var resultingPosition = target.CurrentPosition;

            // Assert
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X, resultingPosition.X);
            Assert.AreEqual(origin.Y, resultingPosition.Y);
        }

        [TestMethod]
        public void MoveRightShouldNotDoAnythingForZeroPixels()
        {
            // Arrange
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveRight(0);
            var resultingPosition = target.CurrentPosition;

            // Assert
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X, resultingPosition.X);
            Assert.AreEqual(origin.Y, resultingPosition.Y);
        }

        [TestMethod]
        public void MoveLeftShouldNotDoAnythingForZeroPixels()
        {
            // Arrange
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.MoveLeft(0);
            var resultingPosition = target.CurrentPosition;

            // Assert
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X, resultingPosition.X);
            Assert.AreEqual(origin.Y, resultingPosition.Y);
        }

        [TestMethod]
        public void StraightLineShouldNotDoAnythingForZeroPixels()
        {
            // Arrange
            var target = CreateRandomTarget();
            var origin = target.CurrentPosition;

            // Act
            var result = target.StraightLine(0, 0);
            var resultingPosition = target.CurrentPosition;

            // Assert
            Assert.AreEqual(origin.X, result.X);
            Assert.AreEqual(origin.Y, result.Y);
            Assert.AreEqual(origin.X, resultingPosition.X);
            Assert.AreEqual(origin.Y, resultingPosition.Y);
        }

        //TODO: Move* should not take negative numbers
        //TODO: StraightLine 10, 15, 30, 45, 60, 75 degrees

        private DrawingHands CreateRandomTarget()
        {
            var verticalResolution = _random.Next(MinResolution, MaxResolution);
            var horizontalResolution = _random.Next(MinResolution, MaxResolution);
            var verticalHand = new InopServoController();
            var horizontalHand = new InopServoController();
            return new DrawingHands(verticalHand, horizontalHand, verticalResolution, horizontalResolution);
        }
    }
}
