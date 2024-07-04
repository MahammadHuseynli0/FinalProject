using NanoNexus.Core.Models;
using NanoNexus.Core.RepositoryAbstracts;
using NanoNexus.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Data.RepositoryConcretes
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
