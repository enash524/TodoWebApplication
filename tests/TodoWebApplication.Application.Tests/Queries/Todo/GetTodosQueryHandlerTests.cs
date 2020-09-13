using System.Collections.Generic;
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
    public class GetTodosQueryHandlerTests
    {
        private readonly GetTodosQueryHandler _handler;
        private readonly Mock<ILogger<GetTodosQueryHandler>> _logger;
        private readonly Mock<ITodoRepository> _todoRepository;

        public GetTodosQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetTodosQueryHandler>>();
            _todoRepository = new Mock<ITodoRepository>();
            _handler = new GetTodosQueryHandler(_logger.Object, _todoRepository.Object);
        }

        [Fact]
        public async Task Should_Call_GetTodoModelsAsync_In_Repository()
        {
            // Arrange
            _todoRepository
                .Setup(x => x.GetTodoModelsAsync())
                .ReturnsAsync(new List<TodoModel>());

            // Act
            QueryResult<List<TodoModel>> actual = await _handler.Handle(new GetTodosQuery(), new CancellationToken());

            // Assert
            using (new AssertionScope())
            {
                actual
                    .Should()
                    .NotBeNull();

                _todoRepository
                    .Verify(x => x.GetTodoModelsAsync(), Times.Once());
            }
        }
    }
}
