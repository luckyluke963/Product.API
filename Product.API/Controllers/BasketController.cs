using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Interface;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;
        private readonly IMapper _mapper;

        public BasketController(IMapper mapper, IUnitOfWork uOW)
        {
            _mapper = mapper;
            _uOW = uOW;
        }

        [HttpGet("get-basket-item/{Id}")]
        public async Task<IActionResult> GetBasketById(string Id)
        {
            var _basket = await _uOW.BasketRepository.GetBasketAsync(Id);
            return Ok(_basket ?? new CustomerBasket(Id));
        }

        [HttpPost("update-basket")]
        public async Task<IActionResult> UpdateBasket(CustomerBasket customerBasket)
        {
          
            var _basket = await _uOW.BasketRepository.UpdateBasketAsync(customerBasket);
            return Ok(_basket);

        }

        [HttpDelete("delete-basket-item/{Id}")]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            return Ok(await _uOW.BasketRepository.DeleteBasketAsync(Id));
        }
    }
}
