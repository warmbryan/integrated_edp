
/* Unmerged change from project '10_App_Code'
Before:
using System;
using System.Threading.Tasks;
using Microsoft.Owin;
After:
using Microsoft.Owin;
using Owin;
using System;
*/
using Microsoft.Owin;
using Owin;

using System;
using System.IO;

[assembly: OwinStartup(typeof(EDP_Project.Startup))]

namespace EDP_Project
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
