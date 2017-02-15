using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackUndertow.Startup))]
namespace StackUndertow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
