using UnityEngine;

public class SpinBall : MonoBehaviour
{
    public Vector3 Axis;
    public float Speed = 2;
    void Update()
    {
            transform.Rotate(Axis,Time.deltaTime* Speed);
    }
}
