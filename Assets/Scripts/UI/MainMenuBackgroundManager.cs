using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackgroundManager : MonoBehaviour
{
    [SerializeField] MainMenuBackground Backgrounds;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = Backgrounds.Images[Random.Range(0, Backgrounds.Images.Length)];
    }
}