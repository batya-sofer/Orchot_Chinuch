using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace service
{
    public partial class Form1 : Form
    {
        DateTime lastRun;
        OrchotDbContext db = new OrchotDbContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeTimer();
            btnState.Text = "Start";
            btnState.Click += new EventHandler(start_Click);
            lblSate.Text = "not work";
        }

        private void InitializeTimer()
        {
            // Call this procedure when the application starts.  
            // Set to 1 day
            timer.Interval = 1000;
            lastRun = DateTime.MinValue;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
            timer.Start();
            btnState.Text = "Stop";
            btnState.Click += new EventHandler(stop_Click);
            lblSate.Text = "work";
        }

        private void stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Enabled = false;
            lblSate.Text = "not work";
            btnState.Text = "Start";
            btnState.Click += new EventHandler(start_Click);
        }

        private void start_Click(object sender, EventArgs e)
        {
            timer.Start();
            timer.Enabled = true;
            lblSate.Text = "work";
            btnState.Text = "Stop";
            btnState.Click += new EventHandler(stop_Click);
            lastRun = DateTime.MinValue;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (lastRun == null || lastRun.Date < DateTime.Today)
            {
                lastRun = DateTime.Now;

                List<Document> list;
                list = db.Documents.ToList();
                foreach (var document in list)
                {
                    Document d = new Document();
                    if (document.AlertActive == true && document.EffectiveDate != null && document.ExpirationDate != null && document.NumDaysExp != 0 && document.MailAddress != "")
                    {
                        d.CreateBy = document.CreateBy;
                        d.AlertActive = true;
                        d.EffectiveDate = document.EffectiveDate;
                        d.ExpirationDate = document.ExpirationDate;
                        d.NumDaysExp = document.NumDaysExp;

                        if (((DateTime)d.ExpirationDate - DateTime.Now).Days <= document.NumDaysExp)
                        {
                            d.Organization = db.TblAssistValues.First(x => x.TableCode == 10 && x.Code == document.Organization).Value1;
                            d.BusinessUnit = db.TblAssistValues.First(x => x.TableCode == 11 && x.Code == document.BusinessUnit).Value1;
                            d.Department = db.TblAssistValues.First(x => x.TableCode == 12 && x.Code == document.Department).Value1;
                            List<string> allMails = document.MailAddress.Split(',').ToList();
                            foreach (string mail in allMails)
                            {
                                string subject = "תפוגת מסמך: " + document.DocumentName;
                                string body = "<p style=\"font-family: 'Tahoma'; font-size: 12px;\">"
                                                 + "ארגון:  " + d.Organization + "<br />"
                                                 + "מחלקה:  " + d.Department + "<br />"
                                                 + "מחלקה עסקית:  " + d.BusinessUnit + "<br />"
                                                 + "תאריך יצירת המסמך: " + document.CreateDate?.ToString("d") + "<br />"
                                                 + "תאריך סיום תוקף:  " + document.ExpirationDate?.ToString("d") + "<br /><br />"
                                                 + "<strong>נא לעדכן את תוקף המסמך</strong><br />"
                                                 + "קישור למסמך:  <a href=\"" + document.DocLink + "\">" + document.DocLink + "</a>"
                                                 + "</p>";
                                SendEmail(mail, d.CreateBy, body, subject);
                            }
                        }

                    }
                }

            }
        }

        public string SendEmail(string receiver, string name, string message, string subject)
        {
            try
            {
                string mail;
                string pass;
                //הבאת ערך המייל והסיסמא מה app config
                mail = ConfigurationManager.AppSettings.Get("mail");
                pass = ConfigurationManager.AppSettings.Get("password");

                // ניתן להגדיר שם שיוצג על כתובות המייל של השולח והמקבל
                var senderEmail = new MailAddress(mail, "התראות ארכיון מסמכים");
                var receiverEmail = new MailAddress(receiver, name);
                var password = pass;
                string sub = subject;
                string body = message;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true

                    // מגדירים במייל אימות דו שלבי, בתוכו ישנה אופציה להגדרת סיסמאות לאפליקציות
                    // ומעתיקים את הקוד בן 16 האותיות במקום הכוכביות windows בוחרים סיסמה לאימייל במחשב 
                    //UseDefaultCredentials = false,
                    //Credentials = new NetworkCredential(mail, "*******")
                };

                using (MailMessage mess = new MailMessage(senderEmail, receiverEmail)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                    smtp.Send(mess);
                return "ok";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return "error";
        }
    }
}