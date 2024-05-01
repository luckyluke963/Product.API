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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork Uow, IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
        }


        [HttpGet("get-all-category")]
        public async Task<ActionResult> Get()
        {
            var all_category = await _uow.CateogryRepository.GetAllAsync();
            if(all_category != null)
            {

                //var res = all_category.Select(x => new CategoryDto
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Description = x.Description,
                //});
                var res =  _mapper.Map<IReadOnlyList<Category>,IReadOnlyList<ListCategoryDto>>(all_category); 
                return Ok(res);
            }
            return BadRequest("Not Found");
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var category = await _uow.CateogryRepository.GetAsync(id);
            if(category == null)
            {
                return BadRequest($"Not found this id = [{id}]");
            }
            return Ok(_mapper.Map<Category, ListCategoryDto>(category));
        }


        [HttpPost("add-new-category")]
        public async Task<ActionResult> Post(CategoryDto categoryDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var res = _mapper.Map<Category>(categoryDto);
                    //var new_category = new Category
                    //{
                    //    Name = categoryDto.Name,
                    //    Description = categoryDto.Description,
                    //};
                    await _uow.CateogryRepository.AddAsync(res);
                    return Ok(res);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //await _uow.CateogryRepository.AddAsync(category);
            //return Ok(category);
        }


        [HttpPost("update-exititng-category-by-id/{id}")]
        public async Task<ActionResult> Put(int id, CategoryDto categoryDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var exiting_cateogry = await _uow.CateogryRepository.GetAsync(id);
                    if(exiting_cateogry != null)
                    {
                        _mapper.Map(categoryDto, exiting_cateogry);
                        //exiting_cateogry.Name = categoryDto.Name;
                        //exiting_cateogry.Description = categoryDto.Description;
                    }
                    await _uow.CateogryRepository.UpdateAsync(id, exiting_cateogry);
                    return Ok(exiting_cateogry);
                }
                return BadRequest($"Category id [{id}] Not Found");
            }
            catch(Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-category-by-id/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var exiting_category = await _uow.CateogryRepository.GetAsync(id);
                if(exiting_category != null)
                {
                    await _uow.CateogryRepository.DeleteAsync(id);
                    return Ok($"This Category [{exiting_category.Name}] Is deleted");
                }
                return BadRequest($"This Category [{exiting_category.Id}] Not Found");
            }
            catch(Exception ex)
            {
                    return BadRequest(ex.Message);
            
            }
        }



    }
}
