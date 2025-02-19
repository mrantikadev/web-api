using Api.Dtos.Stock;
using Api.Models;

namespace Api.Mappers
{
    public static class StockMappers
    {
        // Mapper for returning data to user
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                StockId = stock.StockId,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        // Mapper for sending create data to the api
        public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap
            };
        }
    }
}
