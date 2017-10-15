using WebServer.Application.Models;
using WebServer.Application.Views;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;
using HttpStatusCode = WebServer.Server.Enums.HttpStatusCode;

namespace WebServer.Application.Controllers
{
    public class HomeController
	{
		public IHttpResponse Index()
		{
			return new ViewResponse(HttpStatusCode.OK, new HomeIndexView());
		}

		public IHttpResponse AboutUs()
		{
			return new ViewResponse(HttpStatusCode.OK, new AboutUsView());
		}

		public IHttpResponse CalculatorGet()
		{
			return new ViewResponse(HttpStatusCode.OK, new CalculatorView() );
		}

		public IHttpResponse CalculatorPost(string firstNumber, string secondNumber, string sign)
		{
			Calculator.Validate(decimal.Parse(firstNumber), decimal.Parse(secondNumber), sign);
			return new RedirectResponse("/calculator");
		}

		public IHttpResponse LoginGet()
		{
			return new ViewResponse(HttpStatusCode.OK, new LoginView());
		}

		public IHttpResponse LoginPost(string username, string password)
		{
			LoginData.SetUp(username, password);
			
			return new RedirectResponse("/login");
		}
	}
}