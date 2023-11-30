using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineController : MonoBehaviour 
{

    public PlayableDirector playableDirector;
    public bool isPaused = false;
    

    public List<double> pauseTimes = new List<double> {4.50, 13.000, 19.299, 25.00};

    public void Start()
    {
        Time.timeScale = 1.0f;

        playableDirector = GetComponent<PlayableDirector>();

        Debug.Log("Starting timeline controller");
    }


    public void Update()
    {
       //Debug.Log("Pause Times: " + string.Join(", ", pauseTimes));
        double currentTime = playableDirector.time;
        
        if (!isPaused)
        {

            if (PauseTimesContains(currentTime))
            {
                Debug.Log("Pausing at time: " + currentTime);
                PauseTimeline();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("get key");
            ResumeTimeline();
        }
    }

    public void PauseTimeline()
    {
        Debug.Log("pause timeline");
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        isPaused = true;
    }

    public void ResumeTimeline()
    {
        Debug.Log("Resume timeline");
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        isPaused = false;
    }
    private bool PauseTimesContains(double time)
    {
        // Check if the pauseTimes list contains the specified time with a small epsilon for precision
        double epsilon = 0.001; 
        foreach (double pauseTime in pauseTimes)
        {
            if (Mathf.Abs((float)(pauseTime - time)) < epsilon)
            {
                return true;
            }
        }
        return false;
    }
}