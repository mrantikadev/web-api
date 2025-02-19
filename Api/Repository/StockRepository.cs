using Api.Data;
using Api.Dtos.Stock;
using Api.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                stocks = query.SortBy.ToLower() switch
                {
                    "symbol" => query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol),
                    "companyname" => query.IsDescending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName),
                    _ => stocks
                };
            }

            return await stocks.ToListAsync();
        }

        public Task<Stock?> GetByIdAsync(int id)
        {
            return _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.StockId == id);
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

        public Task<bool> IsStockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.StockId == id);
        }
    }
}
