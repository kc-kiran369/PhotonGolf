using Fusion;
using UnityEngine;

public class Nickname : NetworkBehaviour
{
    [field: SerializeField][Networked] public NetworkString<_16> NickName { get; set; }

    public override void Spawned()
    {
        NickName = PlayerPrefs.GetString("Nickname");
        GetComponentInChildren<PlayerTag>().SetName(NickName.Value);
    }
}