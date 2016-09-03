using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Obsidian.Models
{
    [ModelBinder(BinderType = typeof(AuthorizationCodeGrantRequestModelBinder))]
    public class AuthorizationCodeGrantRequestModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("client_id")]
        public Guid ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }


        public class AuthorizationCodeGrantRequestModelBinder : IModelBinder
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
}