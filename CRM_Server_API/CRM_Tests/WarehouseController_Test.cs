using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Blobs;
using CRM_Server_API.Controllers;
using CRM_Server_API.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Tests
{
    public class WarehouseController_Test
    {
        private readonly Mock<IWarehouseService> _mockWarehouseService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly WarehouseController _controller;

        public WarehouseController_Test()
        {
            _mockWarehouseService = new Mock<IWarehouseService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new WarehouseController(_mockWarehouseService.Object, _mockMapper.Object, null);
        }

        [Fact]
        public async Task GetAllWarehouse_ReturnsOk_WhenWarehousesAreReturned()
        {
            var warehouseList = new List<WarehouseDTO>
            {
                new WarehouseDTO { Id = Guid.NewGuid(), Name = "Склад Запорожье" },
                new WarehouseDTO { Id = Guid.NewGuid(), Name = "Склад Кривой Рог" },
                new WarehouseDTO { Id = Guid.NewGuid(), Name = "Склад Киев " }
            };

            _mockWarehouseService.Setup(service => service.GetAllWarehousesAsync())
                                 .ReturnsAsync(warehouseList);

            var result = await _controller.GetAllWarehouse();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WarehouseDTO>>(okResult.Value);
            Assert.Equal(3, returnValue.Count());
        
        }

        [Fact]
        public async Task GetWarehouseById_ReturnsOk_WhenWarehouseExists()
        {
            var warehouseId = Guid.NewGuid();
            var warehouse = new WarehouseDTO { Id = warehouseId, Name = "Склад Киев" };

            _mockWarehouseService.Setup(service => service.GetWarehouseByIdAsync(warehouseId))
                                 .ReturnsAsync(warehouse);

            var result = await _controller.GetWarehouseById(warehouseId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<WarehouseDTO>(okResult.Value);
            Assert.Equal(warehouseId, returnValue.Id);
        }

        [Fact]
        public async Task AddWarehouse_ReturnsOk_WhenWarehouseIsAdded()
        {
            var warehouseRequest = new WarehouseRequest { Name = "Склад Днепр" };
            var warehouseDTO = new WarehouseDTO { Id = Guid.NewGuid(), Name = "Склад Днепр" };

            _mockMapper.Setup(m => m.Map<WarehouseDTO>(It.IsAny<WarehouseRequest>()))
                       .Returns(warehouseDTO);
            _mockWarehouseService.Setup(service => service.AddWarehousesAsync(warehouseDTO))
                                 .Returns(Task.CompletedTask);

            var result = await _controller.AddWarehouse(warehouseRequest);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task EDitWarehouse_ReturnsOk_WhenWarehouseIsUpdated()
        {
            var warehouseId = Guid.NewGuid();
            var warehouseRequest = new WarehouseRequest { Name = "Склад Запорожье" };
            var warehouseDTO = new WarehouseDTO { Id = warehouseId, Name = "Склад Кривой Рог" };

            _mockMapper.Setup(m => m.Map<WarehouseDTO>(It.IsAny<WarehouseRequest>()))
                       .Returns(warehouseDTO);
            _mockWarehouseService.Setup(service => service.UpdateWarehousesAsync(warehouseId, warehouseDTO))
                                 .Returns(Task.CompletedTask);

            var result = await _controller.UpdateWarehouse(warehouseId, warehouseRequest);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddProductToWarehouse_ReturnsOk_WhenProductIsAdded()
        {
            var warehouseProductRequest = new WarehouseProductRequest { WarehouseId = Guid.NewGuid(), ProductId = Guid.NewGuid(), QuantityStock = 120 };
            var warehouseProductDTO = new WarehouseProductDTO { WarehouseId = warehouseProductRequest.WarehouseId, 
                ProductId = warehouseProductRequest.ProductId, 
                QuantityStock = warehouseProductRequest.QuantityStock };

            _mockMapper.Setup(m => m.Map<WarehouseProductDTO>(It.IsAny<WarehouseProductRequest>()))
                       .Returns(warehouseProductDTO);
            _mockWarehouseService.Setup(service => service.AddProductToWarehouse(warehouseProductDTO))
                                 .Returns(Task.CompletedTask);

            var result = await _controller.AddProductToWarehouse(warehouseProductRequest);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteWarehouse_ReturnsNoContent_WhenWarehouseIsDeleted()
        {
            var warehouseId = Guid.NewGuid();
            _mockWarehouseService.Setup(service => service.IsExists(warehouseId))
                                 .ReturnsAsync(true);
            _mockWarehouseService.Setup(service => service.DeleteWarehousesAsync(warehouseId))
                                 .Returns(Task.CompletedTask);

            var result = await _controller.DeleteProduct(warehouseId);

            Assert.IsType<NoContentResult>(result);
        }

    }
}
