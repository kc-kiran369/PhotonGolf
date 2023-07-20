using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;

    private float progress = 0;
    private RectTransform Transform;

    private void Awake()
    {
        Transform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        AnimateBehaviour();
    }
    private void AnimateBehaviour()
    {
        Transform.localScale = curve.Evaluate(progress) * Vector3.one;
        progress += Time.deltaTime;
        progress %= 1.0f;
    }
}