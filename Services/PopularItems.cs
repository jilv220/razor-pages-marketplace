using Microsoft.EntityFrameworkCore;
using Project.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PopularItemsService
{
    private readonly ApplicationDbContext _dbContext;

    public PopularItemsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PopularItems>> GetPopularItemsAsync()
    {
        return await (from order in _dbContext.Orders
                      join product in _dbContext.Products
                      on order.ProductId equals product.Id
                      group order by new { product.Id, product.Name, product.Description, product.ImageUri, product.Type, product.UnitPrice, product.QtyInStock } into g
                      orderby g.Sum(o => o.Quantity) descending
                      select new PopularItems
                      {
                          ProductId = g.Key.Id,
                          ProductName = g.Key.Name,
                          Description = g.Key.Description,
                          ImageUri = g.Key.ImageUri,
                          QuantitySold = g.Sum(o => o.Quantity),
                          Type = g.Key.Type,
                          UnitPrice = g.Key.UnitPrice,
                          QtyInStock = g.Key.QtyInStock
                      }).Take(10).ToListAsync();
    }
}
