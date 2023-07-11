using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Services.DATA.EmailData;
namespace Services.DATA.EmailData
{
    public class Email : IEmail
    {
       
            private readonly IEmailService _emailService;
            public Email(IEmailService emailService)
            {
                _emailService = emailService;
            }
            public void SendEmail(string toEmail, string toName)
            {
                var subject = "ברוכים הבאים לאפליקציה המגניבה 🖐";
                var message = $@"<h3>הי, {toName}</h3> 
                <p>תודה שהצטרפת אלינו 🤗</p>
                <p>להתראות!</p>";
                _emailService.Send(toEmail, subject, message, true);
            }

            public void sendAddMemberToGroup(string toMail, string toName, string groupGoal)
            {
            var subject = " יש לך קבוצה חדשה";
            var message = $@"<h3>הי, {toName}</h3> 
                <p>הצטרפת בהצלחה לקבוצה: {groupGoal} </p>
                <p>על מנת לראות את הפעליות בקבוצה עליך להכנס לחשבונך האישי באתר!</p>
                <p>תודה ולהתראות!</p>";
            _emailService.Send(toMail, subject, message, true);
        }



    }
}
