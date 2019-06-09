using PQ.EtchASketchPrinter.Core;

namespace EtchASketchPrinter.Core.Tests
{
    public class InopServoController : IServoMotorController
    {
        public int AccumulatedClockwiseDuration { get; set; }
        public int AccumulatedCounterClockwiseDuration { get; set; }
        public int Resolution { get; set; }
        public int ClockwiseActivations { get; private set; }
        public int CounterClockwiseActivations { get; private set; }

        private void RotateClockwise(int duration)
        {
            ClockwiseActivations++;
            AccumulatedClockwiseDuration += duration;
        }


        private void RotateCounterClockwise(int duration)
        {
            CounterClockwiseActivations++;
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