using Fusion;
using UnityEngine;

public class Respawn : NetworkBehaviour
{
    [SerializeField] private NetworkUpdate NetworkUpdate;
    public const float RespawnAfter = 1.2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RespawnBall()
    {
        Invoke(nameof(RespawnNow), RespawnAfter);
    }

    private void RespawnNow()
    {
        rb.angularVelocity = rb.velocity = Vector3.zero;
        var lastposition = gameObject.GetComponent<PlayerInfo>().LastPosition;

        NetworkUpdate.RespawnPlayer(lastposition);
    }
}
