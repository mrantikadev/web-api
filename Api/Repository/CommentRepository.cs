using Api.Data;
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
    }
}
