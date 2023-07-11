using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DATA.EmailData;

namespace payInGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
            private readonly EmailManager _emailManager;
        private readonly IEmail _IEmail;
            public EmailController(EmailManager emailManager, IEmail email)
            {
                _emailManager = emailManager;
            _IEmail = email;
          
        }


        [HttpPost]
            public IActionResult Post()
            {
            string str = "well come";

            //IEmail.sendEmail()
                return Ok();
            }
        
    }
}
