using System;

namespace PQ.EtchASketchPrinter.Core
{
    public class DrawingHands
    {
        private readonly Point _currentPosition;
        private const int DefaultResolution = 10;

        public IServoMotorController VerticalHand { get; }
        public IServoMotorController HorizontalHand { get; }
        public int VerticalResolution => VerticalHand?.Resolution ?? 0;
        public int HorizontalResolution => HorizontalHand?.Resolution ?? 0;
        public Point CurrentPosition => new Point(_currentPosition);

        public DrawingHands(
            IServoMotorController verticalHand,
            IServoMotorController horizontalHand,
            int verticalResolution,
            int horizontalResolution)
        {
            VerticalHand = verticalHand;
            HorizontalHand = horizontalHand;
            VerticalHand.Resolution = verticalResolution;
            HorizontalHand.Resolution = horizontalResolution;
            _currentPosition = new Point(0, 0);
        }

        public DrawingHands(IServoMotorController horizontalHand, IServoMotorController verticalHand) 
            : this(verticalHand, horizontalHand, DefaultResolution, DefaultResolution)
        {
        }

        public Point MoveVertically(int pixels)
        {
            VerticalHand.RotateBy(pixels * VerticalResolution);
            return UpdatePosition(0, pixels);
        }

        public Point MoveUp(int pixels)
        {
            ValidatePositiveValue(pixels);
            return MoveVertically(pixels);
        }

        public Point MoveDown(int pixels)
        {
            ValidatePositiveValue(pixels);
            return MoveVertically(-pixels);
        }

        public Point MoveHorizontally(int pixels)
        {
            HorizontalHand.RotateBy(pixels * HorizontalResolution);
            return UpdatePosition(pixels, 0);
        }

        public Point MoveLeft(int pixels)
        {
            ValidatePositiveValue(pixels);
            return MoveHorizontally(-pixels);
        }

        public Point MoveRight(int pixels)
        {
            ValidatePositiveValue(pixels);
            return MoveHorizontally(pixels);
        }

        public Point StraightLine(int horizontalOffset, int verticalOffset)
        {
            if (horizontalOffset == 0)
            {
                return MoveVertically(verticalOffset);
            }

            if (verticalOffset == 0)
            {
                return MoveHorizontally(horizontalOffset);
            }

            var horizontalDistance = Math.Abs(horizontalOffset);
            var verticalDistance = Math.Abs(verticalOffset);

            if (horizontalDistance == verticalDistance)
            {
                return MoveBoth(horizontalOffset, verticalOffset);
            }

            var steps = Math.Max(horizontalDistance, verticalDistance);
            var milestones = Math.Min(horizontalDistance, verticalDistance);
            var slowPace = (float) milestones / steps;

            // start assuming horizontal is the slow slow...
            var slowHand = HorizontalHand;
            var fastHand = VerticalHand;
            var slowDirection = horizontalOffset / horizontalDistance;
            var fastDirection = verticalOffset / verticalDistance;

            // ... swap fast and slow if horizontal turns up to be the fast one
            if (horizontalDistance != milestones)
            {
                slowHand = VerticalHand;
                fastHand = HorizontalHand;
                slowDirection = verticalOffset / verticalDistance; 
                fastDirection = horizontalOffset / horizontalDistance;
            }

            var fastAmount = fastDirection * fastHand.Resolution;
            var slowAmount = slowDirection * slowHand.Resolution;
            var lag = 0f;

            for (var step = 0; step < steps; step++)
            {
                fastHand.RotateBy(fastAmount);
                lag += slowPace;

                if (lag > step)
                {
                    continue;
                }

                slowHand.RotateBy(slowAmount);
                lag = step + 1;
            }

            return UpdatePosition(horizontalOffset, verticalOffset);
        }

        private Point MoveBoth(int horizontalOffset, int verticalOffset)
        {
            var horizontalDistance = Math.Abs(horizontalOffset);
            var verticalDistance = Math.Abs(verticalOffset);
            var horizontalAmount = horizontalOffset / horizontalDistance;
            var verticalAmount = verticalOffset / verticalDistance;

            while (horizontalDistance > 0 && verticalDistance>0)
            {
                if (horizontalDistance > 0)
                {
                    MoveHorizontally(horizontalAmount);
                    horizontalDistance--;
                }

                if (verticalDistance > 0)
                {
                    MoveVertically(verticalAmount);
                    verticalDistance--;
                }
            }

            return CurrentPosition;
        }

        private static void ValidatePositiveValue(int pixels)
        {
            if (pixels < 0)
            {
                throw new InvalidOperationException("Directional Move operations do not accept negative numbers");
            }
        }

        private Point UpdatePosition(int xOffset, int yOffset)
        {
            _currentPosition.X += xOffset;
            _currentPosition.Y += yOffset;
            return CurrentPosition;
        }
    }
}
