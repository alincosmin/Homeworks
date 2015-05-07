using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebPingPong.Startup))]
namespace WebPingPong
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}