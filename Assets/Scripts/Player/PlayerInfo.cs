using Fusion;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    [field: SerializeField][Networked] public uint NumberOfShoots { get; private set; }
    [field: SerializeField][Networked] public float TimeTaken { get; set; }
    [field: SerializeField][Networked] public Vector3 LastPosition { get; private set; }
    [field: SerializeField][Networked] public bool HasCompletedGame { get; set; } = false;

    public void OnBallShoot()
    {
        NumberOfShoots++;
        RegisterLastPosition();
        Rpc_SendData();
    }

    private void RegisterLastPosition()
    {
        LastPosition = transform.position;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    void Rpc_SendData(RpcInfo info = default)
    {
        if (Runner.TryGetPlayerObject(info.Source, out NetworkObject netObj))
        {
            netObj.gameObject.GetComponentInChildren<PlayerInfo>().NumberOfShoots++;
        }
    }
}