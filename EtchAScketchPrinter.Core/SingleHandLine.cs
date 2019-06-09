using System.Collections.Generic;

namespace PQ.EtchASketchPrinter.Core
{
    public abstract class SingleHandLine : Sketch
    {
        public override string Name => GetType().FullName;
        public int Pixels { get; set; }

        public sealed override IEnumerable<string> Execute(DrawingHands hands)
        {
            ExecuteActual(hands);
            yield return Name;
        }

        protected abstract void ExecuteActual(DrawingHands hands);
    }

    public class StraightLineUp : SingleHandLine
    {
        protected override void ExecuteActual(DrawingHands hands)
        {
            hands.MoveUp(Pixels);
        }
    }

    public class StraightLineDown : SingleHandLine
    {
        protected override void ExecuteActual(DrawingHands hands)
        {
            hands.MoveDown(Pixels);
        }
    }

    public class StraightLineRight : SingleHandLine
    {
        protected override void ExecuteActual(DrawingHands hands)
        {
            hands.MoveRight(Pixels);
        }
    }

    public class StraightLineLeft : SingleHandLine
    {
        protected override void ExecuteActual(DrawingHands hands)
        {
            hands.MoveLeft(Pixels);
        }
    }
}