using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float _timeElapsed;
    private bool _canCount;
    
    public void StartTimer()
    {
        _timeElapsed = 0.0f;
        _canCount = true;
    }

    public float StopTimer()
    {
        _canCount = false;
        float roundedTime = float.Parse(_timeElapsed.ToString("F1"));
        GetComponent<PlayerInfo>().TimeTaken = roundedTime;
        return roundedTime;
    }

    private void Update()
    {
        if (!_canCount) return;
        _timeElapsed += Time.deltaTime;
    }
}
