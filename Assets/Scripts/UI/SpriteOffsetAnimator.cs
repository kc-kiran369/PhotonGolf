using UnityEngine;
using UnityEngine.UI;

public class SpriteOffsetAnimator : MonoBehaviour
{
    Image _image;

    public Vector2 Axis = new(1.0f, 0.0f);
    public float Speed = 0.2f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.material.mainTextureOffset += Time.deltaTime * Speed * Axis;
    }
}