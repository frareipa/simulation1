using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.Entities
{
    public class User
    {
        public string Username { get; set; }

        public string Nickname { get; set; }

        public override bool Equals(object obj)
        {
            User otherUser = (User)obj;
            if (this.Nickname != null && otherUser.Nickname != null)
            {
                return this.Nickname.ToLower().Equals(otherUser.Nickname.ToLower());
            }
            return false;
        }
    }
}
