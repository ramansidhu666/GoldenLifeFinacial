using GoldenLifeFinacial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Text;

namespace GoldenLifeFinacial.Controllers
{
    public class GoldenLifeController : Controller
    {
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult appointment()
        {
            return View();
        }
        public ActionResult financial()
        {
            return View();
        }
        public ActionResult life_insurance()
        {
            return View();
        }
        public ActionResult resp()
        {           
            return View();
        }
        public ActionResult rrsp()
        {
            return View();
        }
        public ActionResult services()
        {
            return View();
        }
        public ActionResult tax_service()
        {
            return View();
        }
        public ActionResult tax_center()
        {
            return View();
        }
        public ActionResult personal_tax()
        {
            return View();
        }
        public ActionResult tax_deadlines()
        {
            return View();
        }
        public ActionResult Under_Constructions()
        {
            return View();
        }
        //
        public ActionResult tfsa()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult sendmail(PersonModel person)
        {
            System.Threading.Thread.Sleep(2000);  /*simulating slow connection*/

            /*Do something with object person*/
            if (person != null)
            {
                SendEmail(person);
            }
            else
            {
                return Json(new { msg = "model empty " });
            }

            return Json(new { msg = "Successfully added " + person.Name });
        }

        [HttpPost]
        public ActionResult GetLatestUpdates(PersonModel person)
        {
            System.Threading.Thread.Sleep(2000);  /*simulating slow connection*/

            /*Do something with object person*/
            if (person != null)
            {
                person.subject = "Need Latest Updates.";
                person.Message = "Please send latest updates on this email.";
                SendEmail(person);
            }
            else
            {
                return Json(new { msg = "model empty " });
            }

            return Json(new { msg = "Successfully added " + person.Name });
        }

        public string SendEmail(PersonModel model)
        {
            var subject = "";
            if (model.subject != null)
            {
                subject = model.subject;
            }           
            else
            {
                subject = "New Client Appointment";
            }


            string Status = "";
            string EmailId = "info@goldenlifesolutions.com";

            //Send mail
            MailMessage mail = new MailMessage();

            string FromEmailID = WebConfigurationManager.AppSettings["RegFromMailAddress"];
            string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];

            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
            int _Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString());
            Boolean _UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseDefaultCredentials"].ToString());
            Boolean _EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSsl"].ToString());
            mail.To.Add(new MailAddress(EmailId));
            mail.From = new MailAddress(FromEmailID);
            mail.Subject = subject;

            string msgbody = "";
            if (model.subject == "Need Latest Updates.")
            {
                msgbody = msgbody + "<p>Email ID : " + model.email + "</p>";
                msgbody = msgbody + "<p>Message : " + model.Message + "</p>";
            }
            else
            {
                msgbody = "<p>Person Name : " + model.Name + "</p>";
                msgbody = msgbody + "<p>Email ID : " + model.email + "</p>";
                msgbody = msgbody + "<p>Appointment Date : " + model.AppointmentDate + "</p>";
                msgbody = msgbody + "<p>Category : " + model.category + "</p>";
                msgbody = msgbody + "<p>Message : " + model.Message + "</p>";
            }
            

            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(msgbody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(msgbody, null, "text/html");

            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);
            // mail.Body = msgbody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp.gmail.com"; //_Host;
            smtp.Port = _Port;
            //smtp.UseDefaultCredentials = _UseDefaultCredentials;
            smtp.Credentials = new System.Net.NetworkCredential(FromEmailID, FromEmailPassword);// Enter senders User name and password
            smtp.EnableSsl = _EnableSsl;
            smtp.Send(mail);

            return Status;
        }

        public ActionResult FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                return Json(captcha.ToString());
            }
            catch
            {
                throw;
            }
        }
    }
}
