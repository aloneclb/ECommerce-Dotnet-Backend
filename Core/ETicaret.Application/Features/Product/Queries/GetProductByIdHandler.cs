using ETicaret.Application.Dtos.Product;
using ETicaret.Application.Features.Product.Requests;
using ETicaret.Application.Features.Product.Responses;
using ETicaret.Application.Repositories.Product;
using ETicaret.Application.Repositories.ProductImages;
using ETicaret.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Product.Queries;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageReadRepository _productImageReadRepository;


    public GetProductByIdHandler(IProductReadRepository productReadRepository,
        IProductImageReadRepository productImageReadRepository)
    {
        _productReadRepository = productReadRepository;
        _productImageReadRepository = productImageReadRepository;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        // var product = await _productReadRepository
        //     .AsQueryable()
        //     .Include(x => x.Images)
        //     .Select(x => new ProductDto()
        //     {
        //         Id = x.Id,
        //         Name = x.Name,
        //         Stock = x.Stock,
        //         UpdatedAt = x.UpdatedAt,
        //         CreatedAt = x.CreatedAt,
        //         Images = x.Images
        //     })
        //     .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        var examProduct = await (
            from prod in _productReadRepository.Table
            where prod.Id == request.Id
            join img in _productImageReadRepository.Table
                on prod.Id equals img.ProductId into productImages
            select new ProductDto()
            {
                Id = prod.Id,
                Name = prod.Name,
                Stock = prod.Stock,
                UpdatedAt = prod.UpdatedAt,
                CreatedAt = prod.CreatedAt,
                Images = productImages
            }).FirstAsync(cancellationToken: cancellationToken);

        //
        // var deneme = await (from prod in _productReadRepository.AsQueryable().Where(x => x.Id == request.Id)
        //     from img in _productImageReadRepository.AsQueryable().DefaultIfEmpty().Where(x => x.ProductId == prod.Id)
        //     select new
        //     {
        //         prod,
        //         img
        //     }).FirstAsync(cancellationToken: cancellationToken);

        var query = from prod in _productReadRepository.Table
            join img in _productImageReadRepository.Table
                on prod.Id equals img.ProductId into grouping
            from img in grouping.DefaultIfEmpty()
            select new
            {
                prod,
                img
            };


        return new GetProductByIdResponse()
        {
            Product = examProduct
        };
    }
}