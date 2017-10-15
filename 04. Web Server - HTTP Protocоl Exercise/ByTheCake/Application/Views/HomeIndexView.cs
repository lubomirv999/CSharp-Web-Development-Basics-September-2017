using System.IO;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class HomeIndexView : IView
	{
		public string View()
		{
			var result = File.ReadAllText(@".\Application\Resources\index.html");
			return result;
		}
	}
}