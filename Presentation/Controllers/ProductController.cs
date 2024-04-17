using Application.Products.CreateProduct;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Products;
using Presentation.Router;

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
