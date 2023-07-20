using Cinemachine;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class DragAndShoot : MonoBehaviour
{
    private const float MaxDistance = 1.5f;
    private const float DistanceThreshold = 0.11f;
    private float _distanceFromBall = 0.0f;
    private Vector3 _shootDirection = Vector3.zero;
    private Camera _camera;
    private bool _isReadyToShoot;
    private bool _isMouseLookEnabled = true;
    private static readonly int Distance = Shader.PropertyToID("_Distance");

    [SerializeField] private string PlayerTag = "Player";

    [SerializeField] private Transform DirectionMarker;
    [SerializeField] private CinemachineFreeLook FreeLookCamera;
    [SerializeField] private Material DirectionMarkerMaterial;
    [SerializeField] private LayerMask PlayerMask;

    private NetworkObject _networkObject;

    public bool CanSendData = false;
    public UnityEvent OnBallShoot;

    private void Start()
    {
        _camera = Camera.main;
        DirectionMarker.transform.gameObject.SetActive(false);
        _networkObject = GetComponentInParent<NetworkObject>();
    }

    /// <summary>
    /// Determines the direction to shoot the ball.
    /// </summary>
    /// <param name="mousePosition">The position of the mouse on screen.</param>
    public void Process(Vector2 mousePosition)
    {
        if (!_networkObject.HasInputAuthority) return;

        //Shoots the ray from screen to world view
        //If ray hits the player or ball, further calculation is done.
        if (!Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out RaycastHit hit, 100.0f, PlayerMask)) return;
        if (hit.transform.CompareTag(PlayerTag) && hit.transform.gameObject.GetComponentInParent<NetworkPlayer>().Object.HasInputAuthority)
            _isReadyToShoot = true;

        if (!_isReadyToShoot) return;


        //Direction is determined by subtraction player's position and mouse position.
        //Direction is determined for XZ axis only
        var direction = transform.position - hit.point;
        var horizontalDirection = new Vector3(direction.x, 0, direction.z);
        _shootDirection = horizontalDirection.normalized;

        //Distance is calculated from player's position to the mouse position and clamped to maximum of MaxDistance
        _distanceFromBall = Mathf.Clamp(Vector3.Distance(transform.position, hit.point), 0, MaxDistance);

        //Distance is send to direction marker shader
        DirectionMarkerMaterial.SetFloat(Distance, _distanceFromBall);

        //Direction marker is positioned on ball's position and rotated towards the direction to shoot
        DirectionMarker.SetPositionAndRotation(transform.position, Quaternion.LookRotation(_shootDirection));

        //Direction Marker is scaled on forward axis to represent the distance amount visually.
        var localScale = DirectionMarker.localScale;
        localScale = new Vector3(localScale.x, localScale.y, _distanceFromBall);
        DirectionMarker.localScale = localScale;
    }

    public void OnMouseLeftButtonDown()
    {
        if (_isReadyToShoot)
        {
            DirectionMarker.transform.gameObject.SetActive(true);
            DisableMouseLook();
        }
    }

    public void OnMouseLeftButtonUp()
    {
        if (!_isReadyToShoot) return;
        DirectionMarker.transform.gameObject.SetActive(false);
        EnableMouseLook();
        _isReadyToShoot = false;
        if (_distanceFromBall > DistanceThreshold)
        {
            OnBallShoot.Invoke();
            CanSendData = true;
        }
    }

    //#if PLATFORM_WINDOWS
    public void OnMouseRightButtonDown()
    {
        if (_isMouseLookEnabled)
            DisableMouseLook();
        else
            EnableMouseLook();
        _isMouseLookEnabled = !_isMouseLookEnabled;
    }
    //#endif

    /// <summary>
    /// Enables the mouse control of cinemachine freelook camera
    /// </summary>
    private void EnableMouseLook()
    {
        FreeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        FreeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
    }

    /// <summary>
    /// Disables the mouse control of cinemachine freelook camera
    /// </summary>
    private void DisableMouseLook()
    {
        FreeLookCamera.m_YAxis.m_InputAxisName = "";
        FreeLookCamera.m_XAxis.m_InputAxisName = "";

        FreeLookCamera.m_YAxis.m_InputAxisValue = 0;
        FreeLookCamera.m_XAxis.m_InputAxisValue = 0;
    }

    public NetworkInputData InputData() => new()
    { ShootDirection = _shootDirection, Distance = _distanceFromBall };
    //{ ShootDirection = _shootDirection, Distance = _distanceFromBall > 0.101 ? _distanceFromBall : 0 };
}