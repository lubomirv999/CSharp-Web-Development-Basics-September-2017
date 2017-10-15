using System.IO;
using WebServer.Application.Models;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class CalculatorView : IView
    {
	    public string View()
	    {
		    var calc = string.Empty;
		    var result = File.ReadAllText(@".\Application\Resources\calculator.html");

		    calc = Calculator.Result;
		    result = result.Replace("<!--...-->", calc);

		    return result;
		}
    }
}