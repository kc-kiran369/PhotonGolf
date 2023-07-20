using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Range(60, 300)]
    public float _totalTime = 60;
    private bool _isCalculating = true;

    public UnityEvent TimerCompleted;

    /// <summary>
    /// Starts the timer which begins from the given time and reaches zero.;
    /// Invokes the "TimerCompleted" Event when the timer reaches Zero
    /// </summary>
    /// <param name="time">The time for the timer</param>
    public void StartTimer(float time)
    {
        _totalTime = time;
        _isCalculating = true;
    }

    void Update()
    {
        if (!_isCalculating) return;

        if (_totalTime >= 0)
        {
            _totalTime -= Time.deltaTime;
        }
        else
        {
            _isCalculating = false;
            TimerCompleted.Invoke();
            MessageBox.Instance.Show("Time's Up", "You didn't complete the game in time.");
        }
    }
}