using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilmBuff.WebMVC.Startup))]
namespace FilmBuff.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
