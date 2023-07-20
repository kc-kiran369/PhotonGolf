using System.Collections;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private Animator SpikeAnimator;
    private static readonly int SpikeUp = Animator.StringToHash("SpikeUp");
    private static readonly int SpikeDown = Animator.StringToHash("SpikeDown");

    void Start()
    {
        StartCoroutine(SpikeBehaviour());
    }

    IEnumerator SpikeBehaviour()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            SpikeAnimator.SetTrigger(SpikeUp);
            yield return new WaitForSeconds(Random.Range(2, 5));
            SpikeAnimator.SetTrigger(SpikeDown);
        }
    }
    
}
