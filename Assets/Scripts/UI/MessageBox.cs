using UnityEngine;
using TMPro;
using Fusion;

public enum Button
{
    OK, CANCEL, CONTINUE
}

public enum ButtonType
{
    NONE, OK, OKCANCEL, CONTINUE
}

public class MessageBox : MonoBehaviour
{
    [SerializeField] private GameObject MessageBoxImg;
    [SerializeField] private TMP_Text TitleText, DescriptionText;

    public static MessageBox Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    /// <summary>
    /// Opens the message box with the title and description provided
    /// </summary>
    /// <param name="title">The title of the messagebox</param>
    /// <param name="description">The description of the messagebox</param>
    /// <param name="time">The amount of time that the messagebox will be visible</param>
    /// <param name="type">The type of button set</param>
    public void Show(string title, string description, float time = 1.0f, ButtonType type = ButtonType.NONE)
    {
        //TitleText.text = title;
        //DescriptionText.text = description;
        RPC_UpdateUI(title, description);

        MessageBoxImg.SetActive(true);

        Invoke(nameof(HideMessageBox), time);
    }

    private void HideMessageBox()
    {
        MessageBoxImg.SetActive(false);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    void RPC_UpdateUI(string title, string description)
    {
        TitleText.text = title;
        DescriptionText.text = description;
    }
}