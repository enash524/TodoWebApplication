using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Application.Queries.Todo;
using TodoWebApplication.Controllers;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Domain.Models;
using Xunit;

namespace TodoWebApplication.Tests
{
    public class TodoControllerTests
    {
        private readonly Mock<ILogger<BaseController>> _logger = new Mock<ILogger<BaseController>>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private readonly Mock<ITodoRepository> _mockTodoRepository = new Mock<ITodoRepository>();
        private readonly TodoController _todoController;

        public TodoControllerTests()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCollection services = new ServiceCollection();

            services.AddScoped(x => _logger.Object);
            services.AddScoped(x => _mediator.Object);

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
        public async Task GetTodoModelById_ShouldReturnOkResult()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<TodoModel>
                {
                    QueryResultType = QueryResultType.Success,
                    Result = new TodoModel()
                });

            // Act
            ActionResult<TodoModel> response = await _todoController.GetTodoModelById(1);

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

                _mediator.Verify(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetTodoModelById_ShouldReturnNotFoundResult()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<TodoModel>
                {
                    QueryResultType = QueryResultType.NotFound,
                    Result = null
                });

            // Act
            ActionResult<TodoModel> response = await _todoController.GetTodoModelById(1);

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

                _mediator.Verify(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetTodoModelById_ShouldReturnBadRequestResult()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<TodoModel>
                {
                    QueryResultType = QueryResultType.Invalid,
                    Result = null
                });

            // Act
            ActionResult<TodoModel> response = await _todoController.GetTodoModelById(-1);

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
                    .BeOfType<BadRequestResult>();

                _mediator.Verify(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }

        [Fact]
        public async Task GetTodoModels_ShouldReturnOkResult()
        {
            // Arrange
            _mediator
                .Setup(x => x.Send(It.IsAny<GetTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<List<TodoModel>>
                {
                    QueryResultType = QueryResultType.Success,
                    Result = new List<TodoModel>()
                });

            // Act
            ActionResult<List<TodoModel>> response = await _todoController.GetTodoModels();

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

                _mediator.Verify(x => x.Send(It.IsAny<GetTodosQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            }
        }
    }
}
