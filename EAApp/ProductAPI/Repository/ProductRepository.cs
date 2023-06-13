using ProductAPI.Data;

namespace ProductAPI;

public interface IProductRepository
{
    List<Product> GetProducts();
    Product? GetById(int id);
    Product Add(Product product);
    Product? Update(int id, Product product);
    Product? Delete(int id);
}
public class ProductRepository : IProductRepository
{

    private readonly ProductDbContext dbContext;

    public ProductRepository(ProductDbContext productDbContext)
    {
        this.dbContext = productDbContext;
    }


    public List<Product> GetProducts()
    {
        return this.dbContext.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return this.dbContext.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product Add(Product product)
    {
        dbContext.Add(product);
        dbContext.SaveChanges();
        return product;
    }

    public Product? Update(int id, Product product)
    {
        var productEntity = GetById(id);
        if (productEntity is null) return null;
        productEntity.Name = product.Name;
        productEntity.Description = product.Description;
        productEntity.Price = product.Price;
        productEntity.ProductType = product.ProductType;

        dbContext.Update(productEntity);
        dbContext.SaveChanges();
        return productEntity;
    }

    public Product? Delete(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return null;
        dbContext.Products.Remove(product);
        return product;
    }
}
