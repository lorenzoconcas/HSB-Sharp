using HSB;
using HSB.Constants;
namespace Runner;


[Binding("/associatedfile1")]
[AssociatedFile("./static/main.html", HTTP_METHOD.GET)]
public class AssociatedFileServletOneMethod : Servlet
{
    public AssociatedFileServletOneMethod(Request req, Response res) : base(req, res)
    {

    }
}
// Test these with postman, curl or custom scripts/pages

[Binding("/associatedfile2")]
[AssociatedFile("./static/main.html", new HTTP_METHOD[] { HTTP_METHOD.GET, HTTP_METHOD.TRACE })]
public class AssociatedFileServletMultipleMethods : Servlet
{
    public AssociatedFileServletMultipleMethods(Request req, Response res) : base(req, res)
    {

    }
}

[Binding("/associatedfile3")]
[AssociatedFile("./static/main.html", "HSBCUSTOMHTTPMETHOD")]
public class AssociatedFileServletCustomMethods : Servlet
{
    public AssociatedFileServletCustomMethods(Request req, Response res) : base(req, res)
    {

    }
}

[Binding("/associatedfile4")]
[AssociatedFile("./static/main.html", new string[] { "GET", "HSBCUSTOMHTTPMETHOD" })] //standard methods are still valid
public class AssociatedFileServletMultipleCustomMethods : Servlet
{
    public AssociatedFileServletMultipleCustomMethods(Request req, Response res) : base(req, res)
    {

    }
}
