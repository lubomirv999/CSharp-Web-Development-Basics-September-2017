using System.IO;
using WebServer.Application.Models;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class AddCakeView : IView
    {
	    public string View()
	    {
		    var result = File.ReadAllText(@".\Application\Resources\add.html");
			var cakes = CakeList.AllCakes();
		    result = result.Replace("<!--...-->", cakes);

			return result;
	    }
    }
}