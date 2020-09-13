using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;
using Moq;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Application.Queries.Todo;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Domain.Models;
using Xunit;

namespace TodoWebApplication.Application.Tests.Queries.Todo
{
    public class GetTodoByIdQueryHandlerTests
    {
        private readonly GetTodoByIdQueryHandler _handler;
        private readonly Mock<ILogger<GetTodoByIdQueryHandler>> _logger;
        private readonly Mock<ITodoRepository> _todoRepository;
        private readonly GetTodoByIdQueryValidator _validator;

        public GetTodoByIdQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetTodoByIdQueryHandler>>();
            _todoRepository = new Mock<ITodoRepository>();
            _validator = new GetTodoByIdQueryValidator();
            _handler = new GetTodoByIdQueryHandler(_logger.Object, _todoRepository.Object, _validator);
        }

        [Fact]
        public async Task Request_With_Invalid_Id_Should_Return_Invalid_Result()
        {
            // Arrange
            int id = 0;
            string errors = "'Id' must be greater than '0'.";
            string expectedMessage = $"GetTodoByIdQuery with Id: {id} produced errors on validation {errors}.";

            // Act
            QueryResult<TodoModel> actual = await _handler.Handle(new GetTodoByIdQuery(), new CancellationToken());

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                actual
                    .QueryResultType
                    .Should()
                    .Be(QueryResultType.Invalid);

                _logger.Invocations.Count
                    .Should()
                    .Be(1);

                _logger.Invocations[0].Arguments[0]
                    .Should()
                    .Be(LogLevel.Error);

                _logger
                    .Verify(x => x.Log(LogLevel.Error,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((x, t) => string.Equals(x.ToString(), expectedMessage)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                        Times.Once());
            }
        }

        [Fact]
        public async Task Should_Call_GetTodoModelByIdAsync_In_Repository()
        {
            // Arrange
            int id = 1;
            _todoRepository
                .Setup(x => x.GetTodoModelByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new TodoModel());

            // Act
            QueryResult<TodoModel> actual = await _handler.Handle(new GetTodoByIdQuery { Id = id }, new CancellationToken());

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                actual
                    .QueryResultType
                    .Should()
                    .Be(QueryResultType.Success);

                _todoRepository
                    .Verify(x => x.GetTodoModelByIdAsync(It.IsAny<int>()), Times.Once());
            }
        }

        [Fact]
        public async Task Should_Return_NotFound_If_No_Entity_Exists()
        {
            // Arrange
            int id = 1;
            string expectedMessage = $"GetTodoByIdQuery with Id: {id} was not found.";
            _todoRepository
                .Setup(x => x.GetTodoModelByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((TodoModel)null);

            // Act
            QueryResult<TodoModel> actual = await _handler.Handle(new GetTodoByIdQuery { Id = id }, new CancellationToken());

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                actual
                    .QueryResultType
                    .Should()
                    .Be(QueryResultType.NotFound);

                _logger.Invocations.Count
                    .Should()
                    .Be(1);

                _logger.Invocations[0].Arguments[0]
                    .Should()
                    .Be(LogLevel.Error);

                _logger
                    .Verify(x => x.Log(LogLevel.Error,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((x, t) => string.Equals(x.ToString(), expectedMessage)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                        Times.Once());
            }
        }
    }
}
