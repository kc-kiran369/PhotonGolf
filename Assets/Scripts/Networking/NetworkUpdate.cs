using Fusion;
using UnityEngine;
using Unity.RemoteConfig;

public class NetworkUpdate : NetworkBehaviour
{

    public struct userAttributes { }
    public struct appAttributes { }

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int ForceAmount = 200;
    [SerializeField] private Transform _ballTransform;

    [SerializeField] private DragAndShoot _dragAndShoot;

    private bool _canRespawn = false;
    private Transform spawnPoint;
    [field: SerializeField][Networked] private Vector3 RespawnPoint { get; set; } = Vector3.zero;

    private void Awake()
    {
        ConfigManager.FetchCompleted += SetBallForce;
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());

        spawnPoint = GameObject.Find("SpawnPoint").transform;
    }

    private void SetBallForce(ConfigResponse response)
    {
        ForceAmount = ConfigManager.appConfig.GetInt("BallForce");
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (_rigidbody)
            {
                _rigidbody.AddForce(ForceAmount * data.Distance * data.ShootDirection, ForceMode.Impulse);
            }
        }

        if (_canRespawn)
        {
            _rigidbody.MovePosition(spawnPoint.position + new Vector3(0, 0, Random.Range(-2.5f, 2.5f)));
            _canRespawn = false;
        }
    }

    public void RespawnPlayer(Vector3 respawnPoint)
    {
        RespawnPoint = respawnPoint;
        _canRespawn = true;
    }
}