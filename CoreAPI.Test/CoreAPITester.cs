using CoreAPI.Controllers;
using NUnit.Framework;

namespace CoreAPI.Test
{
    [TestFixture]
    public class NUnitTestProgram
    {
        [Test]
        public void ControllerTest()
        {
            string message = "Hello Word";
            MessageController controller = new MessageController();
            controller.Post(message);
            var result = controller.Get();

            Assert.AreEqual(result, message);
        }
    }
}
