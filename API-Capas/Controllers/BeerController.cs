using Core.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Data.DTO;

namespace API_Capas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDTO> _beerInsertValidator;
        private IValidator<BeerUpdateDTO> _beerUpdateValidator;
        private IBeerCore _beerCore;

        public BeerController
        (
            IValidator<BeerInsertDTO> beerInsertValidator,
            IValidator<BeerUpdateDTO> beerUpdateValidator,
            IBeerCore beerCore
        )
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerCore = beerCore;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _beerCore.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beerDTO = await _beerCore.GetById(id);
            return beerDTO == null ? NotFound() : Ok(beerDTO);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDTO>> Create(BeerInsertDTO beerInsertDTO)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerCore.Create(beerInsertDTO);
            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerCore.Update(id, beerUpdateDTO);
            return beerDto == null ? NotFound() : Ok(beerDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDTO>> Delete(int id)
        {
            var beerDto = await _beerCore.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

    }
}
