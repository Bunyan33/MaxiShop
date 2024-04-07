using MaxiShop.Domine.Contracts;
using MaxiShop.Domine.Models;
using MaxiShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Update(Category category)
        {
            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
