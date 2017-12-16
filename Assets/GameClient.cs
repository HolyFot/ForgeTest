using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.Forge.Networking.Frame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClient : MonoBehaviour
{
	public GameObject networkManager = null;
	private NetworkManager mgr = null;
    public bool useMainThreadManagerForRPCs = true;
    public string hostIP;
    public int hostPort;
    public bool connectMainSceneServer;

    private int SpawnPlayerOpcode = 5;
    NetWorker client;

	private void Start()
	{
		NetWorker.PingForFirewall((ushort)hostPort);

		if (useMainThreadManagerForRPCs)
			Rpc.MainThreadRunner = MainThreadManager.Instance;

        if (GameSettings.isServer == false) //IF IT IS A SERVER DONT START CLIENT
        {
            StartClient(hostIP, hostPort);
        }
	}

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	public void StartClient(string ip, int port)
	{
        Debug.Log("Connecting to GameServer: " + ip + ":" + port);

		client = new UDPClient();
		((UDPClient)client).Connect(ip, (ushort)port);

		Connected(client);
	}

	public void Connected(NetWorker networker)
	{
		if (!networker.IsBound)
		{
			Debug.LogError("NetWorker failed to bind");
			return;
		}

		if (mgr == null && networkManager == null)
		{
			Debug.LogWarning("A network manager was not provided, generating a new one instead");
			networkManager = new GameObject("Network Manager");
			mgr = networkManager.AddComponent<NetworkManager>();
		}
		else if (mgr == null) //Spawn Prefab
			mgr = Instantiate(networkManager).GetComponent<NetworkManager>();


        //Handle Connects/Disconnects
        networker.disconnected += ServerDisconnect;

        //Read Binary Opcode Messages
        networker.binaryMessageReceived += ReadBinary;

        //Request Spawning Player
        if (connectMainSceneServer)
            RequestSpawnPlayer(GameSettings.chosenChar);
	}

    //Request to Spawn Player
    public void RequestSpawnPlayer(string username)
    {
        Debug.Log("Sending Spawn Player Request");

        //SEND PACKET: SPAWN PLAYER
        ulong timestep = client.Time.Timestep;
        int opcode = MessageGroupIds.START_OF_GENERIC_IDS + SpawnPlayerOpcode;
        BMSByte data = new BMSByte();
        data = ObjectMapper.Instance.MapBytes(data, username);
        Binary bin = new Binary(timestep, false, data, Receivers.Server, opcode, false);
        ((UDPClient)client).Send(bin, true);
    }


    private void ServerDisconnect(NetWorker networker1)
    {
        try
        {
            Debug.Log("The server disconnected.");
            MainThreadManager.Run(() =>
            {
                SceneManager.LoadScene("StartGame");
            });
        }
        catch (Exception e)
        {}
    }

    private void ReadBinary(NetworkingPlayer player, Binary frameData, NetWorker networker1)
    {
        int start = MessageGroupIds.START_OF_GENERIC_IDS;
        BMSByte data = frameData.StreamData;
    }

	private void OnApplicationQuit()
	{
        if (GameSettings.isServer == false)
        {
            client.Disconnect(true);
            Debug.Log("Game closed.");
        }
	}
}
