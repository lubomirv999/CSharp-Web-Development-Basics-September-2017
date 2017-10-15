namespace WebServer.Application.Views
{
    using Server.Http.Contracts;
    using WebServer.Server;

    public class UserDetailsView : IView
    {
        private Model model;

        public UserDetailsView(Model model)
        {
            this.model = model;
        }

        public string View()
        {
            return
                $"<body>Hello, {this.model["name"]}!</body>";
        }
    }
}