using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductsUploader.Models;
using System;
using System.Threading.Tasks;

namespace ProductsUploader
{
    internal class Program
    {

        public static string BaseUrl = "";
        public static string ExcelPath = "";
        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

            BaseUrl = config.GetValue<string>("ServiceUrl");
            ExcelPath = config.GetValue<string>("ExcelPath");
           
            ExcelReader reader= new ExcelReader();
            var file=reader.ReadFile();
            
            Task.Run(async () =>
            {
                var uploader = new Uploader();
                var productUpload = new ProductUpload();
                productUpload.Products = file;
                await uploader.UploadDataAsync(productUpload);
            });
             
            Console.ReadLine();
        }
    }
}
