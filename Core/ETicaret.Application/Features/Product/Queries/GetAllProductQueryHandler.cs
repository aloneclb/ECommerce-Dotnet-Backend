using ETicaret.Application.Features.Product.Requests;
using ETicaret.Application.Features.Product.Responses;
using ETicaret.Application.Repositories.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Product.Queries;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        // Todo: Pagination hata verebilir. Total Counta göre hesaplama yapıp veriyi o şekilde getirmek gerekir.
        // total count >= (pagesize * page olmalı) + pagesize 
        // yukarıdaki şart her zaman sağlanmalı
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
                    .ToListAsync(cancellationToken: cancellationToken);
        
        var totalCount = _productReadRepository.GetWhere(x => true).Count();

        return new GetAllProductQueryResponse
        {
            TotalCount = totalCount,
            Products = products
        };
    }
}


// var x = await _dbContext.ProductImages.Include(x => x.Product).FirstAsync();
// Console.WriteLine(x);

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
// var products = await _productReadRepository.GetWhere(x => true)
//     .Select(p => new
//     {
//         p.Id,
//         p.Name,
//         p.Stock,
//         p.CreatedAt,
//         p.UpdatedAt,
//     })
//     .OrderBy(x => x.Name)
//     .Skip(request.Page * request.PageSize)
//     .Take(request.PageSize)
//     .ToListAsync();
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