using System.Text.Json;

namespace LibraryManager.Entities.Extensions;

public static class EntityExtensions
{
    public static T? Copy<T>(this T ItemToCopy) 
        where T : IEntity
    {
        var json = JsonSerializer.Serialize(ItemToCopy);
        return JsonSerializer.Deserialize<T>(json);
    }
}

