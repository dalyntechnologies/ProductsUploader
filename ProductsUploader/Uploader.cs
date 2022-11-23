using Newtonsoft.Json;
using ProductsUploader.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductsUploader
{
    public class Uploader
    {
        public async Task UploadDataAsync(ProductUpload productUpload)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    var json = JsonConvert.SerializeObject(productUpload);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync($"{Program.BaseUrl}/v1/api/products/uploads", content);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var view = data;
                    

                }
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}
