using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace PropertyApp.Api.IntegrationTests.Helpers
{
    public static class HttpContentHelper
    {
        public static HttpContent ToMultipartFormDataContent(this object obj)
        {
            var objType= obj.GetType();
            var properties= objType.GetProperties();

            var multipartFormContent = new MultipartFormDataContent();

            foreach (var property in properties)
            {
                if(Nullable.GetUnderlyingType(property.PropertyType) == null &&  property.PropertyType.Name != "IFormFile")
                {
                    var propValue = property.GetValue(obj);
                    var propType = propValue?.GetType();
                    if ((propType == typeof(string) || propType.IsPrimitive) && propValue is not null)
                    {
                        multipartFormContent.Add(new StringContent(propValue.ToString()), property.Name);
                    }
                }
                else if(Nullable.GetUnderlyingType(property.PropertyType) == typeof(byte))
                {
                    var propValue = property.GetValue(obj);
                    if (propValue is null) continue;
                    multipartFormContent.Add(new StringContent(propValue.ToString()), property.Name);

                }
                
            }

            return multipartFormContent;
            
        }
        public static HttpContent toJsonContent(this object obj)
        {
          var jsonToString= JsonConvert.SerializeObject(obj);

          return new StringContent(jsonToString, UnicodeEncoding.UTF8, "application/json");

        }
    }

}


