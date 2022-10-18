using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStopwatch : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public float minutes;
    public float seconds;
    public float miliSeconds;

    [SerializeField]TextMeshProUGUI stopwatchText;
    // Update is called once per frame
   

    public void ResetStopWatch()
    {
        time = 0;
        StartCoroutine(StopWatch());
    }

    IEnumerator StopWatch()
    {
        while(true)
        {
            time += Time.deltaTime;
            minutes = (int)(time / 60 % 60);
            seconds = (int)(time % 60);
            miliSeconds = (int)((time - (int)time)*100);
            stopwatchText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes,seconds,miliSeconds);
            yield return  null;
        }

    }

    public void StartStopWatch()
    {
        StartCoroutine(StopWatch());
    }

    public void PauseStopWatch()
    {
        StopCoroutine(StopWatch());
    }


}
