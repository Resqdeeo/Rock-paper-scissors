using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Application.Abstractions;

namespace RockPaperScissors.Infrastructure.CQRS;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Send<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query);
    }
}