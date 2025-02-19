using Api.Data;
using Api.Dtos.Stock;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }

        public Task<Stock?> GetByIdAsync(int id)
        {
            return _context.Stocks.FindAsync(id).AsTask();
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.StockId == id);

            if (existingStock == null)
                return null;

            existingStock.Symbol = updateStockRequestDto.Symbol;
            existingStock.CompanyName = updateStockRequestDto.CompanyName;
            existingStock.Purchase = updateStockRequestDto.Purchase;
            existingStock.LastDiv = updateStockRequestDto.LastDiv;
            existingStock.Industry = updateStockRequestDto.Industry;
            existingStock.MarketCap = updateStockRequestDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.StockId == id);

            if (stockModel == null)
                return null;

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}
