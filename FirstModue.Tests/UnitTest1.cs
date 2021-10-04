using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductFirstModule.Controller;
using ProductFirstModule.Database.Entities;
using ProductFirstModule.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FirstModue.Tests
{
    public class UnitTest1
    {
        public Mock<IProductMicroservice> mock = new Mock<IProductMicroservice>();

        [Fact]
        public async void Test_InvalidData_ProductNyId()
        {
            var result = new Product()
                {
                    Id = 1,
                    Price=600,
                    Name="karthik",
                    Description="Nad",
                    Image_Name="ledhu",
                    Rating=4
                };
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.ProductById(1)).ReturnsAsync(result);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 = await controller.SearchProductById(2);
            Assert.False(result.Equals(result1.Value));
        }
        [Fact]
        public async void Test_ValidData_ProductNyId()
        {
            var result = new Product()
            {
                Id = 1,
                Price = 600,
                Name = "karthik",
                Description = "Nad",
                Image_Name = "ledhu",
                Rating = 4
            };
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.ProductById(1)).ReturnsAsync(result);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 = await controller.SearchProductById(1);
            Assert.True(result.Equals(result1.Value));
        }
        [Fact]
        public async void Test_ValidData_ProductByName()
        {
            var result = new Product()
            {
                Id = 1,
                Price = 600,
                Name = "karthik",
                Description = "Nad",
                Image_Name = "ledhu",
                Rating = 4
            };
            string name = "karthik";
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.SearchProductByName(name)).ReturnsAsync(result);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 = await controller.SearchProductByName(name);
            Assert.True(result.Equals(result1.Value));
        }

        [Fact]
        public async void Test_InvalidData_ProductByName()
        {
            var result = new Product()
            {
                Id = 1,
                Price = 600,
                Name = "karthik",
                Description = "Nad",
                Image_Name = "ledhu",
                Rating = 4
            };
            string name = "karthik";
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.SearchProductByName(name)).ReturnsAsync(result);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 = await controller.SearchProductByName("purna");
            Assert.False(result.Equals(result1.Value));
        }

        [Fact]
        public  void Test_ValidData_GetProducts()
        {
            var result = new List<Product>();
            result.Add(new Product()
            {
                Id = 1,
                Price = 600,
                Name = "karthik",
                Description = "Nad",
                Image_Name = "ledhu",
                Rating = 4
            });
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.GetProducts()).Returns(result);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 =  controller.GetProducts();
            Assert.True(result.Equals(result1.Value));
        }

        [Fact]
        public async void Test_InvalidData_ProductRating()
        {
            var result = new Rating()
            {
                ProductId = 3,
                UserId = 103,
                ProductRating = 5
            };
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.AddProductRating(result)).ReturnsAsync(false);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 = await controller.AddProductRating(result);
            var okResult = result1 as StatusCodeResult;
            Assert.Equal("404", okResult.StatusCode.ToString());
        }
        [Fact]
        public async void Test_ValidData_ProductRating()
        {
            var result = new Rating()
            {
                ProductId = 1,
                UserId = 103,
                ProductRating = 5
            };
            //var DataStore = A.Fake<IProductMicroservice>();
            mock.Setup(p => p.AddProductRating(result)).ReturnsAsync(true);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new ProductMicroServicesController(mock.Object);
            var result1 = await controller.AddProductRating(result);
            var okResult = result1 as StatusCodeResult;
            Assert.Equal("200", okResult.StatusCode.ToString());
        }

    }
}
