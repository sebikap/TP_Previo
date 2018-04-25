using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TP_Previo.Startup))]
namespace TP_Previo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
