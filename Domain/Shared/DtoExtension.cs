using System.Reflection;

namespace Domain.Shared;

public static class DtoExtension
{
    public static TDto CreateDto<TDto>(this object entity, PropertyInfo[]? properties)
    {
        properties ??= typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var dto = Activator.CreateInstance<TDto>();
        foreach (var property in properties)
        {
            var productProperity = entity.GetType().GetProperty(property.Name);
            if (productProperity is { })
            {
                var value = productProperity.GetValue(entity);
                property.SetValue(dto, value);
            }
        }
        return dto;
    }
}