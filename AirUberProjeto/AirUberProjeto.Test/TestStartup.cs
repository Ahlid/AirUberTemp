using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Models;
using AirUberProjeto.Test.Mocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;

namespace AirUberProjeto.Test
{
    using Microsoft.AspNetCore.Hosting;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //services.ReplaceSingleton<UserManager<ApplicationUser>,UserManagerMock>();
            services.ReplaceSingleton<UserManager<ApplicationUser>, UserManagerMock2>();
        }
    }
}
