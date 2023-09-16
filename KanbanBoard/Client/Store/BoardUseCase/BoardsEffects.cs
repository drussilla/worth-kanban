using Fluxor;
using KanbanBoard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public class BoardsEffects
    {
        private readonly IBoardService boardService;
        private readonly NavigationManager navigationManager;
        private readonly ILogger<BoardsEffects> logger;

        public BoardsEffects(IBoardService boardService, NavigationManager navigationManager, ILogger<BoardsEffects> logger)
        {
            this.boardService = boardService;
            this.navigationManager = navigationManager;
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

        [EffectMethod]
        public async Task HandleSaveBoardAction(SaveBoardAction action, IDispatcher dispatcher)
        {
            try
            {
                // I use fire and forget here, to save some time on development, in prod I would handle result and dispatched another action to update the state accordingly
                await boardService.UpdateBoardAsync(action.Id, action.Name);
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
        public async Task HandleAddBoardResultAction(AddBoardResultAction action, IDispatcher dispatcher)
        {
            navigationManager.NavigateTo($"/board/{action.Board.Id}");
        }

        [EffectMethod]
        public async Task HandleDeleteBoardAction(DeleteBoardAction action, IDispatcher dispatcher)
        {
            try
            {
                // I use fire and forget here, to save some time on development, in prod I would handle result and dispatched another action to update the state accordingly
                await boardService.DeleteBoardAsync(action.Id);
                var nextUrl = "/";
                if (action.BoardToSelectNext != null)
                {
                    nextUrl = $"/board/{action.BoardToSelectNext}";
                }

                logger.LogInformation($"Navigating to {nextUrl}");
                navigationManager.NavigateTo(nextUrl);
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
