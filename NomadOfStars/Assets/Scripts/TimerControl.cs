using TMPro;
using UnityEngine;

public class TimerControl : MonoBehaviour
{
    [SerializeField] private WaveControl waveControl;
    [SerializeField] private float timeTotal;
    private int currentPlanet;
    private float[] TimeLeft = new float[3];
    //private float[] TimeOrder = new float[3]; 
    private bool[] TimerOn = {false, false, false};
    [SerializeField] private TMP_Text[] txtTimer = new TMP_Text[3];
   
    void Start()
    {
        currentPlanet = 0;

        TimeLeft[0] = timeTotal;
        TimeLeft[1] = timeTotal+60;
        TimeLeft[2] = timeTotal+120;
        TimerOn[0] = true;
        TimerOn[1] = true;
        TimerOn[2] = true;
    }

    void Update()
    {
        if(TimerOn[0])
        {
            if(TimeLeft[0] > 0)
            {
                TimeLeft[0] -= Time.deltaTime;
                updateTimer(TimeLeft[0], 0);
            }
            else
            {
                TimeLeft[0] = 0;
                TimerOn[0] = false;
                waveControl.WaveStart();
            }
        }
        if (TimerOn[1])
        {
            if (TimeLeft[1] > 0)
            {
                TimeLeft[1] -= Time.deltaTime;
                updateTimer(TimeLeft[1], 1);
            }
            else
            {
                TimeLeft[1] = 0;
                TimerOn[1] = false;
                waveControl.WaveStart();
            }
        }
        if (TimerOn[2])
        {
            if (TimeLeft[2] > 0)
            {
                TimeLeft[2] -= Time.deltaTime;
                updateTimer(TimeLeft[2], 2);
            }
            else
            {
                TimeLeft[2] = 0;
                TimerOn[2] = false;
                waveControl.WaveStart();
            }
        }
    }

    public void ResetTimer(int timer)
    {
        if (timer == 2)
        {
            TimeLeft[timer] = timeTotal + TimeLeft[1];
        }
        else
        {
            TimeLeft[timer] = timeTotal + TimeLeft[timer+1];
        }

        TimerOn[timer] = true;
        updateTimer(TimeLeft[timer], timer);
    }

    public void ClearTimer()
    {
        TimeLeft[currentPlanet] = 0;
        TimerOn[currentPlanet] = false;
        updateTimer(TimeLeft[currentPlanet], currentPlanet);
    }

    void updateTimer(float currentTime, int actualTimer)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        if (actualTimer == currentPlanet)
        {
            txtTimer[0].text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (currentPlanet == actualTimer - 1 || currentPlanet == actualTimer + 2)
        {
            txtTimer[1].text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            txtTimer[2].text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
