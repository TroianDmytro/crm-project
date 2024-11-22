using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
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
    public class DealController_Test
    {
        private readonly Mock<IDealService> _mockDealService;
        private readonly Mock<IDealProductService> _mockDealProductService;
        private readonly Mock<IClientService> _mockClientService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<BlobModul> _mockBlobModul; 
        private readonly DealController _controller;

        public DealController_Test()
        {
            _mockDealService = new Mock<IDealService>();
            _mockDealProductService = new Mock<IDealProductService>();
            _mockClientService = new Mock<IClientService>();
            _mockMapper = new Mock<IMapper>();
            _mockBlobModul = new Mock<BlobModul>(null, null); 
            _controller = new DealController(
                _mockMapper.Object,
                _mockDealService.Object,
                _mockDealProductService.Object,
                _mockClientService.Object,
                _mockBlobModul.Object 
            );
        }

        [Fact]
        public async Task GetDealList_ReturnsOk_WhenDealsExist()
        {
            var deals = new List<DealDTO>
            {
                new DealDTO { DealId = Guid.NewGuid(), ProductDTOs = new List<ProductDTO>() }
            };

            _mockDealService.Setup(service => service.GetAllDealsAsync())
                .ReturnsAsync(deals);

            var result = await _controller.GetDealList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DealDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetDealList_ReturnsOk_WhenNoDealsExist()
        {
            _mockDealService.Setup(service => service.GetAllDealsAsync())
                .ReturnsAsync(new List<DealDTO>());

            var result = await _controller.GetDealList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DealDTO>>(okResult.Value);
            Assert.Empty(returnValue);
        }


        [Fact]
        public async Task GetDealId_ReturnsNotFound_WhenDealDoesNotExist()
        {
            var dealId = Guid.NewGuid();
            _mockDealService.Setup(service => service.GetDealByIdAsync(dealId))
                .ReturnsAsync((DealDTO)null);

            var result = await _controller.GetDealId(dealId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddDeal_ReturnsNotFound_WhenClientDoesNotExist()
        {
            var dealRequest = new DealRequest { ClientId = Guid.NewGuid() };

            _mockClientService.Setup(service => service.GetClientById(dealRequest.ClientId))
                .ReturnsAsync((ClientDTO)null);

            var result = await _controller.AddDeal(dealRequest);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddDeal_ReturnsOk_WhenDealIsAdded()
        {
            var dealRequest = new DealRequest { ClientId = Guid.NewGuid() };
            var client = new ClientDTO { Id = dealRequest.ClientId };
            var dealDto = new DealDTO();

            _mockClientService.Setup(service => service.GetClientById(dealRequest.ClientId))
                .ReturnsAsync(client);
            _mockMapper.Setup(mapper => mapper.Map<DealDTO>(dealRequest))
                .Returns(dealDto);

            var result = await _controller.AddDeal(dealRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dealDto, okResult.Value);
        }

        [Fact]
        public async Task AddProductToDeal_ReturnsNotFound_WhenExceptionThrown()
        {
            var dealProductDTO = new DealProductDTO();

            _mockDealProductService.Setup(service => service.AddProductToDeal(dealProductDTO))
                .ThrowsAsync(new KeyNotFoundException("Deal not found"));

            var result = await _controller.AddProductToDeal(dealProductDTO);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Deal not found", notFoundResult.Value);
        }

        [Fact]
        public async Task AddProductToDeal_ReturnsOk_WhenProductIsAdded()
        {
            var dealProductDTO = new DealProductDTO();

            _mockDealProductService.Setup(service => service.AddProductToDeal(dealProductDTO))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddProductToDeal(dealProductDTO);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task EditDeal_ReturnsNotFound_WhenDealDoesNotExist()
        {
            var dealId = Guid.NewGuid();
            var dealUpdate = new DealUpdate();

            _mockDealService.Setup(service => service.DealIsExists(dealId))
                .ReturnsAsync(false);

            var result = await _controller.UpdateDeal(dealId, dealUpdate);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task EditDeal_ReturnsOk_WhenDealUpdated()
        {
            var dealId = Guid.NewGuid();
            var dealUpdate = new DealUpdate();

            _mockDealService.Setup(service => service.DealIsExists(dealId))
                .ReturnsAsync(true);
            _mockDealService.Setup(service => service.UpdateDealAsync(dealId, dealUpdate))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateDeal(dealId, dealUpdate);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteDeal_ReturnsNotFound_WhenDealDoesNotExist()
        {
            var dealId = Guid.NewGuid();

            _mockDealService.Setup(service => service.DealIsExists(dealId))
                .ReturnsAsync(false);

            var result = await _controller.DeleteDeal(dealId);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
