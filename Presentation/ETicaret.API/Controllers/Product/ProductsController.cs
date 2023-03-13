using ETicaret.Application.Features.Product.Requests;
using ETicaret.Application.Features.ProductImage.Requests;
using ETicaret.Application.Repositories.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers.Product;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IMediator _mediator; // Mediatr

    public ProductsController(
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository,
        IMediator mediator)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("{Id:required}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _mediator.Send(new GetProductByIdRequest { Id = id });
        return Ok(response.Product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Result ? Ok() : BadRequest();
    }

    [HttpDelete]
    [Route("{id:required}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var entity = await _productReadRepository.GetByIdAsync(id);
        if (entity == null)
            return BadRequest("Bulunamadı");

        var result = _productWriteRepository.Remove(entity);
        await _productWriteRepository.SaveChanges();
        return result ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload(List<IFormFile> images, Guid id)
    {
        var request = new AddImageForProductRequest()
        {
            Id = id,
            Images = images
        };
        var result = await _mediator.Send(request);

        return result
            ? Ok("başarılı")
            : BadRequest("başarısız");
    }
}