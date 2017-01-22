using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Test
{
    using Microsoft.AspNetCore.Hosting;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }
    }
}
