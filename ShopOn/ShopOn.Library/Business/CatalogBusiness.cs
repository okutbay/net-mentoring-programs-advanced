using AutoMapper;
using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Cataloging;
using ShopOn.Library.Models.Cataloging;
using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Library.Business;

public class CatalogBusiness(IRepository<Category> repository,
    IRepository<Product> repositoryProduct,
    IRepository<CatalogItem> repositoryCatalogItem,
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
        Product dbProduct = mapper.Map<Product>(productDto);

        dbProduct.Image = productToUpdate.Image;
        dbProduct.Name = productToUpdate.Name;

        repositoryProduct.Update(dbProduct);
        await repositoryProduct.SaveChanges(token);
    }

    public async Task RemoveProduct(string productId, CancellationToken token = default)
    {
        Product? product = await repositoryProduct.Get(
            x => x.ExtGuid == productId,
            token: token);

        repositoryProduct.Delete(product);
        await repositoryProduct.SaveChanges(token);
    }

    // CatalogItem
    public async Task<CatalogItemDto[]> GetCatalogItems(CancellationToken token = default)
    {
        var catalogItems = await repositoryCatalogItem.GetAll(token);
        IEnumerable<CatalogItemDto> catalogItemsDto = mapper.Map<IEnumerable<CatalogItemDto>>(catalogItems);
        CatalogItemDto[] catalogItemDtoArray = catalogItemsDto.ToArray();

        return catalogItemDtoArray;
    }

    public async Task<CatalogItemDto> GetCatalogItem(string catalogItemId, CancellationToken token = default)
    {
        CatalogItem? catalogItem = await repositoryCatalogItem.Get(
            x => x.ExtGuid == catalogItemId,
            token: token);
        CatalogItemDto catalogItemDto = mapper.Map<CatalogItemDto>(catalogItem);
        return catalogItemDto;
    }

    public async Task AddCatalogItem(CatalogItemDto catalogItemToAdd, CancellationToken token = default)
    {
        CatalogItem dbCatalogItem = mapper.Map<CatalogItem>(catalogItemToAdd);
        dbCatalogItem.ExtGuid = guidProvider.GetGuid().ToString();
        await repositoryCatalogItem.Add(dbCatalogItem, token);
        await repositoryCatalogItem.SaveChanges(token);
    }

    public async Task UpdateCatalogItem(CatalogItemDto catalogItemToUpdate, CancellationToken token = default)
    {
        CatalogItemDto catalogItemDto = await GetCatalogItem(catalogItemToUpdate.ExtGuid, token);
        CatalogItem dbCatalogItem = mapper.Map<CatalogItem>(catalogItemDto);

        dbCatalogItem.StatusId = catalogItemToUpdate.StatusId;

        repositoryCatalogItem.Update(dbCatalogItem);
        await repositoryCatalogItem.SaveChanges(token);
    }

    public async Task RemoveCatalogItem(string catalogItemId, CancellationToken token = default)
    {
        CatalogItem? catalogItem = await repositoryCatalogItem.Get(
            x => x.ExtGuid == catalogItemId,
            token: token);

        repositoryCatalogItem.Delete(catalogItem);
        await repositoryCatalogItem.SaveChanges(token);
    }
}
