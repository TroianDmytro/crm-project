using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Blobs;
using CRM_Server_API.Controllers;
using CRM_Server_API.Models.Request;
using CRM_Server_API.Models.Responce;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Tests
{
    public class ProductController_Test
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductController _controller;

        public ProductController_Test()
        {
            _mockProductService = new Mock<IProductService>();
            _mockMapper = new Mock<IMapper>();

            _controller = new ProductController(_mockProductService.Object, _mockMapper.Object, null);
        }
        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithProductList()
        {
            var productDtos = new List<ProductDTO>
        {
            new ProductDTO { ProductId = Guid.NewGuid(), Name = "bananas", Price = 15 },
            new ProductDTO { ProductId = Guid.NewGuid(), Name = "fish", Price = 32 }
        };

            var productResponses = new List<ProductResponce>
        {
            new ProductResponce { ProductId = productDtos[0].ProductId, Name = "bananas", Price = 15 },
            new ProductResponce { ProductId = productDtos[1].ProductId, Name = "fish", Price = 32 }
        };

            _mockProductService.Setup(service => service.GetAllProductsAsync())
                               .ReturnsAsync(productDtos);
            _mockMapper.Setup(mapper => mapper.Map<List<ProductResponce>>(productDtos))
                       .Returns(productResponses);

            var result = await _controller.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<ProductResponce>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetProductId_ReturnsOkResult_WhenProductExists()
        {
            var productId = Guid.NewGuid();
            var productDto = new ProductDTO { ProductId = productId, Name = "Iphone 15", Price = 4 };
            var productResponse = new ProductResponce { ProductId = productId, Name = "Iphone 15", Price = 4 };

            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                               .ReturnsAsync(productDto);
            _mockMapper.Setup(mapper => mapper.Map<ProductResponce>(productDto))
                       .Returns(productResponse);

            var result = await _controller.GetProductId(productId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductResponce>(okResult.Value);
            Assert.Equal(productId, returnValue.ProductId);
        }

        [Fact]
        public async Task AddProduct_ReturnsOkResult_WhenProductIsAdded()
        {
            var productRequest = new ProductRequest { Name = "Playstation 5 Pro", Price = 900 };
            var productDto = new ProductDTO { ProductId = Guid.NewGuid(), Name = "Playstation 5 Pro", Price = 900 };

            _mockMapper.Setup(mapper => mapper.Map<ProductDTO>(productRequest))
                       .Returns(productDto);
            _mockProductService.Setup(service => service.AddProductAsync(productDto))
                               .Returns(Task.CompletedTask);

            var result = await _controller.AddProduct(productRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productDto.Name, returnValue.Name);
        }

        [Fact]
        public async Task EditProduct_ReturnsNoContent_WhenProductIsUpdated()
        {
            var productId = Guid.NewGuid();
            var productRequest = new ProductRequest { Name = "Playstation 5 Pro", Price = 900 };
            var existingProductDto = new ProductDTO { ProductId = productId, Name = "Playstation 5", Price = 600 };

            _mockProductService.Setup(service => service.ProductIsExists(productId))
                               .ReturnsAsync(true);
            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                               .ReturnsAsync(existingProductDto);
            _mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<ProductDTO>()))
                               .Returns(Task.CompletedTask);

            var result = await _controller.UpdateProduct(productId, productRequest);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal("Playstation 5 Pro", existingProductDto.Name);
            Assert.Equal(900, existingProductDto.Price);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult_WhenProductIsDeleted()
        {
            var productId = Guid.NewGuid();
            _mockProductService.Setup(service => service.ProductIsExists(productId))
                               .ReturnsAsync(true);
            _mockProductService.Setup(service => service.DeleteProductAsync(productId))
                               .Returns(Task.CompletedTask);

            var result = await _controller.DeleteProduct(productId);

            Assert.IsType<OkResult>(result);
        }

    }
}
