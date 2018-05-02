#region

using Microsoft.Owin;
using Owin;
using PrimeActs.UI;

#endregion

[assembly: OwinStartup(typeof (Startup))]

namespace PrimeActs.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}