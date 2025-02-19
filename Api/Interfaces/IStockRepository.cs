using Api.Models;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
    }
}
