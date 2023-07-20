using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerListManager : MonoBehaviour
{
    TMP_Text[] texts;

    private void Awake()
    {
        texts = GetComponentsInChildren<TMP_Text>();
    }

    public void UpdateList(List<PlayerNetworkData> playerNetworkDatas)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if (i < playerNetworkDatas.Count)
            {
                texts[i].text = playerNetworkDatas[i].NickName;
            }
            else
            {
                texts[i].text = "";
            }
        }
    }
}