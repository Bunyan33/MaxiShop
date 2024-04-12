using MaxiShop.Domine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Domine.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task UpdateAsync(Product product);

        Task<IEnumerable<Product>> GetAllProductAsync();

        Task<Product> GetProductByIdAsync(int id);

    }
}
