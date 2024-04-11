using Application.Products.CreateProduct;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Products;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public sealed class ProductController : ApiController
{
    public ProductController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
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
