using Fusion;
using Fusion.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

#pragma warning disable UNT0006, UNT0008

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    private DragAndShoot _shoot;

    [SerializeField] private NetworkPrefabRef PlayerPrefab;

    private PlayerRegistry _registry;

    private void Awake()
    {
        _registry = GetComponent<PlayerRegistry>();
    }

    private async void StartGame(GameMode mode, string sessionName)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        var nickName = PlayerPrefs.GetString("Nickname");

        if (mode is GameMode.Host or GameMode.Server)
        {
            PlayerPrefs.SetString("Nickname", "[Host] " + nickName);
            //Analytics.CustomEvent("GameHosted");
        }
        else if (nickName.Length == 0)
        {
            PlayerPrefs.SetString("Nickname", "Guest");
        }

        _ = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = sessionName,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
        });

        await LoadScene(1);
    }

    public void StartAsServer(string sessionName)
    {
        StartGame(GameMode.Server, sessionName);
    }

    public void StartAsHost(string sessionName)
    {
        StartGame(GameMode.Host, sessionName);
    }

    public void StartAsClient(string sessionName)
    {
        StartGame(GameMode.Client, sessionName);
    }

    public async Task LoadScene(int index = 1)
    {
        await Task.Run(() => { _runner.SetActiveScene(index); });
    }

    #region INTERFACE_OVERRIDES

    public async void OnPlayerJoined(Fusion.NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {

            var spawnTransform = GameObject.Find("SpawnPoint").transform;

            //Spawn the player at specified position
            Vector3 spawnPosition = spawnTransform.position + new Vector3(0, 0, Random.Range(-2.5f, 2.5f));
            NetworkObject networkPlayerObject =
                _runner.Spawn(PlayerPrefab, position: spawnPosition, Quaternion.identity, player);

            //Get data of spawned player and store in a list
            var playerNetworkData = new PlayerNetworkData()
            { PlayerRefId = player.PlayerId, NickName = PlayerPrefs.GetString("Nickname") };

            _registry.AddPlayerData(playerNetworkData);

            runner.SetPlayerObject(player, networkPlayerObject);

            //Set the name of the spawned player
            networkPlayerObject.gameObject.GetComponentInChildren<PlayerTag>().SetName();

            //Sync the list of data of spawned players through RPC
            string jsonString = JsonConvert.SerializeObject(_registry.GetPlayersDataList());
            networkPlayerObject.transform.gameObject.GetComponent<SendRPC>().Rpc_SendNames(jsonString);
        }
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!_shoot)
        {
            _shoot = NetworkPlayer.Local?.gameObject.GetComponentInChildren<DragAndShoot>();
            return;
        }

        if (_shoot.CanSendData)
        {
            var newData = _shoot.InputData();
            if (newData.Distance > 0)
                input.Set(newData);
            _shoot.CanSendData = false;
        }
    }
    public void OnPlayerLeft(Fusion.NetworkRunner runner, PlayerRef player)
    {
        if (runner.TryGetPlayerObject(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _registry.RemovePlayerByPlayerRef(player);
        }
    }

    #region UNUSED_INTERFACE_OVERRIDES

    public void OnInputMissing(Fusion.NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(Fusion.NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(Fusion.NetworkRunner runner)
    {
        runner.SetActiveScene(0);
        runner.Shutdown();
    }

    public void OnConnectRequest(Fusion.NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        //Debug.Log("Connection Requesting : " + token.ToString());
    }

    public void OnConnectFailed(Fusion.NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        //Debug.Log("Connection Failed.\nReason : " + reason.ToString());
    }

    public void OnUserSimulationMessage(Fusion.NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(Fusion.NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(Fusion.NetworkRunner runner, Dictionary<string, object> Data)
    {
    }

    public void OnHostMigration(Fusion.NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(Fusion.NetworkRunner runner, PlayerRef player, ArraySegment<byte> Data)
    {
    }

    public void OnSceneLoadDone(Fusion.NetworkRunner runner)
    {
        SceneTransition.Instance.FadeOut();
    }

    public void OnSceneLoadStart(Fusion.NetworkRunner runner)
    {
    }

    #endregion

    #endregion
}