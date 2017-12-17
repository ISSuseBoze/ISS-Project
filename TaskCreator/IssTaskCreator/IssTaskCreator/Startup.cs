using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IssTaskCreator.Startup))]
namespace IssTaskCreator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
