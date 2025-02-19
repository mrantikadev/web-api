using Api.Data;
using Api.Dtos.Comment;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Comment>> GetAllAsync()
        {
            return _context.Comments.ToListAsync();
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            return _context.Comments.FindAsync(id).AsTask();
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
                return null;

            existingComment.Title = updateCommentRequestDto.Title;
            existingComment.Content = updateCommentRequestDto.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}
