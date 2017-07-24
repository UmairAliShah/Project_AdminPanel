using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cuffs_And_Cufflinks.Startup))]
namespace Cuffs_And_Cufflinks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
