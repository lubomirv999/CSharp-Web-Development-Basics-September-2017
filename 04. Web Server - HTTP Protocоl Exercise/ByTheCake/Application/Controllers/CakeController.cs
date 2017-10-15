using System.Collections.Generic;
using WebServer.Application.Models;
using WebServer.Application.Views;
using WebServer.Server.Enums;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.Application.Controllers
{
    public class CakeController
    {
	    public IHttpResponse AddCakeGet()
	    {
		    return new ViewResponse(HttpStatusCode.OK, new AddCakeView());
	    }

	    public IHttpResponse AddCakePost(string name, string price)
	    {
		    
		    if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(price))
		    {
				var castPriceToDecimal = decimal.Parse(price);
			    var cake = new Cake(name, castPriceToDecimal);
			    CakeList.Add(cake);
			}
			
		    return new RedirectResponse("/add");
	    }

	    public IHttpResponse SearchGet(IDictionary<string, string> queryParams)
	    {
		    var cake = string.Empty;

		    if (queryParams.ContainsKey("criteria"))
		    {
			    cake = queryParams["criteria"];
		    }
		    return new ViewResponse(HttpStatusCode.OK,new SearchCakeView(cake) );
	    }
	}
}