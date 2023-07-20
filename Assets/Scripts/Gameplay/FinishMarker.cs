using Fusion;
using System.Text;
using UnityEngine;

public class FinishMarker : NetworkBehaviour
{
    public NetworkRunner NetRunner;

    private void Start()
    {
        if (!NetRunner) NetRunner = GameObject.Find("NetworkRunner").GetComponent<NetworkRunner>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Player")) return;

        Debug.Log("Collided with Finish Marker");

        var info = other.transform.gameObject.GetComponent<PlayerInfo>();
        info.HasCompletedGame = true;

        if (!info.HasInputAuthority) return;

        var reg = NetRunner.gameObject.GetComponent<PlayerRegistry>().GetPlayersDataList();
        bool hasAllPlayerCompleted = true;
        for (int i = 0; i < reg.Count; i++)
        {
            if (NetRunner.TryGetPlayerObject(reg[i].PlayerRefId, out NetworkObject netObj))
            {
                if (!netObj.gameObject.GetComponentInChildren<PlayerInfo>().HasCompletedGame)
                {
                    hasAllPlayerCompleted = false;
                }
            }
        }

        if (hasAllPlayerCompleted)
        {
            StringBuilder stringBuilder = new();
            foreach (var item in reg)
            {
                if (NetRunner.TryGetPlayerObject(item.PlayerRefId, out NetworkObject netObj))
                {
                    var playerInfo = netObj.gameObject.GetComponentInChildren<PlayerInfo>();
                    var nickName = netObj.gameObject.GetComponent<Nickname>().NickName;

                    Debug.Log(nickName + " " + playerInfo.NumberOfShoots);
                    stringBuilder.Append(nickName + " " + playerInfo.NumberOfShoots + "\n");
                }
            }
            MessageBox.Instance.Show("Level Completed", stringBuilder.ToString(), 3.0f);
        }
        else
        {
            MessageBox.Instance.Show("Level Completed", "Waiting for other players");
            Debug.Log("Waiting for other players");
        }
    }
}