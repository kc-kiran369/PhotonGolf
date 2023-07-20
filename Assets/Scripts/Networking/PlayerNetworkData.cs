using System;
using Fusion;
using UnityEngine;

[Serializable]
public class PlayerNetworkData
{
    [field: SerializeField] public string NickName { get; set; } = "";
    [field: SerializeField] public int PlayerRefId { get; set; } = 0;
}