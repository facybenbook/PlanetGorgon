using DarkRift.Client.Unity;
using DarkRift;
using UnityEngine;
using DarkRift.Client;

public class PlayerSpawner : MonoBehaviour
{
    const byte SPAWN_TAG = 0;

    [SerializeField]
    [Tooltip("The DarkRift client to communicate on.")]
    UnityClient client;

    [SerializeField]
    [Tooltip("The controllable player prefab.")]
    GameObject controllablePrefab;

    [SerializeField]
    [Tooltip("The network controllable player prefab.")]
    GameObject networkPrefab;

    [SerializeField]
    [Tooltip("The network player manager.")]
    NetworkPlayerManager networkPlayerManager;

    void Awake()
    {
        if (client == null)
        {
            Debug.LogError("Client unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (controllablePrefab == null)
        {
            Debug.LogError("Controllable Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (networkPrefab == null)
        {
            Debug.LogError("Network Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }

        client.MessageReceived += SpawnPlayer;
        client.MessageReceived += DespawnPlayer;
        client.Disconnected += DisconnectPlayer;
    }

    void SpawnPlayer(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage())
        using (DarkRiftReader reader = message.GetReader())
        {
            if (message.Tag == Tags.SpawnPlayerTag)
            {
                //Extract the positions
                Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3 rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                //Extract their ID
                ushort id = reader.ReadUInt16();

                //If it's a player for us then spawn us our prefab and set it up
                if (id == client.ID)
                {
                    GameObject o = Instantiate(
                        controllablePrefab,
                        position,
                        Quaternion.Euler(rotation)
                    ) as GameObject;

                    Player player = o.GetComponent<Player>();
                    player.PlayerID = id;
                    player.Client = client;
                }

                //If it's for another player then spawn a network player and to the manager. 
                else
                {
                    GameObject o = Instantiate(
                        networkPrefab,
                        position,
                        Quaternion.Euler(rotation)
                    ) as GameObject;

                    PlayerNetwork player = o.GetComponent<PlayerNetwork>();
                    networkPlayerManager.Add(id, player);
                }
            }
        }
    }

    void DespawnPlayer(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage())
        if (message.Tag == Tags.DespawnPlayerTag)
        {
            using (DarkRiftReader reader = message.GetReader())
            {
                DespawnPlayer(reader);
            }
        }
    }

    void DespawnPlayer(DarkRiftReader reader)
    {
        networkPlayerManager.RemoveCharacter(reader.ReadUInt16());
    }

    void DisconnectPlayer(object sender, DisconnectedEventArgs e)
    {
        networkPlayerManager.RemoveAllCharacters();
    }
}