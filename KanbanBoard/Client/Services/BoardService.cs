using KanbanBoard.Client.Pages;
using KanbanBoard.Shared;
using KanbanBoard.Shared.Requests;
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

        public async Task<BoardDto> CreateBoard(string name)
        {
            var response = await httpClient.PostAsJsonAsync("api/board", new CreateBoardRequest { Name = name });
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Cannot create new board");
            }

            var board = await response.Content.ReadFromJsonAsync<BoardDto>();
            if (board == null)
            {
                throw new InvalidOperationException($"Cannot deserialize response");
            }

            return board;
        }

        public async Task DeleteBoardAsync(Guid id)
        {
            await httpClient.DeleteAsync($"api/board/{id}");
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
            await httpClient.PostAsJsonAsync($"api/task/{id}/move", new MoveTaskRequest
            {
                NewStageId = stageId
            });
        }

        public async Task UpdateBoardAsync(Guid id, string name)
        {
            await httpClient.PatchAsJsonAsync($"api/board/{id}", new UpdateBoardRequest
            {
                Name = name,
            });
        }

        public async Task UpdateOrCreateStageAsync(Guid id, Guid boardId, string name)
        {
            await httpClient.PatchAsJsonAsync($"api/stage/{id}", new UpdateOrCreateStageRequest
            {
                BoardId = boardId,
                Name = name,
            });
        }

        public async Task UpdateOrCreateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description)
        {
            await httpClient.PatchAsJsonAsync($"api/task/{id}", new UpdateOrCreateTaskRequest 
            {
                StageId = stageId,
                BoardId = boardId,
                Title = title,
                Description = description
            });
        }
    }
}
