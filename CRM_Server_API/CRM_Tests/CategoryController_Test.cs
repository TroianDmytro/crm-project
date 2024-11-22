using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Controllers;
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
    public class CategoryController_Test
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryController _controller;

        public CategoryController_Test()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CategoryController(_mockCategoryService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllCategory_ReturnsOk_WhenCategoriesExist()
        {
            var categoryList = new List<CategoryDTO>
            {
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Fruits" },
                new CategoryDTO { Id = Guid.NewGuid(), Name = "Drinks" }
            };

            _mockCategoryService.Setup(service => service.GetAllCategoriesAsync())
                .ReturnsAsync(categoryList);

            var categoryResponceList = new List<CategoryResponce>
            {
                new CategoryResponce { Id = categoryList[0].Id, Name = categoryList[0].Name },
                new CategoryResponce { Id = categoryList[1].Id, Name = categoryList[1].Name }
            };

            _mockMapper.Setup(m => m.Map<List<CategoryResponce>>(categoryList)).Returns(categoryResponceList);

            var result = await _controller.GetAllCategory();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CategoryResponce>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            var categoryId = Guid.NewGuid();
            _mockCategoryService.Setup(service => service.GetCategoryAsync(categoryId))
                .ReturnsAsync((CategoryDTO)null);

            var result = await _controller.GetCategoryById(categoryId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsOk_WhenCategoryExists()
        {
            var categoryId = Guid.NewGuid();
            var category = new CategoryDTO { Id = categoryId, Name = "Food" };
            var categoryResponce = new CategoryResponce { Id = categoryId, Name = "Food" };

            _mockCategoryService.Setup(service => service.GetCategoryAsync(categoryId))
                .ReturnsAsync(category);

            _mockMapper.Setup(m => m.Map<CategoryResponce>(category)).Returns(categoryResponce);

            var result = await _controller.GetCategoryById(categoryId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CategoryResponce>(okResult.Value);
            Assert.Equal(categoryId, returnValue.Id);
            Assert.Equal("Food", returnValue.Name);
        }

        //service completes adding the category without errors controller returns code 200 OK
        [Fact]
        public async Task AddCategory_ReturnsOk_WhenCategoryIsAddedSuccessfully()
        {
            var categoryName = "Food";
            var categoryDTO = new CategoryDTO { Name = categoryName };

            _mockCategoryService.Setup(service => service.AddCategoryAsync(categoryDTO))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddCategory(categoryName);

            Assert.IsType<OkResult>(result);
        }

        //if the category exists edit successful and controller returns code 200 OK.
        [Fact]
        public async Task EditCategory_ReturnsOk_WhenCategoryIsUpdatedSuccessfully()
        {
            var categoryId = Guid.NewGuid();
            var categoryName = "Phones";
            var existingCategory = new CategoryDTO { Id = categoryId, Name = "Fish" };

            _mockCategoryService.Setup(service => service.GetCategoryAsync(categoryId))
                .ReturnsAsync(existingCategory);
            _mockCategoryService.Setup(service => service.UpdateCategoryAsync(It.IsAny<CategoryDTO>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateCategory(categoryId, categoryName);

            Assert.IsType<OkResult>(result);
        }

        // after deleting a category controller returns code 200 OK
        [Fact]
        public async Task DeleteCategory_ReturnsOk_WhenCategoryIsDeletedSuccessfully()
        {
            var categoryId = Guid.NewGuid();

            _mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteCategory(categoryId);

            Assert.IsType<OkResult>(result);
        }
    }
}
