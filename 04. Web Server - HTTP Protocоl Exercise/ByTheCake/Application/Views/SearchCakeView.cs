using System.IO;
using WebServer.Application.Models;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class SearchCakeView : IView
    {
	    public SearchCakeView(string criteria)
	    {
		    this.criteria = criteria;
	    }

	    private string criteria;
	    public string View()
	    {
		    var cakes = string.Empty;
		    var result = File.ReadAllText(@".\Application\Resources\search.html");

		    if (string.IsNullOrEmpty(criteria))
		    {
			    cakes = "No records.";
		    }
		    else
		    {
				cakes = CakeList.Search(criteria);
			}
		   
		    result = result.Replace("<!--...-->", cakes);

		    return result;
		}
    }
}