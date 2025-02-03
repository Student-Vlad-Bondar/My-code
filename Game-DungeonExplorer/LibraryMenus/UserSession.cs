using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBatles
{
    public static class UserSession
    {
        public static int UserId { get; set; } = -1;
        public static bool IsLoggedIn => UserId != -1;
    }
}
