using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TechClub.Common;

namespace TechClub.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files)
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string[] fileNames;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    fileNames = new string[files.Count];
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/Uploads/"));
                        if (!dir.Exists)
                        {
                            dir.Create();
                        }

                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);

                        fileNames[i] = file.FileName;



                    }

                    SendEmailToAdmin(fileNames); //TODO: This is blocking the view loading! 


                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        public void SendEmailToAdmin(string[] names)
        {
            string downloadLinks = "";
            string smtpAddress = "smtp-mail.outlook.com"; // smtp for outlook
            int portNumber = 587;
            bool enableSSL = true;
            for (int i = 0; i < names.Count(); i++)
            {
                downloadLinks += "File " + i + ": http://msptechclub.azurewebsites.net/Download.ashx?file=Uploads/" 
                    + names[i] 
                    + " " 
                    + "<br>";
            }
            string emailFrom = "kinani95@outlook.com"; // write yours
            string password = Credentials.MyEmailPSWD;
            string emailTo = "kinani95@outlook.com";
            string subject = "New upload";
            string body = "Hello Big Brother, " + "<br>"
                + "New file(s) have been submitted to you."
                + "<br>"
                + " Download link(s): " 
                + "<br>"
                + downloadLinks;
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}