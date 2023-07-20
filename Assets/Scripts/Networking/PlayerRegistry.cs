using System.Collections.Generic;
using Fusion;
using UnityEngine;


public class PlayerRegistry : MonoBehaviour
{
    [property: SerializeField]
    [Networked]
    private List<PlayerNetworkData> JoinPlayerNetworkData { get; set; }
    private void Awake()
    {
        JoinPlayerNetworkData = new List<PlayerNetworkData>();
    }
    private void OnPlayerUpdated()
    {
        GameObject.Find("PlayerListManager")?.GetComponent<PlayerListManager>()?.UpdateList(JoinPlayerNetworkData);
    }
    public void AddPlayerData(PlayerNetworkData playerNetworkData)
    {
        JoinPlayerNetworkData.Add(playerNetworkData);
        OnPlayerUpdated();
    }
    public List<PlayerNetworkData> GetPlayersDataList() => JoinPlayerNetworkData;
    public void RemovePlayerByPlayerRef(PlayerRef playerRef)
    {
        for (int i = 0; i < JoinPlayerNetworkData.Count; i++)
        {
            if (JoinPlayerNetworkData[i].PlayerRefId == playerRef)
            {
                JoinPlayerNetworkData.Remove(JoinPlayerNetworkData[i]);
                OnPlayerUpdated();
            }
        }
    }
    public void Clear()
    {
        JoinPlayerNetworkData.Clear();
    }
}