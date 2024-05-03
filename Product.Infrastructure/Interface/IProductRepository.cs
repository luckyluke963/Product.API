

using Product.Core.Dto;
using Product.Core.Entities;

namespace Product.Infrastructure.Interface
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        Task<bool> AddAsync(CreateProductDto dto);
    }
}
