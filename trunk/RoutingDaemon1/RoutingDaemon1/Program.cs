using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutingDaemon1.Utilities;
using RoutingDaemon1.Backend;
using RoutingDaemon1.Entities;
using IRC.Utilities;
using IRC.Utilities.Entities;

namespace RoutingDaemon1
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Verbose = true;
            Dictionary<ArgumentKey, object> arguments;

            Logger.Instance.Info("Starting Routing Daemon...");

            try
            {
                // Load arguments
                arguments = ArgumentsParser.GetArguments(args);
            }
            catch
            {
                Logger.Instance.Error("Invalid arguments");
                return;
            }

            // Set the local node ID.
            DaemonBackEnd.Instance.LocalNode.NodeID = (int)arguments[ArgumentKey.NodeID];

            List<NodeConfiguration> config;

            try
            {
                // Load configuration file.
                config = ConfigFileParser.LoadConfigurationFromPath((string)arguments[ArgumentKey.ConfigFilePath]);
            }
            catch
            {
                return;
            }

            Logger.Instance.Debug("Loaded configuration file");

            // Configure all the neighbors of the local node
            DaemonBackEnd.Instance.ConfigureLocalNode(config);

            RoutingDaemon1 daemon = new RoutingDaemon1(
                DaemonBackEnd.Instance.LocalNode.Configuration.RoutingPort,
                DaemonBackEnd.Instance.LocalNode.Configuration.LocalPort
                );

            // Start the daemon
            daemon.Start();
            Logger.Instance.Info("Daemon Started Successfully");

            // Infinite Loop till the application exits.
            while (true)
            {
                System.Threading.Thread.Sleep(100000);
            }
        }
    }
}
