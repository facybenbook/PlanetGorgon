using System;
using System.Collections.Generic;
using System.Linq;
using DarkRift;
using DarkRift.Server;

public class GorgonPlayerManager : Plugin
{
    public override bool ThreadSafe => false;
    public override Version Version => new Version(1, 0, 0);
    const float MAP_WIDTH = 20;

    Dictionary<IClient, GorgonPlayer> players = new Dictionary<IClient, GorgonPlayer>();

    public GorgonPlayerManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
    {        
        ClientManager.ClientConnected += ClientConnected;
    }

    void ClientConnected(object sender, ClientConnectedEventArgs e)
    {
        Random r = new Random();
        GorgonPlayer newPlayer = new GorgonPlayer(
            e.Client.ID,
            (float)r.NextDouble() * MAP_WIDTH - MAP_WIDTH / 2,
            (float)r.NextDouble() * MAP_WIDTH - MAP_WIDTH / 2,
            (float)r.NextDouble() * MAP_WIDTH - MAP_WIDTH / 2,
            1f,
            (byte)r.Next(0, 200),
            (byte)r.Next(0, 200),
            (byte)r.Next(0, 200)
        );

        using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
        {
            newPlayerWriter.Write(newPlayer.ID);
            newPlayerWriter.Write(newPlayer.X);
            newPlayerWriter.Write(newPlayer.Y);
            newPlayerWriter.Write(newPlayer.Radius);
            newPlayerWriter.Write(newPlayer.ColorR);
            newPlayerWriter.Write(newPlayer.ColorG);
            newPlayerWriter.Write(newPlayer.ColorB);

            using (Message newPlayerMessage = Message.Create(Tags.SpawnPlayerTag, newPlayerWriter))
            {
                foreach (IClient client in ClientManager.GetAllClients().Where(x => x != e.Client))
                    client.SendMessage(newPlayerMessage, SendMode.Unreliable);
            }
        }

        players.Add(e.Client, newPlayer);

        using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
        {
            foreach (GorgonPlayer player in players.Values)
            {
                playerWriter.Write(player.ID);
                playerWriter.Write(player.X);
                playerWriter.Write(player.Y);
                playerWriter.Write(player.Radius);
                playerWriter.Write(player.ColorR);
                playerWriter.Write(player.ColorG);
                playerWriter.Write(player.ColorB);
            }

            using (Message playerMessage = Message.Create(Tags.SpawnPlayerTag, playerWriter))
                e.Client.SendMessage(playerMessage, SendMode.Reliable);
        }
    }
}


class GorgonPlayer
{
    public ushort ID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Radius { get; set; }
    public byte ColorR { get; set; }
    public byte ColorG { get; set; }
    public byte ColorB { get; set; }

    public GorgonPlayer(ushort ID, float x, float y, float z, float radius, byte colorR, byte colorG, byte colorB)
    {
        this.ID = ID;
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.Radius = radius;
        this.ColorR = colorR;
        this.ColorG = colorG;
        this.ColorB = colorB;
    }
}
