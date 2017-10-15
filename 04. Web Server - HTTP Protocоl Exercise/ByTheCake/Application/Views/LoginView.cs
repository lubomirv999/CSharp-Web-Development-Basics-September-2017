using System.IO;
using WebServer.Application.Models;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class LoginView : IView
    {
	    public string View()
	    {
			var login = string.Empty;
		    var result = File.ReadAllText(@".\Application\Resources\login.html");

		    login = LoginData.Result;
		    result = result.Replace("<!--...-->", login);

		    return result;
		}
    }
}