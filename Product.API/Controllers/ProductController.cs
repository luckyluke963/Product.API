using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Infrastructure.Data;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork Uow, IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult> Get()
        {
            var res = await _uow.ProductRepository.GetAllAsync(x =>x.Category);
            var result = _mapper.Map<List<ProductDto>>(res);
            return Ok(result);
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _uow.ProductRepository.GetByIdAsync(id, x=>x.Category);
            var result = _mapper.Map<ProductDto>(res);
            return Ok(result);


        }

        [HttpPost("add-new-product")]
         public async Task<ActionResult> Post(CreateProductDto createProductDto)
        {
            try
            {
              
                    if(ModelState.IsValid)
                    {
                        var res = _mapper.Map<Products>(createProductDto);
                        await _uow.ProductRepository.AddAsync(res);
                        return Ok(res);
                    }
                    return BadRequest(createProductDto);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
