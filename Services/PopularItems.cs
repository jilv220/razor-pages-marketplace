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
        return await (from orderDetail in _dbContext.OrderDetails
                      join order in _dbContext.Orders on orderDetail.OrderId equals order.Id
                      join product in _dbContext.Products on orderDetail.ProductId equals product.Id
                      group orderDetail by new { product.Id, product.Name, product.Description, product.ImageUri, product.Type, product.UnitPrice, product.QtyInStock } into g
                      orderby g.Sum(od => od.Quantity) descending
                      select new PopularItems
                      {
                          ProductId = g.Key.Id,
                          ProductName = g.Key.Name,
                          Description = g.Key.Description,
                          ImageUri = g.Key.ImageUri,
                          QuantitySold = g.Sum(od => od.Quantity),
                          Type = g.Key.Type,
                          UnitPrice = g.Key.UnitPrice,
                          QtyInStock = g.Key.QtyInStock
                      }).Take(10).ToListAsync();
    }
}
