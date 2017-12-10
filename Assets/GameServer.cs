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
	public bool useTCP = false;
    public string hostIP = "127.0.0.1";
    public int hostPort = 12345;

	NetWorker server;

	private void Start()
	{
        NetWorker.PingForFirewall((ushort)hostPort);

		if (useMainThreadManagerForRPCs)
			Rpc.MainThreadRunner = MainThreadManager.Instance;
        Host(hostIP, hostPort);
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
		if (mgr == null && networkManager == null)
		{
			Debug.LogWarning("A network manager was not provided, generating a new one instead");
			networkManager = new GameObject("Network Manager");
			mgr = networkManager.AddComponent<NetworkManager>();
		}
		else if (mgr == null) //Instantiate Prefab
			mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

        //Setup Server for Spawning Objects/RPCs
        mgr.Initialize(server);

        //Handle Connects/Disconnects
        networker.playerConnected += PlayerConnected2;
        networker.playerDisconnected += PlayerDisconnected2;

        //Read Binary Opcode Messages
        networker.binaryMessageReceived += ReadBinary;

        //IS NEEDED?
		//if (networker is IServer)
		//{
		    //NetworkObject.Flush(networker); //Called because we are already in the correct scene!
		//}


        //Spawn Server Player
        Debug.Log("Spawning Server Player");
        Invoke("SpawnServerPlayer", 1.0f);
	}

    private void ReadBinary(NetworkingPlayer player, Binary frameData, NetWorker networker1)
    {
        int start = MessageGroupIds.START_OF_GENERIC_IDS;
        BMSByte data = frameData.StreamData;

        //Handle Packets
    }


    public void SpawnPlayer(uint netID)
    {
        Debug.Log("Spawning Player");

        var player1 = NetworkManager.Instance.InstantiatePlayer();

        //Set Player Owner
        player1.networkObject.playerNetworkId = netID;
    }

    public void SpawnServerPlayer()
    {
        SpawnPlayer(server.Me.NetworkId);   //server.Me.NetworkId);
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
        server.Disconnect(true);
        Debug.Log("Server Shutdown. Runtime: " + Time.time + " seconds.");
	}
}
