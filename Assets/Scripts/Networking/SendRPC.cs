using Fusion;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class SendRPC : NetworkBehaviour
{

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, TickAligned = true, InvokeLocal = false)]
    public void Rpc_SendNames(string names, RpcInfo info = default)
    {
        var var = JsonConvert.DeserializeObject<List<PlayerNetworkData>>(names);

        var registry = Runner.gameObject.GetComponent<PlayerRegistry>();
        registry.Clear();
        foreach (var data in var)
        {
            registry.AddPlayerData(data);
        }
    }
}