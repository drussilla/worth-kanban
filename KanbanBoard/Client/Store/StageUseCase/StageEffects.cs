using Fluxor;
using KanbanBoard.Client.Services;
using KanbanBoard.Client.Store.TaskUseCase;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace KanbanBoard.Client.Store.StageUseCase
{
    public class StageEffects
    {
        private readonly IBoardService boardService;
        private readonly ILogger<StageEffects> logger;

        public StageEffects(IBoardService boardService, ILogger<StageEffects> logger)
        {
            this.boardService = boardService;
            this.logger = logger;
        }

        [EffectMethod]
        public async Task HandleSaveStageAction(SaveStageAction action, IDispatcher dispatcher)
        {
            try
            {
                await boardService.UpdateOrCreateStageAsync(action.Id, action.BoardId, action.Name);
                dispatcher.Dispatch(new SavedStageAction(action.Id));
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
        public async Task HandleDeleteStagekAction(DeleteStageAction action, IDispatcher dispatcher)
        {
            try
            {
                // I use fire and forget here, to save some time on development, in prod I would handle result and dispatched another action to update the state accordingly
                await boardService.DeleteStageAsync(action.Id);
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
