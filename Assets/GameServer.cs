using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.Forge.Networking.Frame;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameServer : MonoBehaviour
{
	public GameObject networkManager = null;
	private NetworkManager mgr = null;
	public bool useMainThreadManagerForRPCs = true;
    public string hostIP;
    public int hostPort;
    public bool isMainSceneServer;
    private int SpawnPlayerOpcode = 5;
	NetWorker server;

	private void Start()
	{
        if (GameSettings.isServer == true)
        {
            NetWorker.PingForFirewall((ushort)hostPort);

            if (useMainThreadManagerForRPCs)
                Rpc.MainThreadRunner = MainThreadManager.Instance;

            if (GameSettings.isServer == true) //IF IT IS A CLIENT, DONT START SERVER
                Host(hostIP, hostPort);
        }
	}

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	public void Host(string ip, int port)
	{
        Debug.Log("Starting Forge Server: " + ip + ":" + port);

		server = new UDPServer(64);
		((UDPServer)server).Connect(ip, (ushort)port);

		server.playerTimeout += (player, sender) =>
		{
			Debug.Log("Player " + player.NetworkId + " timed out");
		};

		Connected(server);
	}

	public void Connected(NetWorker networker)
	{
		if (!networker.IsBound)
		{
			Debug.LogError("NetWorker failed to bind");
			return;
		}

        //Setup NetworkManager
        if (isMainSceneServer) //Only 1 Network Manager can be running
        {
            if (mgr == null && networkManager == null)
            {
                Debug.LogWarning("A network manager was not provided, generating a new one instead");
                networkManager = new GameObject("Network Manager");
                mgr = networkManager.AddComponent<NetworkManager>();
            }
            else if (mgr == null) //Instantiate Prefab
                mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

            //Setup Server for Spawning Objects/RPCs
            //mgr.Initialize(networker);
            NetworkManager.Instance.Initialize(networker);
            Debug.Log("NetworkManager Initialized");
        }

        //Handle Connects/Disconnects
        networker.playerConnected += PlayerConnected2;
        networker.playerDisconnected += PlayerDisconnected2;

        //Read Binary Opcode Messages
        networker.binaryMessageReceived += ReadBinary;

        //IS NEEDED?
		//if (networker is IServer)
		//{
		   // NetworkObject.Flush(networker); //Called because we are already in the correct scene!
		//}


        //Spawn Server Player
        if (isMainSceneServer)
        {
            Invoke("SpawnServerPlayer", 1.0f);
        }
	}

    private void ReadBinary(NetworkingPlayer player, Binary frameData, NetWorker networker1)
    {
        int start = MessageGroupIds.START_OF_GENERIC_IDS;
        BMSByte data = frameData.StreamData;

        //Handle Packets
        if (frameData.GroupId == start + SpawnPlayerOpcode)
            HandleSpawnPlayerRequest(player, data);
    }

    public void HandleSpawnPlayerRequest(NetworkingPlayer sender, BMSByte stream)
    {
        string charName1 = ObjectMapper.Instance.Map<string>(stream);
        sender.Name = charName1;

        Debug.Log("Spawning Client Player");
        SpawnPlayer(sender.NetworkId, true, sender);
    }

    public void SpawnPlayer(uint netID, bool isClient, NetworkingPlayer clientPlayer)
    {
        //Spawn Player & Set Player Owner
        MainThreadManager.Run(() =>
        {
            var player1 = NetworkManager.Instance.InstantiatePlayer();
            player1.networkObject.playerNetworkId = netID;

            if (isClient)
            {
                Debug.Log("Assigning Ownership for Client");
                player1.networkObject.AssignOwnership(clientPlayer);
            }
        });
    }

    public void SpawnServerPlayer()
    {
        Debug.Log("Spawning Server Player");
        SpawnPlayer(server.Me.NetworkId, false, null);
    }


    private void PlayerDisconnected2(NetworkingPlayer player, NetWorker networker1)
    {
        try
        {
			Debug.Log("Player disconnected: " + player.Name);

            //DESTROY PLAYER
            foreach (var no in networker1.NetworkObjectList)
            {
                if (no.Owner == player)
                {
                    //Found him
                    networker1.NetworkObjectList.Remove(no);
                    no.Destroy();
                    return;
                }
            }
        }
        catch (Exception e)
        {}
    }

    private void PlayerConnected2(NetworkingPlayer player, NetWorker networker1)
    {
        try
        {
            Debug.Log("User Connected: " + player.Name + ". NetID: " + player.NetworkId);
        }
        catch (Exception e)
        {}
    }

	private void OnApplicationQuit()
	{
		if (server != null)
        	server.Disconnect(true); //Disconnecting Fixes Unity Freezing on Recompile

        if (!isMainSceneServer)
            Debug.Log("Server Shutdown. Runtime: " + Time.time + " seconds.");
	}
}
