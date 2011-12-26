using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.Utilities
{
    public enum DaemonCommandType
    {
        ADDUSER,
        REMOVEUSER,
        NEXTHOP,
        USERTABLE
    };

    public static class CommandParser
    {
        public const string ADDUSERCommand = "ADDUSER";
        public const string REMOVEUSERCommand = "REMOVEUSER";
        public const string NEXTHOPCommand = "NEXTHOP";
        public const string USERTABLECommand = "USERTABLE";

        public static string[] GetParameters(string message)
        {
            string[] colonSplit = message.Split(new char[] { ':' });

            if (colonSplit.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            string[] spaceSplit = colonSplit[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (spaceSplit.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return spaceSplit.Concat(colonSplit.Except(new List<string>() { colonSplit[0] })).ToArray();
        }

        public static DaemonCommandType? GetIRCCommandFromString(string command)
        {
            string commandLowerCase = command.ToUpper();

            switch (commandLowerCase)
            {
                case ADDUSERCommand:
                    return DaemonCommandType.ADDUSER;
                case REMOVEUSERCommand:
                    return DaemonCommandType.REMOVEUSER;
                case NEXTHOPCommand:
                    return DaemonCommandType.NEXTHOP;
                case USERTABLECommand:
                    return DaemonCommandType.USERTABLE;
                default:
                    return null;
            }
        }
    }
}
