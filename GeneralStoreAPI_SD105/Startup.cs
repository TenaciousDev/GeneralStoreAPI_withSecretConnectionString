using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GeneralStoreAPI_SD105.Startup))]

namespace GeneralStoreAPI_SD105
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //DotEnv.Load("/App_Data/dotenv.txt");
            ConfigureAuth(app);
        }
    }
}
