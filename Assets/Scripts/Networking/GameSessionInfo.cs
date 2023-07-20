using Fusion;
using UnityEngine;

public class GameSessionInfo : NetworkBehaviour
{
    [field: SerializeField] public static uint TotalPlayers { get; set; } = 0;

    public void OnPlayerAdd()
    {
        TotalPlayers += 1;
    }
}
