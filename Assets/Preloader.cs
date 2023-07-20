using UnityEngine;
using UnityEngine.Analytics;

#pragma warning disable SPELL
public class Preloader : MonoBehaviour
{
    public string ClientArgument = "-start-as-client";
    public string ServerArgument = "-start-as-server";

    [SerializeField] MainMenu menu;
    void Start()
    {
        Analytics.initializeOnStartup = true;
        var result = Analytics.CustomEvent("gameStarted");
        Debug.Log("Analytics Result : " + result);

        var args = System.Environment.GetCommandLineArgs();

        foreach (var arg in args)
        {
            if (arg == ServerArgument)
            {
                menu.OnHostGameClicked();
            }
            else if (arg == ClientArgument)
            {
                menu.OnJoinGameClicked();
            }
        }
    }
}
