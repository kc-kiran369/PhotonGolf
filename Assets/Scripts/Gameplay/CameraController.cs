using UnityEngine;
using Fusion;

public class CameraController : NetworkBehaviour
{
    [SerializeField]private GameObject _cam;
    private void Start()
    {
        if (HasInputAuthority)
        {
            _cam.SetActive(true);
        }
    }
}
