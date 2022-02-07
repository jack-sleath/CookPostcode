using CookPostcode.Services;
using NUnit.Framework;

namespace CookPostcodeTests
{
    public class PostcodeCleanupServiceTests
    {
        private PostcodeCleanupService service;

        public PostcodeCleanupServiceTests()
        {
            service = new PostcodeCleanupService();
        }

        [Test]
        [TestCase("BAD CODE", false)]
        [TestCase("BAD POST CODE", false)]
        [TestCase("TN 34 2 GT", false)]
        [TestCase("TN 342GT", false)]
        [TestCase("TN34 2GT", true)]
        [TestCase("TN1 5BG", true)]
        [TestCase("TN15BG", true)]
        public void PostcodeValidationTests(string postcodeInput, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, service.IsValidPostcode(postcodeInput));
        }

        [Test]
        [TestCase("test input", "TEST INPUT")]
        [TestCase("test input   ", "TEST INPUT")]
        [TestCase("    test input", "TEST INPUT")]
        [TestCase("    test input    ", "TEST INPUT")]
        public void PostcodeCleanupTests(string postcodeInput, string expectedOutput)
        {
            Assert.AreEqual(expectedOutput, service.CleanPostcode(postcodeInput));
        }
    }
}
