using CookPostcode.Models;
using CookPostcode.Services;
using CookPostcode.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CookPostcodeTests
{
    public class PostcodeLookupServiceTests
    {
        private List<PostcodeDelivery> listOfPostcode = new List<PostcodeDelivery>();
        private Mock<IPostcodeCleanupService> postcodeCleanupService;
        private PostcodeLookupService service;

        public PostcodeLookupServiceTests()
        {
            postcodeCleanupService = new Mock<IPostcodeCleanupService>();
            service = new PostcodeLookupService(postcodeCleanupService.Object);
        }

        [Test]
        public void NoMatchReturnsExpectedResults() { }

        [Test]
        public void LongestMatchGetsReturned() { }

        [Test]
        public void MatchesBasedOnStart() { }
    }
}