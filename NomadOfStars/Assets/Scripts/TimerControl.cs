using TMPro;
using UnityEngine;

public class TimerControl : MonoBehaviour
{
    [SerializeField] private WaveControl waveControl;
    [SerializeField] private float timeTotal;
    private int currentPlanet;
    private float[] TimeLeft = new float[3];
    private bool[] TimerOn = { false, false, false };
    [SerializeField] private TMP_Text[] txtTimer = new TMP_Text[3];

    void Start()
    {
        currentPlanet = 0;

        TimeLeft[0] = timeTotal;
        TimeLeft[1] = timeTotal + 60;
        TimeLeft[2] = timeTotal + 120;
        TimerOn[0] = true;
        TimerOn[1] = true;
        TimerOn[2] = true;
    }

    void Update()
    {
        if (TimerOn[0])
        {
            if (TimeLeft[0] > 0)
            {
                TimeLeft[0] -= Time.deltaTime;
                updateTimer(TimeLeft[0], 0);
            }
            else
            {
                TimeLeft[0] = 0;
                TimerOn[0] = false;
                waveControl.WaveStart(0);
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
                waveControl.WaveStart(1);
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
                waveControl.WaveStart(2);
            }
        }
    }

    public void StartWaveManually()
    {
        if (TimerOn[currentPlanet])
        {
            TimeLeft[currentPlanet] = 0;
            updateTimer(TimeLeft[currentPlanet], currentPlanet);
        }
    }

    // CORREÇÃO: Lógica do método ResetTimer simplificada e corrigida.
    public void ResetTimer(int planetIndex)
    {
        if (planetIndex >= 0 && planetIndex < TimeLeft.Length)
        {
            // Simplesmente reseta o timer do planeta especificado para o valor total.
            TimeLeft[planetIndex] = timeTotal;
            TimerOn[planetIndex] = true;
        }
    }

    public void SetCurrentPlanet(int planetIndex)
    {
        if (planetIndex >= 0 && planetIndex < 3)
        {
            currentPlanet = planetIndex;
        }
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