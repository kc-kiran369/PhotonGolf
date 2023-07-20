using TMPro;
using UnityEngine;
using Fusion;
using System.Collections;

public class PlayerTag : NetworkBehaviour
{
    [SerializeField] private Nickname PlayersNickName;
    [SerializeField] private TMP_Text PlayersTagText;
    [SerializeField] private Transform BallTransform;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject PlayerIndicatorArrow;
    private void Awake()
    {
        offset = new Vector3(0, 0.5f, 0);
        CameraTransform = Camera.main.transform;
    }

    public void SetName()
    {
        SetName(gameObject.GetComponentInParent<Nickname>().NickName.Value);
    }

    public void SetName(string name)
    {
        //PlayersTagText.text = name;
        //Debug.Log("Setting Tag To : " + name);
    }

    private void Update()
    {
        transform.SetPositionAndRotation(BallTransform.position + offset,
Quaternion.LookRotation((this.transform.position - CameraTransform.position).normalized));
    }

    public void EnablePlayerIndicatorArrow()
    {
        if (Object.HasInputAuthority)
        {
            PlayerIndicatorArrow.SetActive(true);
        }
    }
}