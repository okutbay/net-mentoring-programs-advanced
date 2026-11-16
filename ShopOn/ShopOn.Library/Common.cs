using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library;

public static class Common
{
    public static Dictionary<string, string?[]> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, true);

        return results
            .SelectMany(r => r.MemberNames.Select(m => new { m, r.ErrorMessage }))
            .GroupBy(x => x.m)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}
