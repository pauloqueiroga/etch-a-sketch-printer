using PQ.EtchASketchPrinter.Core;

namespace EtchASketchPrinter.Core.Tests
{
    public class InopServoController : IServoMotorController
    {
        public int AccumulatedClockwiseDuration { get; set; }
        public int AccumulatedCounterClockwiseDuration { get; set; }
        public int Resolution { get; set; }

        private void RotateClockwise(int duration)
        {
            AccumulatedClockwiseDuration += duration;
        }

        private void RotateCounterClockwise(int duration)
        {
            AccumulatedCounterClockwiseDuration += duration;
        }

        public void RotateBy(int signaledDuration)
        {
            if (signaledDuration >= 0)
            {
                RotateClockwise(signaledDuration);
            }
            else
            {
                RotateCounterClockwise(-signaledDuration);
            }
        }
    }
}