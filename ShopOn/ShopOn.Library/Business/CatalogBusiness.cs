using AutoMapper;
using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Cataloging;
using ShopOn.Library.Exceptions;
using ShopOn.Library.Models.Cataloging;
using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Library.Business;

public class CatalogBusiness(IRepository<Category> repository,
    IRepository<Product> repositoryProduct,
    IGuidProvider guidProvider,
    IMapper mapper) : ICatalogBusiness
{
    // Category
    public async Task<CategoryDto[]> GetCategories(CancellationToken token = default)
    {
        var categories = await repository.GetAll(token);
        IEnumerable<CategoryDto> categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);
        CategoryDto[] categoryDtoArray = categoriesDto.ToArray();

        return categoryDtoArray;
    }

    public async Task<CategoryDto> GetCategory(string categoryId, CancellationToken token = default)
    {
        Category? category = await repository.Get(
            x => x.ExtGuid == categoryId,
            tracked: false,
            token: token);
        CategoryDto categoryDto = mapper.Map<CategoryDto>(category);
        return categoryDto;
    }

    public async Task AddCategory(CategoryDto categoryToAdd, CancellationToken token = default)
    {
        Category dbCategory = mapper.Map<Category>(categoryToAdd);
        dbCategory.ExtGuid = guidProvider.GetGuid().ToString();
        await repository.Add(dbCategory, token);
        await repository.SaveChanges(token);
    }

    public async Task UpdateCategory(CategoryDto categoryToUpdate, CancellationToken token = default)
    {
        CategoryDto categoryDto = await GetCategory(categoryToUpdate.ExtGuid, token);

        if (categoryDto is null)
            throw new EntityNotFoundException($"Category ({categoryToUpdate.ExtGuid}) is not found");

        Category dbCategory = mapper.Map<Category>(categoryDto);

        dbCategory.Image = categoryToUpdate.Image;
        dbCategory.Name = categoryToUpdate.Name;

        repository.Update(dbCategory);
        await repository.SaveChanges(token);
    }

    public async Task RemoveCategory(string categoryId, CancellationToken token = default)
    {
        Category? category = await repository.Get(
            x => x.ExtGuid == categoryId,
            token: token);

        if (category is null)
            throw new EntityNotFoundException($"Category ({categoryId}) is not found");

        repository.Delete(category);
        await repository.SaveChanges(token);
    }

    // Product
    public async Task<ProductDto[]> GetProducts(CancellationToken token = default)
    {
        var products = await repositoryProduct.GetAll(token);
        IEnumerable<ProductDto> productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
        ProductDto[] productDtoArray = productsDto.ToArray();

        return productDtoArray;
    }

    public async Task<ProductDto> GetProduct(string productId, CancellationToken token = default)
    {
        Product? product = await repositoryProduct.Get(
            x => x.ExtGuid == productId,
            tracked: false,
            token: token);
        ProductDto productDto = mapper.Map<ProductDto>(product);
        return productDto;
    }

    public async Task AddProduct(ProductDto productToAdd, CancellationToken token = default)
    {
        Product dbProduct = mapper.Map<Product>(productToAdd);
        dbProduct.ExtGuid = guidProvider.GetGuid().ToString();
        await repositoryProduct.Add(dbProduct, token);
        await repositoryProduct.SaveChanges(token);
    }

    public async Task UpdateProduct(ProductDto productToUpdate, CancellationToken token = default)
    {
        ProductDto productDto = await GetProduct(productToUpdate.ExtGuid, token);

        if (productDto is null)
            throw new EntityNotFoundException($"Product ({productToUpdate.ExtGuid}) is not found");

        Product dbProduct = mapper.Map<Product>(productDto);
        
        dbProduct.Name = productToUpdate.Name;
        dbProduct.Description = productToUpdate.Description;
        dbProduct.Image = productToUpdate.Image;
        dbProduct.Category = productToUpdate.Category;
        dbProduct.Price = productToUpdate.Price;
        dbProduct.Amount = productToUpdate.Amount;

        repositoryProduct.Update(dbProduct);
        await repositoryProduct.SaveChanges(token);
    }

    public async Task RemoveProduct(string productId, CancellationToken token = default)
    {
        Product? product = await repositoryProduct.Get(
            x => x.ExtGuid == productId,
            token: token);

        if (product is null)
            throw new EntityNotFoundException($"Product ({productId}) is not found");

        repositoryProduct.Delete(product);
        await repositoryProduct.SaveChanges(token);
    }
}
