using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PQ.EtchASketchPrinter.Core;

namespace EtchASketchPrinter.Core.Tests
{
    [TestClass]
    public class SketchTests
    {
        [TestMethod]
        public void SketchConstructorShouldPopulateProperties()
        {
            //Act
            var target = new Sketch();
            
            //Assert
            Assert.IsNotNull(target);
            Assert.IsNotNull(target.Steps);
            Assert.IsFalse(target.Steps.Any());
        }

        [TestMethod]
        public void ExecuteShouldRunEveryStep()
        {
            //Arrange
            var step1 = new Sketch {Name = "step1",};
            var step2 = new Sketch {Name = "step2",};
            var step21 = new Sketch {Name = "step2.1"};
            var step22 = new Sketch {Name = "step2.2"};
            var step3 = new Sketch {Name = "step3"};
            var target = new Sketch {Name = "steps1-3"};
            target.Steps.Add(step1);
            step2.Steps.Add(step21);

            //Act
            var target = new Sketch();
            
            //Assert
            Assert.IsNotNull(target);
            Assert.IsNotNull(target.Steps);
            Assert.IsFalse(target.Steps.Any());

        }
    }
}
