using ETicaret.Application.Abstractions;
using ETicaret.Application.Dtos.Product;
using ETicaret.Application.Repositories.Customer;
using ETicaret.Application.Repositories.Product;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IImageService _imageService;

    public ProductsController(ICustomerReadRepository customerReadRepository,
        ICustomerWriteRepository customerWriteRepository, IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository, IImageService imageService)
    {
        _customerReadRepository = customerReadRepository;
        _customerWriteRepository = customerWriteRepository;
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ProductListRequest request)
    {
        // var customer = await _customerReadRepository.GetByIdAsync(new Guid("370a3f54-56e8-4cd2-d2f4-08db18641380"));
        // customer.Name = "asdasd";
        // _customerWriteRepository.Update(customer);
        //
        // await _customerWriteRepository.SaveChanges();

        // await _productWriteRepository.AddAsync(new Product()
        // {
        //     Name = "deneme",
        //     Price = 15611,
        //     Stock = 4,
        // });
        //
        // await _customerWriteRepository.SaveChanges();
        //
        var products = await _productReadRepository.GetWhere(x => true)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.CreatedAt,
                p.UpdatedAt,
            })
            .OrderBy(x => x.Name)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        //
        // var products = await (from item in _productReadRepository.GetWhere(x => true)
        //     select new
        //     {
        //         item.Id,
        //         item.Name,
        //         item.Stock,
        //         item.CreatedAt,
        //         item.UpdatedAt,
        //     })
        //     .Skip(request.Page * request.PageSize)
        //     .Take(request.PageSize)
        //     .ToListAsync();

        var totalCount = _productReadRepository.GetWhere(x => true).Count();

        return Ok(new
        {
            totalCount,
            products
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };
        var result = await _productWriteRepository.AddAsync(product);
        await _productWriteRepository.SaveChanges();

        return result ? Ok() : BadRequest();
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
        foreach (var image in images)
        {
            var pathname = await _imageService.Upload("deneme", image);
            Console.WriteLine(pathname);
        }
        return Ok("ok");
    }
}