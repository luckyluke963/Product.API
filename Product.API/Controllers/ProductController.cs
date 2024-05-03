

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Error;
using Product.API.MyHelper;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Core.Sharing;

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
        public async Task<ActionResult> Get([FromQuery] ProductParams productParams)
        {
            var res = await _uow.ProductRepository.GetAllAsync(productParams);
            var totalitem = await   _uow.ProductRepository.CountAsync();
            var result = _mapper.Map<List<ProductDto>>(res);
            return Ok(new Pagination<ProductDto>(productParams.PageNumber,productParams.Pagesize,totalitem, result));
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _uow.ProductRepository.GetByIdAsync(id, x=>x.Category);
            if(res == null)
            {
                return NotFound(new BaseCommonResponse(404));
            }
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
                        await _uow.ProductRepository.AddAsync(createProductDto);
                        return Ok(res);
                    }
                    return BadRequest(createProductDto);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("update-exiting-product/{id}")]
        public async Task<ActionResult> Put (int id, [FromForm]UploadProductDto uploadProductDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.UpdateAsync(id, uploadProductDto);
                    return res ? Ok(uploadProductDto) : BadRequest(res);
                }
                return BadRequest(uploadProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleted-exiting-product/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                   if(ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.DeleteAsync(id);
                    return res ? Ok(res) : BadRequest(res);
                }
                   return NotFound($"this id={id} not found");
            }   
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
