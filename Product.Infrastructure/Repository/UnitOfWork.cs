using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Product.Core.Interface;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
    
        public UnitOfWork(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper)
        {
           _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            CateogryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
        }

        public IcateogryRepository CateogryRepository { get; }

        public IProductRepository ProductRepository { get; }
    }
}
