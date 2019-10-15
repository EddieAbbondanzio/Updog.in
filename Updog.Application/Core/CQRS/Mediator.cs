using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Updog.Application {
    public interface IMediator {
        Task<CommandResult> Command<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : class, IQuery;
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

        public async Task<CommandResult> Command<TCommand>(TCommand command) where TCommand : class, ICommand {
            CommandHandler<TCommand> handler = serviceProvider.GetRequiredService<CommandHandler<TCommand>>();
            return await handler.Execute(command);
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : class, IQuery {
            QueryHandler<TQuery, TResult> handler = serviceProvider.GetRequiredService<QueryHandler<TQuery, TResult>>();
            return await handler.Execute(query);
        }
    }
}