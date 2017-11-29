using BoDi;
using TechTalk.SpecFlow;
using Zukini.API.Steps;

namespace UnitTestProject1
{
    [Binding]
    public class UnitTest1 : ApiSteps
    {
        public UnitTest1(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public void TestMethod1()
        {
        }
    }
}
