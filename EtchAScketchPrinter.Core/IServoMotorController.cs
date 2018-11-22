namespace PQ.EtchASketchPrinter.Core
{
    public interface IServoMotorController
    {
        int Resolution { get; set; }
        void RotateBy(int signaledDuration);
    }
}