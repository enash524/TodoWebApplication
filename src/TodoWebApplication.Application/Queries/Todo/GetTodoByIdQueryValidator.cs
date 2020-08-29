using FluentValidation;

namespace TodoWebApplication.Application.Queries.Todo
{
    /// <summary>
    /// Validates the input parameters for the GetTodoByIdQuery
    /// </summary>
    public class GetTodoByIdQueryValidator : AbstractValidator<GetTodoByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTodoByIdQueryValidator`1"/> class.
        /// </summary>
        public GetTodoByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
