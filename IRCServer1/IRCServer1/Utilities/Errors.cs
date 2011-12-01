using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRCServer1.Utilities
{
    public enum ErrorCode
    {
        ERR_NEEDMOREPARAMS,
        ERR_ALREADYREGISTERED,
        ERR_UNKNOWNCOMMAND,
        ERR_NONICKNAMEGIVEN,
        ERR_NICKNAMEINUSE,
        ERR_NORECIPIENT,
        ERR_NOTEXTTOSEND,
        ERR_NOSUCHNICK
    };

    public class Errors
    {
        private const string ERR_NEEDMOREPARAMS = "461 {0} :Need more params";

        private const string ERR_ALREADYREGISTERED = "462 :You are already registered";

        private const string ERR_UNKNOWNCOMMAND = "404 :Unknown command";

        private const string ERR_NONICKNAMEGIVEN = "431 :No nickname given";

        private const string ERR_NICKNAMEINUSE = "433 {0} :Nickname is already in use";

        private const string ERR_NORECIPIENT = "411 :No recipient given ({0})";

        private const string ERR_NOTEXTTOSEND = "412 :No text to send";

        private const string ERR_NOSUCHNICK = "401 {0} :No such nick/channel";

        public static string GetErrorResponse(ErrorCode errorCode, params string[] arguments)
        {
            switch (errorCode)
            {
                case ErrorCode.ERR_NEEDMOREPARAMS:
                    if (arguments.Length != 1)
                        throw new ArgumentOutOfRangeException();
                    return string.Format(ERR_NEEDMOREPARAMS, arguments[0]);
                case ErrorCode.ERR_ALREADYREGISTERED:
                    return ERR_ALREADYREGISTERED;
                case ErrorCode.ERR_UNKNOWNCOMMAND:
                    return ERR_UNKNOWNCOMMAND;
                case ErrorCode.ERR_NONICKNAMEGIVEN:
                    return ERR_NONICKNAMEGIVEN;
                case ErrorCode.ERR_NICKNAMEINUSE:
                    if (arguments.Length != 1)
                        throw new ArgumentOutOfRangeException();
                    return string.Format(ERR_NICKNAMEINUSE, arguments[0]);
                case ErrorCode.ERR_NORECIPIENT:
                    if (arguments.Length != 1)
                        throw new ArgumentOutOfRangeException();
                    return string.Format(ERR_NORECIPIENT, arguments[0]);
                case ErrorCode.ERR_NOTEXTTOSEND:
                    return ERR_NOTEXTTOSEND;
                case ErrorCode.ERR_NOSUCHNICK:
                    if (arguments.Length != 1)
                        throw new ArgumentOutOfRangeException();
                    return string.Format(ERR_NOSUCHNICK, arguments[0]);
                default:
                    return String.Empty;
            }
        }
    }
}
