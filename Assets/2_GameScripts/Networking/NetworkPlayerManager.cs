using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using System.Collections.Generic;
using UnityEngine;

internal class NetworkPlayerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The DarkRift client to communicate on.")]
    UnityClient client;

    Dictionary<ushort, PlayerNetwork> networkPlayers = new Dictionary<ushort, PlayerNetwork>();

    void Awake()
    {
        if (client == null)
        {
            Debug.LogError("No client assigned to NetworkPlayerManager component!");
            return;
        }

        client.MessageReceived += MessageReceived;
    }

    void MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        //using (Message message = e.GetMessage() as Message)
        //{
        //    if (message.Tag == Tags.MovePlayerTag)
        //    {
        //        using (DarkRiftReader reader = message.GetReader())
        //        {
        //            // Wrap In While so we dont go outsite the bounts of the message
        //            while (reader.Position < reader.Length)
        //            {
        //                //Read message
        //                Vector3 newPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        //                Vector3 newRotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        //                ushort id = reader.ReadUInt16();

        //                //Update characters to move to new positions
        //                networkPlayers[id].NewPosition = newPosition;
        //                networkPlayers[id].NewRotation = newRotation;
        //            }
        //        }
        //    }
        //}
       
    }
    public void Add(ushort id, PlayerNetwork player)
    {
        //networkPlayers.Add(id, player);
    }
}