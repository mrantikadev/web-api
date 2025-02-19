using Api.Dtos.Stock;
using Api.Interfaces;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
                return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = createStockRequestDto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.StockId }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateAsync(id, updateStockRequestDto);

            if (stockModel == null)
                return NotFound();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null)
                return NotFound();

            return NoContent();
        }
    }
}
