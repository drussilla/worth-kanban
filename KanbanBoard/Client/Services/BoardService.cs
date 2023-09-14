using KanbanBoard.Shared;
using System.Net.Http.Json;

namespace KanbanBoard.Client.Services
{
    public interface IBoardService 
    {
        Task<IEnumerable<BoardDto>> GetBoardsAsync();
    }

    public class BoardsService : IBoardService
    {
        private readonly HttpClient httpClient;

        public BoardsService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<BoardDto>> GetBoardsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<BoardDto>>($"api/board") ?? new List<BoardDto>();
        }
    }
}
