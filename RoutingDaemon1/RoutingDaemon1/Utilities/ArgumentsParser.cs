using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.Utilities
{
    public enum ArgumentKey
    {
        NodeID,
        ConfigFilePath,
        AdvertisementCycle,
        NeighborTimeout,
        RetransmissionTimeout,
        LSATimeout
    };

    public static class ArgumentsParser
    {
        private const string NodeIDKey = "-i";
        private const string ConfigFileKey = "-c";
        private const string AdvertisementCycleKey = "-a";
        private const string NeighborTimeoutKey = "-n";
        private const string RetransmissionTimeoutKey = "-r";
        private const string LSATimeoutKey = "-t";

        /// <summary>
        /// Gets the arguments passed to the router daemon in a dictionary.
        /// </summary>
        /// <param name="args">The args array passed to main().</param>
        /// <returns>Dictionary of the configuration key and value.</returns>
        public static Dictionary<ArgumentKey, object> GetArguments(string[] args)
        {
            if (args.Length % 2 != 0)
                throw new ArgumentOutOfRangeException();

            Dictionary<ArgumentKey, object> arguments = new Dictionary<ArgumentKey, object>();

            for (int i = 0; i < args.Length; i += 2)
            {
                string arg = args[i].ToLower();

                switch (arg)
                {
                    case NodeIDKey:
                        arguments.Add(ArgumentKey.NodeID, int.Parse(args[i + 1]));
                        break;
                    case ConfigFileKey:
                        arguments.Add(ArgumentKey.ConfigFilePath, args[i + 1]);
                        break;
                    case AdvertisementCycleKey:
                        arguments.Add(ArgumentKey.AdvertisementCycle, int.Parse(args[i + 1]));
                        break;
                    case NeighborTimeoutKey:
                        arguments.Add(ArgumentKey.NeighborTimeout, int.Parse(args[i + 1]));
                        break;
                    case RetransmissionTimeoutKey:
                        arguments.Add(ArgumentKey.RetransmissionTimeout, int.Parse(args[i + 1]));
                        break;
                    case LSATimeoutKey:
                        arguments.Add(ArgumentKey.LSATimeout, int.Parse(args[i + 1]));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (!arguments.ContainsKey(ArgumentKey.NodeID))
                throw new ArgumentNullException();

            if (!arguments.ContainsKey(ArgumentKey.ConfigFilePath))
                throw new ArgumentNullException();

            if (!arguments.ContainsKey(ArgumentKey.AdvertisementCycle))
                arguments.Add(ArgumentKey.AdvertisementCycle, 30);

            if (!arguments.ContainsKey(ArgumentKey.NeighborTimeout))
                arguments.Add(ArgumentKey.NeighborTimeout, 120);

            if (!arguments.ContainsKey(ArgumentKey.RetransmissionTimeout))
                arguments.Add(ArgumentKey.RetransmissionTimeout, 3);

            if (!arguments.ContainsKey(ArgumentKey.LSATimeout))
                arguments.Add(ArgumentKey.LSATimeout, 120);

            return arguments;
        }
    }
}
