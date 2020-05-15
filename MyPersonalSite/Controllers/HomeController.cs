using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;

namespace MyPersonalSite.Controllers
{
    public class HomeController : Controller
    {
        MyPersonalEntities1 db = new MyPersonalEntities1();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]

        public ActionResult Contact(ContactForm contact, string savestaus = null)
        {
            string Status = "NA";
            ContactForm ct = new ContactForm();

            try
            {
                ct.Name = contact.Name;
                ct.Email = contact.Email;
                ct.Mobile = contact.Mobile;
                ct.Message = contact.Message + " " + DateTime.Now;
               

                db.ContactForms.Add(ct);

                int result = db.SaveChanges();
                if (result == 1)
                {


                    //var msg = "<span style='font-weight:bold;color:#900C3F;text-decoration:underline;font-size:Large'>New Contact</span>" +
                    //    "<br><br><span style='font-weight:bold'>Name :</span>" + " " + "<span style='color:#5D311B;font-weight:bold'>" + contact.Name + "</span>" +
                    //    "<br><span style='font-weight:bold'> Email :</span>" + " " + "<span style='color:#5D311B;font-weight:bold'>" + contact.Email + "</span>" +
                    //     "<br/><span style='font-weight:bold'>Mobile :</span> " + "<span style='color:#5D311B;font-weight:bold'>" + contact.Mobile + "</span>" +
                    //   "<br/><span style='font-weight:bold'>Message :</span>" + "<span style='color:#5D311B;font-weight:bold'>" + contact.Message + "</span>" +
                    //     "<br/><br/>Thank you for Contacting me" + "<br><span style='color:#2867DE;font-weight:bold;font-size:medium'>Personal Contact Support !</span>";

                    //var sub = contact.Name;
                    var name = contact.Name;
                    var mobile = contact.Mobile;
                    //string res = SendEmail("vijaydwivedi125@gmail.com", sub, msg, name);

                    Status = "Succeeded";

                    sendSMS(mobile.ToString(), name.ToString());

                    //TempData["example"] = Status;
                    //return RedirectToAction("ThankYou", "Home", new { savestatus = Status });

                }
                else
                {
                    Status = "UnSucceeded";
                }
            }
            catch(Exception ex)
            {
                Status = "Unsucceeded " + ex;
            }

          






            TempData["example"] = Status;


            //ViewBag.Status = Status;


            return RedirectToAction("Contact", "Home");


        }

        public ActionResult Education()
        {


            return View();
        }

        public ActionResult Experience()
        {


            return View();
        }

        public ActionResult Skill()
        {


            return View();
        }

        public ActionResult Project()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        // Method for Sending Email

        public string SendEmail(string EmailId = null, string subject = null, string msg = null, string Name = null, string Email = null)
        {
            var senderEmail = new MailAddress("vijaywebsuport@gmail.com", "Vijay Web Contact");
            var receiverEmail = new MailAddress(EmailId, "Receiver");
            var password = "2020@vijaypd";
            var sub = subject;
            var body = msg;
            var Cname = Name;
            var cemail = Email;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body

            })
            {
                mess.IsBodyHtml = true;
                smtp.Send(mess);
            }
            return "Done";
        }


        // Method for Sending text Message

        public string sendSMS(string number = null, string Name = null)
        {
            String message = HttpUtility.UrlEncode(" Hello " + Name +   " Thank You for Contacting me." + " Regards : Vijay Dwivedi");
            //String message = HttpUtility.UrlEncode(" Hello M.R. Videh Tiwari  Today your children Virat Tiwari Class - 2 is absent in the classroom . Regards : KPS public school Raipur ");

            using (var wb = new WebClient())
            {

                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "qgiCIoNTTDM-0oN4B5d8kJ9yNq61qARJ877VzgYK2M"},
                {"numbers" , number},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }



    }
}