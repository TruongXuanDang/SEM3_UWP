using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApplication.Entities;

namespace MusicApplication.Services
{
    interface IMemberService
    {
        string Login(LoginMember member);
        string Register(User user);
        string GetInformation(string token);
    }
}
