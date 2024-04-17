using Application.Products.CreateProduct;
using Presentation.Contracts.Products;

namespace Presentation.Controllers;

public sealed class ProductController : ApiController
{
    public ProductController(ISender sender) : base(sender)
    {
    }

    [HttpPost(Routers.Product.Create)]
    public async Task<IActionResult> CreateProductAsync(
       [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var createProductCommand = request.Adapt<CreateProductCommand>();

        var result = await Sender.Send(createProductCommand, cancellationToken);

        return result.MapActionResult(
                id => Ok(id),
                BadRequest);
    }
}
