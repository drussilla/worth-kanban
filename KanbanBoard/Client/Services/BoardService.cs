using KanbanBoard.Shared;
using KanbanBoard.Shared.Commands;
using System.Net.Http.Json;

namespace KanbanBoard.Client.Services
{
    public class BoardsService : IBoardService
    {
        private readonly HttpClient httpClient;

        public BoardsService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task DeleteStageAsync(Guid stageId)
        {
            await httpClient.DeleteAsync($"api/stage/{stageId}");
        }

        public async Task DeleteTaskAsync(Guid taskId)
        {
            await httpClient.DeleteAsync($"api/task/{taskId}");
        }

        public async Task<IEnumerable<BoardDto>> GetBoardsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<BoardDto>>($"api/board") ?? new List<BoardDto>();
        }

        public async Task MoveTaskToStageAsync(Guid id, Guid stageId)
        {
            await httpClient.PostAsJsonAsync($"api/task/{id}/move", new MoveTaskCommand
            {
                NewStageId = stageId
            });
        }

        public async Task UpdateOrCreateStageAsync(Guid id, Guid boardId, string name)
        {
            await httpClient.PatchAsJsonAsync($"api/stage/{id}", new UpdateOrCreateStageCommand
            {
                BoardId = boardId,
                Name = name,
            });
        }

        public async Task UpdateOrCreateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description)
        {
            await httpClient.PatchAsJsonAsync($"api/task/{id}", new UpdateOrCreateTaskCommand 
            {
                StageId = stageId,
                BoardId = boardId,
                Title = title,
                Description = description
            });
        }
    }
}
