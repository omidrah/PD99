using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;


namespace Web
{
    public class Utils
    {
        public static string CreateThumbnail(string OriginalFileFullPath,int width=150,int height=150)
        {            
            string filename = string.Empty;

            if (File.Exists(OriginalFileFullPath))
            {
                Image img = Bitmap.FromFile(OriginalFileFullPath);
                Bitmap bmp = new Bitmap(img);

                bmp = BitmapManipulator.ThumbnailBitmap(bmp, width, height);

                string thumbfilename = Path.GetFileNameWithoutExtension(OriginalFileFullPath) + "_Thumb" + Path.GetExtension(OriginalFileFullPath);
                
                bmp.Save(Path.Combine(Path.GetDirectoryName(OriginalFileFullPath),thumbfilename), System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                bmp.Dispose();
                filename = thumbfilename;
            }
            return filename;
        }
    }

    public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public MyHttpControllerHandler(RouteData routeData): base(routeData)
        {
        }
    }

    public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHttpControllerHandler(requestContext.RouteData);
        }
    }
}