using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private Animator _sceneTransitionAnimator;
    [SerializeField] private GameObject VisualLayers;
    public static SceneTransition Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _sceneTransitionAnimator = GetComponent<Animator>();
            HideLayers();
        }
    }

    public void FadeIn(float time = 0)
    {
        _sceneTransitionAnimator.SetTrigger("FadeIn");

        if (time > 0)
            Invoke(nameof(FadeOut), time);
    }

    public void FadeOut()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
    }

    public void ShowLayers()
    {
        VisualLayers.SetActive(true);
    }
    public void HideLayers()
    {
        VisualLayers.SetActive(false);
    }
}
