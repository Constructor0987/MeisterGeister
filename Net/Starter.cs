using Owin;

namespace MeisterGeister.Net.Web
{
    class Starter
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
