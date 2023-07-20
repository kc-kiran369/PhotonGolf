using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _hostGameMenu;
    [SerializeField] private GameObject _joinGameMenu;
    [SerializeField] private GameObject _PlayerTypeMenu;
    [SerializeField] private GameObject _matchMakingMenu;
    [SerializeField] private GameObject _hostStartButton;

    [SerializeField] private TMP_InputField _hostSessionInputField;
    [SerializeField] private TMP_InputField _clientSessionInputField;
    [SerializeField] private TMP_InputField _nickNameInputField;

    [SerializeField] private Nickname Nickname;

    [SerializeField] private Spawner _spawner;

    public void OnHostAGameBtnClicked()
    {
        _PlayerTypeMenu.SetActive(false);
        _hostGameMenu.SetActive(true);
    }

    public void OnJoinAGameBtnClicked()
    {
        _PlayerTypeMenu.SetActive(false);
        _joinGameMenu.SetActive(true);
    }

    public void OnHostGameClicked()
    {
        SetName();

        SceneTransition.Instance.FadeIn();
        _spawner.StartAsHost(_hostSessionInputField.text.ToLower());
        _hostGameMenu.SetActive(false);
    }

    public void OnJoinGameClicked()
    {
        SetName();

        SceneTransition.Instance.FadeIn();
        _spawner.StartAsClient(_clientSessionInputField.text.ToLower());
    }

    public void SetName()
    {
        PlayerPrefs.SetString("Nickname", _nickNameInputField.text);
    }
}