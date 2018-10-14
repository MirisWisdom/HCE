using NUnit.Framework;

namespace Atarashii.Tests
{
    [TestFixture]
    public class OpenSauceTests
    {
        private readonly string result = new OpenSauce().ToXml();

        [Test]
        public void FromXml_PropertyValueIsCorrect_True()
        {
            var object01 = new OpenSauce
            {
                Camera = new Camera
                {
                    FieldOfView = 128.64
                }
            };

            var object02 = OpenSauceFactory.GetFromXml(object01.ToXml());
            Assert.AreEqual(object02.Camera.FieldOfView, object01.Camera.FieldOfView);
        }

        [Test]
        public void ToXml_BooleansAreCorrect_True()
        {
            StringAssert.Contains("<DepthFade>true</DepthFade>", result);
        }

        [Test]
        public void ToXml_NumbersAreCorrect_True()
        {
            StringAssert.Contains("<FieldOfView>70</FieldOfView>", result);
        }
    }
}