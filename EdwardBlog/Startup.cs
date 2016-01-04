using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EdwardBlog.Startup))]
[assembly:log4net.Config.XmlConfigurator(ConfigFile="Web.config",Watch=true)]
namespace EdwardBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
