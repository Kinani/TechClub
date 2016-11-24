using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TechClub
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    public class Download : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string file = context.Request.QueryString["file"];

            if (!string.IsNullOrEmpty(file) && File.Exists(context.Server.MapPath(file)))
            {
                context.Response.Clear();
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(file));
                context.Response.WriteFile(context.Server.MapPath(file));
                // Potential after download clean-up  
                context.Response.End();

            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("File not found!");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}