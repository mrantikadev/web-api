using Api.Models;

namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetByIdAsync(int id);
    }
}
