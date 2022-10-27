using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tools.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var resutl = new List<string>();
            foreach (var item in modelState.Values)
                resutl.AddRange(item.Errors.Select(error => error.ErrorMessage));

            return resutl;
        }
    }
}