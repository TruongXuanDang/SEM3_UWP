using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1807EHelloUWP.Entities;

namespace T1807EHelloUWP.Services
{
    interface IMemberService
    {
        void FormLogin(Member member, string api);
        void FormRegister(User user, string api);
        string FormGetInfo(string api);
    }
}
