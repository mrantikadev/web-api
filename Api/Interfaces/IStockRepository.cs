using Api.Dtos.Stock;
using Api.Helpers;
using Api.Models;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync(QueryObject query);
        public Task<Stock?> GetByIdAsync(int id);
        public Task<Stock> CreateAsync(Stock stockModel);
        public Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto);
        public Task<Stock?> DeleteAsync(int id);
        public Task<bool> IsStockExists(int id);
    }
}
