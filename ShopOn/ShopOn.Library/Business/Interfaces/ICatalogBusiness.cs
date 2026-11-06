using ShopOn.Library.Models.Cataloging;

namespace ShopOn.Library.Business.Interfaces;

public interface ICatalogBusiness
{
    // Category
    public Task<CategoryDto[]> GetCategories(CancellationToken token = default);
    public Task<CategoryDto> GetCategory(string categoryId, CancellationToken token = default);
    public Task AddCategory(CategoryDto categoryToAdd, CancellationToken token = default);
    public Task UpdateCategory(CategoryDto categoryToUpdate, CancellationToken token = default);
    public Task RemoveCategory(string categoryId, CancellationToken token = default);

    // Product
    public Task<ProductDto[]> GetProducts(CancellationToken token = default);
    public Task<ProductDto> GetProduct(string productId, CancellationToken token = default);
    public Task AddProduct(ProductDto productToAdd, CancellationToken token = default);
    public Task UpdateProduct(ProductDto productToUpdate, CancellationToken token = default);
    public Task RemoveProduct(string categoryId, CancellationToken token = default);

    // CatalogItem
    public Task<CatalogItemDto[]> GetCatalogItems(CancellationToken token = default);
    public Task<CatalogItemDto> GetCatalogItem(string catalogItemId, CancellationToken token = default);
    public Task AddCatalogItem(CatalogItemDto catalogItemToAdd, CancellationToken token = default);
    public Task UpdateCatalogItem(CatalogItemDto catalogItemToUpdate, CancellationToken token = default);
    public Task RemoveCatalogItem(string catalogItemId, CancellationToken token = default);
}
