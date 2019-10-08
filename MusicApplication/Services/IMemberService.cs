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
        String Login(String username, String password);
        User Register(User user);
        User GetInformation(String token);
    }
}
