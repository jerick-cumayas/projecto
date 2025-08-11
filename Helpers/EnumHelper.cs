using Microsoft.AspNetCore.Mvc.Rendering;

public static class EnumHelper
{
    public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new SelectListItem { Text = e.ToString(), Value = e.ToString() });
    }
}