using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICRServer.Entities
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's real name.
        /// </summary>
        /// <value>
        /// The real name.
        /// </value>
        public string Realname { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the user's machine.
        /// </summary>
        /// <value>
        /// The hostname of the user's machine.
        /// </value>
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the nickname of the user.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        public string Nickname { get; set; }
    }
}
