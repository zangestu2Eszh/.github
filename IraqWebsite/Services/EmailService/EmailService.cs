using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using IraqWebsite.Data;
using Microsoft.AspNetCore.Hosting;

namespace IraqWebsite.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public Task CustemSendEmailAsync(string email, string subject, string htmlMessage, string link, string cardTitle)
        {
            try
            {
                var setting = _context.EmailSettings.Where(x => x.IsActive == true).FirstOrDefault();
                if (setting == null)
                {
                    return Task.CompletedTask;
                }

                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(setting.Password);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);



                string fromMail = setting.Email;
                string fromPassword = result;

                var html = $"<html lang='en-US'>"
                + "<head>"
                    + "<meta content='text/html; charset = utf-8' http-equiv='Content-Type' />"
                    + "<title> AUIB FM </title >"
                    + "<meta name ='description' content ='AUIB FM News' >"
                    + "<style type ='text/css'>"
                    + "a:hover { text-decoration: underline !important; }" +
                    "</style>" +
                    "</head>" +
                    "<body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>" +
                    "<table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8' style ='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;' >" +
                        "<tr>" +
                            "<td>" +
                                "<table style='background-color: #f2f3f8; max-width:670px; margin:0 auto;' width='100%' border='0' align ='center' cellpadding ='0' cellspacing ='0' >" +
                                    "<tr>" +
                                       "<td style='height: 80px;'>&nbsp;</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style='text-align:center;'>" +
                                            "<a href ='' title ='logo' target ='_blank' >" +
                                                $"<img width ='250' src = 'cid:myImage' title ='AUIB FM Logo' alt ='logo'>" +
                                            "</a>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style='height: 20px;'>&nbsp;</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td>" +
                                            "<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0' style ='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);' >" +
                                                "<tr>" +
                                                    "<td style ='height:40px;'>&nbsp;</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                    "<td style ='padding:0 35px;' >" +
                                                    $"<h1 style ='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'> {cardTitle} </h1>" +
                                                    "<span style ='vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></ span >" +
                                                    "<p style = 'color:#455056; font-size:15px;line-height:24px; margin:0;'>" +
                                                        $"{htmlMessage}" +
                                                        $"{(link != null ? "<br><br>Click the following link and follow the instructions." : "")}" +
                                                    "</p>" +

                                         $"{(link != null ? $"<a href='{link}' style='background:#3567b1;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;'>See Details</a>" : "")}" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style ='height:40px;'>&nbsp;</td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                          "<tr>" +
                             "<td style='height: 20px;'>&nbsp;</td>" +
                          "</tr>" +
                          "<tr>" +
                             "<td style='text-align:center;'>" +
                                 "<p style = 'font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;'>&copy;<strong> AUIB FM </strong></p>" +
                             "</td>" +
                          "</tr>" +
                          "<tr>" +
                             "<td style ='height:80px;'>&nbsp;</td>" +
                         "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "</table>" +
                       $"<p>If you'd rather not receive future emails from AUIB FM Web Services, <a href='http://194.153.103.26/Home/Unsubscribe?email={email}>Unsubscribe here</a></ p>"+
                       "</body>" +
                       "</html>";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.To.Add(new MailAddress(email));
                message.Body = html;
                message.IsBodyHtml = true;

                var path = _webHostEnvironment.WebRootFileProvider.GetFileInfo("images/logo fm 1.png")?.PhysicalPath;
                if(path == null)
                {
                    return Task.CompletedTask;
                }
                LinkedResource image = new LinkedResource(path, "image/jpeg");
                image.ContentId = "myImage";

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
                htmlView.LinkedResources.Add(image);

                message.AlternateViews.Add(htmlView);

                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient(setting.SmtpClient)
                {
                    // Your Email Provider Configurations
                    Port = setting.Port,
                    Credentials = new NetworkCredential(fromMail, fromPassword),

                    // always enable ssl
                    EnableSsl = setting.EnableSSl,
                };

                // Send Email
                smtpClient.SendMailAsync(message);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
