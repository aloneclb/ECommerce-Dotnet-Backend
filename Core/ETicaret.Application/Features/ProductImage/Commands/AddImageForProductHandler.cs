using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.Features.ProductImage.Requests;
using ETicaret.Application.Repositories.Product;
using ETicaret.Application.Repositories.ProductImages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.ProductImage.Commands;

public class AddImageForProductHandler : IRequestHandler<AddImageForProductRequest, bool>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageWriteRepository _productImageWriteRepository;
    private readonly IStorageService _storageService;

    public AddImageForProductHandler(IProductReadRepository productReadRepository, IStorageService storageService,
        IProductImageWriteRepository productImageWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _productImageWriteRepository = productImageWriteRepository;
    }

    public async Task<bool> Handle(AddImageForProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetWhere(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);

        if (product == null)
            return false;

        if (request.Images == null)
            return false;

        foreach (var image in request.Images)
        {
            var pathname = await _storageService.UploadAsync("deneme", image);

            await _productImageWriteRepository.AddAsync(new Domain.Entities.ProductImage()
            {
                ProductId = product.Id,
                FileUrl = pathname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await _productImageWriteRepository.SaveChanges();
        }

        return true;
    }
}