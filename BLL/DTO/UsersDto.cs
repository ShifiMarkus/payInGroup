using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class UsersDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserId { get; set; }
        public string? UserPassword { get; set; }
        public string? UserMail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserType { get; set; }
    }
}
