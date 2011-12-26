using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.Utilities
{
    public enum ResponseCodes
    {
        OK,
        NextHop_OK,
        UserTable_OK,
        UserEntry,
        None
    };

    public class Responses
    {
        private const string RESPONSE_OK = "OK";

        private const string RESPONSE_NEXTHOP_OK = "OK {0} {1}";

        private const string RESPONSE_USERTABLE_OK = "OK {0}";

        private const string RESPONSE_NONE = "NONE";

        private const string RESPONSE_USERENTRY = "{0} {1} {2}";


        public static string GetResponse(ResponseCodes response, params string[] arguments)
        {
            switch (response)
            {
                case ResponseCodes.NextHop_OK:
                    if (arguments.Length != 2)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    return string.Format(RESPONSE_NEXTHOP_OK, arguments[0], arguments[1]);
                case ResponseCodes.UserTable_OK:
                    if (arguments.Length != 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    return string.Format(RESPONSE_USERTABLE_OK, arguments[0]);
                case ResponseCodes.OK:
                    return RESPONSE_OK;
                case ResponseCodes.None:
                    return RESPONSE_NONE;
                case ResponseCodes.UserEntry:
                    if (arguments.Length != 3)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    return string.Format(RESPONSE_NEXTHOP_OK, arguments[0], arguments[1], arguments[2]);
                default:
                    return String.Empty;
            }
        }
    }

}
