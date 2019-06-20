using System.Collections.Generic;

namespace PQ.EtchASketchPrinter.Core
{
    public class Sketch
    {
        public Sketch()
        {
            Steps = new List<Sketch>();
        }

        public virtual string Name { get; set; }
        public ICollection<Sketch> Steps { get; }

        public virtual IEnumerable<string> Execute(DrawingHands hands)
        {
            var steps = new List<string>();

            foreach (var step in Steps)
            {
                steps.AddRange(step.Execute(hands));
            }

            return steps;
        }
    }
}
