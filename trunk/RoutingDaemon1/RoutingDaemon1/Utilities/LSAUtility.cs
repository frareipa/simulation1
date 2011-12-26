using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutingDaemon1.Entities;
using RoutingDaemon1.Backend;

namespace RoutingDaemon1.Utilities
{
    /// <summary>
    /// The LSA Utility class.
    /// </summary>
    public static class LSAUtility
    {
        /// <summary>
        /// Creates the LSA from byte array.
        /// </summary>
        /// <param name="data">The byte array received.</param>
        /// <returns>The LSA.</returns>
        public static LSA CreateLSAFromByteArray(byte[] data)
        {
            LSA lsa = new LSA();

            int version = (int)data[0];
            int ttl = (int)data[1];
            LSAType type = (LSAType)data[3];
            int senderId = BitConverter.ToInt32(data, 4);
            int sequenceNumber = BitConverter.ToInt32(data, 8);

            if (type == LSAType.Acknowledgement)
            {
                return new LSA()
                {
                    Version = version,
                    TTL = ttl,
                    Type = type,
                    SequenceNumber = sequenceNumber,
                    SenderNodeID = senderId
                };
            }


            int numOfLinks = BitConverter.ToInt32(data, 12);
            int numOfUsers = BitConverter.ToInt32(data, 16);

            Node[] nodes = new Node[numOfLinks];

            for (int i = 0; i < numOfLinks; i++)
            {
                int nodeId = BitConverter.ToInt32(data, 20 + i * 4);
                nodes[i] = DaemonBackEnd.Instance.GetNodeByID(nodeId);
            }

            User[] users = new User[numOfUsers];

            for (int i = 0; i < numOfUsers; i++)
            {
                string nickname = System.Text.Encoding.ASCII.GetString(data, 20 + numOfLinks * 4 + i * 16, 16);
                users[i] = new User() { Nickname = nickname.Trim().Replace("\0", "") };
            }

            return new LSA()
            {
                Version = version,
                Type = type,
                TTL = ttl,
                SenderNodeID = senderId,
                Links = nodes.ToList(),
                Users = users.ToList(),
                SequenceNumber = sequenceNumber
            };
        }

        /// <summary>
        /// Creates the acknoledgment LSA from the received LSA.
        /// </summary>
        /// <param name="receivedLSA">The received LSA.</param>
        /// <returns></returns>
        public static byte[] CreateAckLSAFromLSA(LSA receivedLSA)
        {
            LSA ackLsa = new LSA()
                   {
                       Type = LSAType.Acknowledgement,
                       SenderNodeID = Backend.DaemonBackEnd.Instance.LocalNode.NodeID,
                       SequenceNumber = receivedLSA.SequenceNumber
                   };
            byte[] buffer = GetByteArrayFromLSA(ackLsa);
            return buffer;
        }

        /// <summary>
        /// Gets the byte array from the LSA.
        /// </summary>
        /// <param name="lsa">The LSA.</param>
        /// <returns>The byte array to send.</returns>
        public static byte[] GetByteArrayFromLSA(LSA lsa)
        {
            byte[] data = new byte[20 + lsa.Users.Count * 16 + lsa.Links.Count * 4];

            data[0] = Convert.ToByte(lsa.Version);
            data[1] = Convert.ToByte(lsa.TTL);
            data[2] = Convert.ToByte(0);
            data[3] = Convert.ToByte(lsa.Type);

            byte[] senderId = BitConverter.GetBytes(lsa.SenderNodeID);
            byte[] sequenceNumber = BitConverter.GetBytes(lsa.SequenceNumber);
            byte[] numOfLinks = BitConverter.GetBytes(lsa.Links.Count);
            byte[] numOfUsers = BitConverter.GetBytes(lsa.Users.Count);

            byte[] users = new byte[lsa.Users.Count * 16];

            for (int i = 0; i < lsa.Users.Count * 16; i++)
            {
                users[i] = 0;
            }

            int offset = 0;
            foreach (User user in lsa.Users)
            {
                System.Text.Encoding.ASCII.GetBytes(user.Nickname).CopyTo(users, offset);
                offset += 16;
            }

            byte[] links = new byte[lsa.Links.Count * 4];

            offset = 0;
            foreach (Node link in lsa.Links)
            {
                BitConverter.GetBytes(link.NodeID).CopyTo(links, offset);
                offset += 4;
            }

            senderId.CopyTo(data, 4);
            sequenceNumber.CopyTo(data, 8);
            numOfLinks.CopyTo(data, 12);
            numOfUsers.CopyTo(data, 16);
            links.CopyTo(data, 20);
            users.CopyTo(data, 20 + links.Length);

            return data;
        }

    }
}
