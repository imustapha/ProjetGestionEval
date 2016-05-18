using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetGestionEval.Startup))]
namespace ProjetGestionEval
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
