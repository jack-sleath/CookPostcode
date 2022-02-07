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
        private Mock<IPostcodeRepository> postcodeRepository;
        private PostcodeLookup service;

        public PostcodeLookupServiceTests()
        {
            postcodeCleanupService = new Mock<IPostcodeCleanupService>();
            postcodeRepository = new Mock<IPostcodeRepository>();
            service = new PostcodeLookup(postcodeCleanupService.Object, postcodeRepository.Object);
        }

        [Test]
        public void NoMatchReturnsExpectedResults()
        {
            var enteredPostcode = "NOMATCH";
            postcodeCleanupService.Setup(x => x.CleanPostcode(It.IsAny<string>())).Returns(enteredPostcode);

            listOfPostcode.Add(new PostcodeDelivery { Postcode = "AAAAAA", Delivery = "Test Message" });
            postcodeRepository.Setup(x=>x.GetPostcodeDeliveries()).Returns(listOfPostcode);

            var result = service.GetValidDeliveryOptions(enteredPostcode);
            Assert.AreEqual("Delivery by Courier", result[3]);
        }

        [Test]
        public void LongestMatchGetsReturned()
        {
            var enteredPostcode = "AAAAAAB";
            postcodeCleanupService.Setup(x => x.CleanPostcode(It.IsAny<string>())).Returns(enteredPostcode);

            listOfPostcode.Add(new PostcodeDelivery { Postcode = "AAAAAA", Delivery = "Six A's" });
            listOfPostcode.Add(new PostcodeDelivery { Postcode = "AAAAA", Delivery = "Five A's" });
            listOfPostcode.Add(new PostcodeDelivery { Postcode = "AAAAB", Delivery = "Four A's and a B" });
            postcodeRepository.Setup(x => x.GetPostcodeDeliveries()).Returns(listOfPostcode);

            var result = service.GetValidDeliveryOptions(enteredPostcode);
            Assert.AreEqual("Six A's", result[3]);
            Assert.AreEqual("AAAAAA", result[2]);
        }

        [Test]
        public void MatchesBasedOnStart()
        {
            var enteredPostcode = "STARTHERE";
            postcodeCleanupService.Setup(x => x.CleanPostcode(It.IsAny<string>())).Returns(enteredPostcode);

            listOfPostcode.Add(new PostcodeDelivery { Postcode = "START", Delivery = "Postcode begins with START" });
            listOfPostcode.Add(new PostcodeDelivery { Postcode = "FALSESTART", Delivery = "Postcode begins with FALSE" });
            listOfPostcode.Add(new PostcodeDelivery { Postcode = "STARTEND", Delivery = "Postcode begins with END" });
            postcodeRepository.Setup(x => x.GetPostcodeDeliveries()).Returns(listOfPostcode);

            var result = service.GetValidDeliveryOptions(enteredPostcode);
            Assert.AreEqual("Postcode begins with START", result[3]);
            Assert.AreEqual("START", result[2]);
        }
    }
}