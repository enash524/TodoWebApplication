using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TodoWebApplication.Controllers;
using TodoWebApplication.Models;
using TodoWebApplication.Repositories;
using Xunit;

namespace TodoWebApplication.Tests
{
    public class TodoControllerTests
    {
        private readonly Mock<ILogger<BaseController>> _logger = new Mock<ILogger<BaseController>>();
        private readonly Mock<ITodoRepository> _mockTodoRepository = new Mock<ITodoRepository>();
        private readonly TodoController _todoController;

        public TodoControllerTests()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCollection services = new ServiceCollection();

            services.AddScoped(x => _logger.Object);

            httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext
                {
                    RequestServices = services.BuildServiceProvider()
                });

            _todoController = new TodoController(_mockTodoRepository.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextAccessorMock.Object.HttpContext
                }
            };
        }

        [Fact]
        public void GetTodoModelById_ShouldReturnOkResult()
        {
            // Arrange
            _mockTodoRepository
                .Setup(x => x.GetTodoModelById(It.IsAny<int>()))
                .Returns(new TodoModel() { Id = 1 });

            // Act
            ActionResult<TodoModel> response = _todoController.GetTodoModelById(1);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<OkObjectResult>();
            }

            _mockTodoRepository.Verify(x => x.GetTodoModelById(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void GetTodoModelById_ShouldReturnNotFoundResult()
        {
            // Arrange
            _mockTodoRepository
                .Setup(x => x.GetTodoModelById(It.IsAny<int>()))
                .Returns((TodoModel)null);

            // Act
            ActionResult<TodoModel> response = _todoController.GetTodoModelById(1);

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<NotFoundResult>();
            }

            _mockTodoRepository.Verify(x => x.GetTodoModelById(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void GetTodoModels_ShouldReturnOkResult()
        {
            // Arrange
            _mockTodoRepository
                .Setup(x => x.GetTodoModels())
                .Returns(new List<TodoModel>());

            // Act
            ActionResult<List<TodoModel>> response = _todoController.GetTodoModels();

            // Assert
            using (new AssertionScope())
            {
                response
                    .Should()
                    .NotBeNull();

                response.Result
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<OkObjectResult>();
            }

            _mockTodoRepository.Verify(x => x.GetTodoModels(), Times.Once());
        }
    }
}
