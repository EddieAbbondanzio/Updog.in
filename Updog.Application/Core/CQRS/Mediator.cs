using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Updog.Domain;

namespace Updog.Application {
    public interface IMediator {
        Task<Either<CommandResult, Error>> Command<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task<Either<TResult, Error>> Query<TQuery, TResult>(TQuery query) where TQuery : class, IQuery;
    }

    /// <summary>
    /// Mediator to handle issuing commands, and queries.
    /// </summary>
    public sealed class Mediator : IMediator {
        #region Fields
        private IServiceProvider serviceProvider;
        #endregion

        #region Constructor(s)
        public Mediator(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }
        #endregion

        public async Task<Either<CommandResult, Error>> Command<TCommand>(TCommand command) where TCommand : class, ICommand {
            CommandHandler<TCommand> handler = serviceProvider.GetRequiredService<CommandHandler<TCommand>>();
            handler.Init(serviceProvider);

            return await handler.Execute(command);
        }

        public async Task<Either<TResult, Error>> Query<TQuery, TResult>(TQuery query) where TQuery : class, IQuery {
            QueryHandler<TQuery, TResult> handler = serviceProvider.GetRequiredService<QueryHandler<TQuery, TResult>>();
            handler.Init(serviceProvider);

            return await handler.Execute(query);
        }
    }
}