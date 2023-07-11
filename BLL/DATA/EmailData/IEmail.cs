using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.EmailData
{
    public interface IEmail
    {
        public void SendEmail(string toMail, string massage);
        public void sendAddMemberToGroup(string toMail, string massage, string groupGoal);
        
    }
}
