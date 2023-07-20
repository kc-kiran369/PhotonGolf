using UnityEngine;

public class DontDestroyOnSceneLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
