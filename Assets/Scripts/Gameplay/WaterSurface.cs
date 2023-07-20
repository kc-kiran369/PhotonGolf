using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    [SerializeField] GameObject splash;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        other.gameObject.GetComponent<Respawn>().RespawnBall();
    }
}