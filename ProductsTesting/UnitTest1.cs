using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductFirstModule.Controller;
using ProductFirstModule.Database.Entities;
using ProductFirstModule.Services;

namespace ProductsTesting
{
    [TestFixture]
    public class Tests
    {
        Mock<IProductMicroservice> mock;

        [OneTimeSetUp]
        public void Setup()
        {
            Mock<IProductMicroservice> mock = new Mock<IProductMicroservice>();
        }

        [Test]
        public void Test_SearchProductById_RetunsTrue()
        {
            ActionResult<Product> p = new Product() { Id = 1, Name = "Hello" };
            mock.Setup(s => s.SearchProductById(1)).ReturnsAsync(p);

            ProductMicroServicesController product = new ProductMicroServicesController(mock.Object);
            var result =  product.SearchProductById(1);
            Assert.Equals(p,result);
        }
    }
}