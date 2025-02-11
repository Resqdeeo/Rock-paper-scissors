namespace RockPaperScissors.Application.Abstractions;

public interface IQueryDispatcher
{
    Task<TResult> Send<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}