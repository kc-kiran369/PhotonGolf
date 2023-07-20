using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft, IPlayerJoined
{
    public static NetworkPlayer Local { get; set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            GetComponentInChildren<PlayerTag>().EnablePlayerIndicatorArrow();
        }
    }

    public void PlayerLeft(PlayerRef player)
    {

    }

    public void PlayerJoined(PlayerRef player)
    {

    }
}