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
        public void NoMatchReturnsExpectedResults()
        {
            var enteredPostcode = "NOMATCH";
            postcodeCleanupService.Setup(x => x.CleanPostcode(It.IsAny<string>())).Returns(enteredPostcode);
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "AAAAAA", Delivery = "Test Message" });

            var result = service.GetValidDeliveryOptions(enteredPostcode, listOfPostcode);
            Assert.AreEqual("Delivery by Courier", result.DeliveryOption);
        }

        [Test]
        public void LongestMatchGetsReturned()
        {
            var enteredPostcode = "AAAAAAB";
            postcodeCleanupService.Setup(x => x.CleanPostcode(It.IsAny<string>())).Returns(enteredPostcode);
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "AAAAAA", Delivery = "Six A's" });
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "AAAAA", Delivery = "Five A's" });
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "AAAAB", Delivery = "Four A's and a B" });

            var result = service.GetValidDeliveryOptions(enteredPostcode, listOfPostcode);
            Assert.AreEqual("Six A's", result.DeliveryOption);
            Assert.AreEqual("AAAAAA", result.Matched);
        }

        [Test]
        public void MatchesBasedOnStart()
        {
            var enteredPostcode = "STARTHERE";
            postcodeCleanupService.Setup(x => x.CleanPostcode(It.IsAny<string>())).Returns(enteredPostcode);
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "START", Delivery = "Postcode begins with START" });
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "FALSESTART", Delivery = "Postcode begins with FALSE" });
            listOfPostcode.Add(new PostcodeDelivery { PostCode = "STARTEND", Delivery = "Postcode begins with END" });

            var result = service.GetValidDeliveryOptions(enteredPostcode, listOfPostcode);
            Assert.AreEqual("Postcode begins with START", result.DeliveryOption);
            Assert.AreEqual("START", result.Matched);
        }
    }
}