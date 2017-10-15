using System.IO;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class AboutUsView : IView
    {
	    public string View()
	    {
			var result = File.ReadAllText(@".\Application\Resources\aboutus.html");
		    return result;
		}
    }
}