using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Diagnostics;

namespace ToDoApi
{
    public class Program
    {
        public static readonly string CONNECTION_STRING = "data source=C:\\Users\\thanh\\Desktop\\ToDoDb.db";

        static Timer timer;
        public static void Main(string[] args)
        {
            Debug.WriteLine("wtf is it");
            Worker.Start().GetAwaiter().GetResult();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();

            //sms and email service
        }
    }
}
