using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRCServer1.Utilities
{
    public enum IRCCommandType
    {
        USER,
        NICK,
        QUIT,
        PRIVMSG
    };

    public static class CommandParser
    {
        public const string USERCommand = "USER";
        public const string NICKCommand = "NICK";
        public const string QUITCommand = "QUIT";
        public const string PRIVMSGCommand = "PRIVMSG";

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

        public static IRCCommandType? GetIRCCommandFromString(string command)
        {

            string commandUpperCase = command.ToUpper();

            switch (commandUpperCase)
            {
                case USERCommand:
                    return IRCCommandType.USER;
                case NICKCommand:
                    return IRCCommandType.NICK;
                case QUITCommand:
                    return IRCCommandType.QUIT;
                case PRIVMSGCommand:
                    return IRCCommandType.PRIVMSG;
                default:
                    return null;
            }
        }
    }
}
