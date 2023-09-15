using Fluxor;
using KanbanBoard.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace KanbanBoard.Client.Store.TaskUseCase
{
    public class TaskEffects
    {
        private readonly IBoardService boardService;
        private readonly ILogger<TaskEffects> logger;

        public TaskEffects(IBoardService boardService, ILogger<TaskEffects> logger)
        {
            this.boardService = boardService;
            this.logger = logger;
        }

        [EffectMethod]
        public async Task HandleSaveTaskAction(SaveTaskAction action, IDispatcher dispatcher)
        {
            try
            {
                // I use fire and forget here, to save some time on development, in prod I would handle result and dispatched another action to update the state accordingly
                await boardService.UpdateTaskAsync(action.TaskId, action.StageId, action.BoardId, action.Title, action.Description);
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
        public async Task HandleMoveTaskAction(MoveTaskAction action, IDispatcher dispatcher)
        {
            try
            {
                // I use fire and forget here, to save some time on development, in prod I would handle result and dispatched another action to update the state accordingly
                await boardService.MoveTaskToStageAsync(action.TaskId, action.NewStageId);
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
        public async Task HandleDeleteTaskAction(DeleteTaskAction action, IDispatcher dispatcher)
        {
            try
            {
                // I use fire and forget here, to save some time on development, in prod I would handle result and dispatched another action to update the state accordingly
                await boardService.DeleteTaskAsync(action.TaskId);
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
