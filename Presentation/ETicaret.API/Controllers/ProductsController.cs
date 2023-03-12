using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.Dtos.Product;
using ETicaret.Application.Features.Product.Requests;
using ETicaret.Application.Repositories.Customer;
using ETicaret.Application.Repositories.Product;
using ETicaret.Application.Services;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ETicaretDbContext _dbContext;
    private readonly IProductWriteRepository _productWriteRepository;

    private readonly IProductReadRepository _productReadRepository;

    // private readonly IFileService _imageService;
    private readonly IStorageService _storageService;

    // Mediatr
    private readonly IMediator _mediator;

    public ProductsController(
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository,
        ETicaretDbContext dbContext,
        IStorageService storageService,
        IMediator mediator)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _dbContext = dbContext;
        _storageService = storageService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
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
    public async Task<IActionResult> Upload(List<IFormFile> images)
    {
        var product =
            await _productReadRepository.GetSingleAsync(x =>
                x.Id == Guid.Parse("b337002d-2775-4214-39eb-01db1e92354c"));
        foreach (var image in images)
        {
            var pathname = await _storageService.UploadAsync("deneme", image);
            Console.WriteLine(pathname);

            await _dbContext.ProductImages.AddAsync(new ProductImage()
            {
                ProductId = product!.Id,
                FileUrl = pathname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await _dbContext.SaveChangesAsync();
        }
        // example

        return Ok("ok");
    }
}