using Api.Dtos.Comment;
using Api.Models;

namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetByIdAsync(int id);
        public Task<Comment> CreateAsync(Comment commentModel);
        public Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);
    }
}
