using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Animator[] _doors;
    void Start()
    {
        StartCoroutine(DoorBehaviour());
    }

    IEnumerator DoorBehaviour()
    {
        //Open and close the random door by random interval of time
        while (true)
        {
            int waitTime = Random.Range(2, 4);
            int doorIndex = Random.Range(0, 4);

            yield return new WaitForSeconds(waitTime);
            _doors[doorIndex].SetTrigger("DoorOpen");
            yield return new WaitForSeconds(waitTime);
            _doors[doorIndex].SetTrigger("DoorClose");
        }
    }

}
