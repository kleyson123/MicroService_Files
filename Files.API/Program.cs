﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Files.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 默认配置
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();
            WebHost.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration((hostingContext, _config) =>
                  {
                      _config
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddEnvironmentVariables();
                  })
                  .UseKestrel()
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseStartup<Startup>()
                  .UseUrls(config.GetValue<string>("urls"))
                  .Build()
                  .Run();
        }

    }
}
