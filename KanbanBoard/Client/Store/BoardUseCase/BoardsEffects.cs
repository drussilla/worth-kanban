using Fluxor;
using KanbanBoard.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Threading.Tasks.Dataflow;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public class BoardsEffects
    {
        private readonly IBoardService boardService;
        private readonly ILogger<BoardsEffects> logger;

        public BoardsEffects(IBoardService boardService, ILogger<BoardsEffects> logger)
        {
            this.boardService = boardService;
            this.logger = logger;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(FetchBoardsAction action, IDispatcher dispatcher)
        {
            try 
            {
                var boards = await boardService.GetBoardsAsync();
                dispatcher.Dispatch(new FetchBoardsResultAction(boards));
            }
            catch (AccessTokenNotAvailableException exception)
            {
                logger.LogError(exception, "Token is not valid. Redirecting to login");
                exception.Redirect();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unexpected error. Rethrowing");
                throw;
            }
        }

        [EffectMethod]
        public async Task HandleAddBoardAction(AddBoardAction action, IDispatcher dispatcher)
        {
            try
            {
                var board = await boardService.CreateBoard(action.Name);
                dispatcher.Dispatch(new AddBoardResultAction(board));
            }
            catch (AccessTokenNotAvailableException exception)
            {
                logger.LogError(exception, "Token is not valid. Redirecting to login");
                exception.Redirect();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unexpected error. Rethrowing");
                throw;
            }
        }
    }

}
