using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Display.Utilities.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
        Task<string> RenderToStringAsync(string viewName, object model, ModelStateDictionary modelStateDictionary);
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
    }
}
