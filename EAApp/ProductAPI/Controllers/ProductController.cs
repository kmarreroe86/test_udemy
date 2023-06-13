using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;

namespace ProductAPI;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("/getAll")]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return _repository.GetProducts();
    }

    [HttpGet("{id:Guid}")]
    public ActionResult<Product> GetById([FromRoute] int id)
    {
        var product = _repository.GetById(id);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] Product product)
    {
        var savedWalk = _repository.Add(product);
        return Ok(savedWalk);// change for CreatedAt
    }

    [HttpPut("{id:Guid}")]
    public ActionResult<Product> Update([FromRoute] int id, [FromBody] Product product)
    {
        var walkRegionDto = _repository.Update(id, product);
        if (walkRegionDto == null)
            return NotFound();
        return Ok(walkRegionDto);
    }

    [HttpDelete("{id:Guid}")]
    public ActionResult<Product> DeleteById([FromRoute] int id)
    {
        var productDeleted = _repository.Delete(id);
        return productDeleted is not null ? productDeleted : null;
    }
}
