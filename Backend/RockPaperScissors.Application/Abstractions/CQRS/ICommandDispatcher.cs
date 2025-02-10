namespace RockPaperScissors.Application.Abstractions;

public interface ICommandDispatcher
{
    Task<TResult> Send<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
}