using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Obsidian.Models
{
    public class JsonDeserializeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            using (var sr = new StreamReader(request.Body))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    var model = serializer.Deserialize<AuthorizationCodeGrantRequestModel>(jr);
                    if (model != null)
                    {
                        bindingContext.Result = ModelBindingResult.Success(model);
                    }
                    else
                    {
                        bindingContext.Result = ModelBindingResult.Failed();
                    }
                    return Task.FromResult(0);
                }
            }
        }
    }
}