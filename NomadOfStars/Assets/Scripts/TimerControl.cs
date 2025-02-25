using TMPro;
using UnityEngine;

public class TimerControl : MonoBehaviour
{
    [SerializeField] private WaveControl waveControl;
    [SerializeField] private float timeTotal;
    private float TimeLeft;
    private bool TimerOn = false;

    [SerializeField] private TMP_Text txtTimer;
   
    void Start()
    {
        TimeLeft = timeTotal;
        TimerOn = true;
    }

    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
                waveControl.WaveStart();
            }
        }
    }

    public void ResetTimer()
    {
        TimeLeft = timeTotal;
        TimerOn = true;
        updateTimer(TimeLeft);
    }

    public void ClearTimer()
    {
        TimeLeft = 0;
        TimerOn = false;
        updateTimer(TimeLeft);
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        txtTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
