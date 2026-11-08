using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Entities.Cataloging;
using ShopOn.Library.Models.Cataloging;

namespace ShopOn.Services.CatalogService.Extensions;

internal static partial class EndpointRegistrationExtensions
{
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        var categoriesGroup = app.MapGroup("categories").WithTags("Categories");

        // GET /categories
        categoriesGroup.MapGet("",
            async (ICatalogBusiness catalogBusiness, ILogger<CategoryDto> logger, CancellationToken token) =>
                await catalogBusiness.GetCategories(token)
        );

        // GET /categories/{categoryid}
        categoriesGroup.MapGet("/{categoryid}",
            async (string categoryid, ICatalogBusiness catalogBusiness, ILogger<CategoryDto> logger, CancellationToken token) =>
                await catalogBusiness.GetCategory(categoryid, token)
        );

        // POST New Category
        categoriesGroup.MapPost("",
            async (CategoryDto categoryDto, ICatalogBusiness catalogBusiness, ILogger<CategoryDto> logger, CancellationToken token) =>
            {
                await catalogBusiness.AddCategory(categoryDto, token);
                return Results.Created($"/categories/{categoryDto.ExtGuid}", categoryDto);
            }).ExcludeFromDescription();

        // PUT Update Category
        categoriesGroup.MapPut("",
            async (CategoryDto categoryDto, ICatalogBusiness catalogBusiness, ILogger<CategoryDto> logger, CancellationToken token) =>
            {
                await catalogBusiness.UpdateCategory(categoryDto, token);
                return Results.Ok($"/categories/{categoryDto.ExtGuid}");
            }).ExcludeFromDescription();


        // DELETE /categories/{id} deletes a category
        categoriesGroup.MapDelete("/{categoryid}",
            async (string categoryid, ICatalogBusiness catalogBusiness, ILogger<CategoryDto> logger, CancellationToken token) =>
            {
                await catalogBusiness.RemoveCategory(categoryid, token);
                return Results.Ok(categoryid);
            });
    }

    public static void MapProductsEndpoints(this WebApplication app)
    {
        var productsGroup = app.MapGroup("products").WithTags("Products");

        // GET /products
        productsGroup.MapGet("",
            async (ICatalogBusiness catalogBusiness, ILogger<ProductDto> logger, CancellationToken token) =>
                await catalogBusiness.GetProducts(token)
        );

        // GET /products/{productid}
        productsGroup.MapGet("/{productid}",
            async (string productid, ICatalogBusiness catalogBusiness, ILogger<ProductDto> logger, CancellationToken token) =>
                await catalogBusiness.GetProduct(productid, token)
        );

        // POST New Product
        productsGroup.MapPost("",
            async (ProductDto productDto, ICatalogBusiness catalogBusiness, ILogger<ProductDto> logger, CancellationToken token) =>
            {
                await catalogBusiness.AddProduct(productDto, token);
                return Results.Created($"/products/{productDto.ExtGuid}", productDto);
            }).ExcludeFromDescription();

        // PUT Update Product
        productsGroup.MapPut("",
            async (ProductDto productDto, ICatalogBusiness catalogBusiness, ILogger<ProductDto> logger, CancellationToken token) =>
            {
                await catalogBusiness.UpdateProduct(productDto, token);
                return Results.Ok($"/products/{productDto.ExtGuid}");
            }).ExcludeFromDescription();

        // DELETE /products/{id} deletes a Product
        productsGroup.MapDelete("/{productid}",
            async (string productid, ICatalogBusiness catalogBusiness, ILogger<ProductDto> logger, CancellationToken token) =>
            {
                await catalogBusiness.RemoveProduct(productid, token);
                return Results.Ok(productid);
            });
    }
}
