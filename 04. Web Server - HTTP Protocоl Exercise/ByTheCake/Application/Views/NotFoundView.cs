using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class NotFoundView : IView
    {
	    public string View()
	    {
		    return "The requested page wasn't found!";
	    }
    }
}