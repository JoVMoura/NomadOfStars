using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveControl : MonoBehaviour
{
    [SerializeField] private TimerControl timerControl;
    [SerializeField] private UI_control uiControl;
    [SerializeField] private Transform[] spwans;
    [SerializeField] private Wave[] waves;
    private int numWave;

    void Start()
    {
        numWave = -1;
    }

    public void WaveStart()
    {
        if(numWave < 3)
        {
            numWave++;
            StartCoroutine(Wave());
        }
    }

    private IEnumerator Wave()
    {
        int actualDensity = 0;
        List<int> posSpwans = new List<int>();
        List<int> amount = new List<int>();
        int i = 0, j = 0, k = 0;
        bool saida = false;
    
        for(i = 0; i < waves[numWave].waveData.Count; i++)
        {
            posSpwans.Add(i);
            amount.Add(waves[numWave].waveData[i].Amount);
        }

        do
        {
            actualDensity = waves[numWave].waveDensity;

            for(i = actualDensity; i > 0; i--)
            {
                j = Random.Range(0,posSpwans.Count);
                if(amount[posSpwans[j]] > 0)
                {
                    k = Random.Range(0,spwans.Length);
                    Instantiate(waves[numWave].waveData[posSpwans[j]].Enemy, spwans[k].position, Quaternion.identity);
                    amount[posSpwans[j]]--;
                }
                else
                {
                    posSpwans.RemoveAt(j);
                    i++;
                }

                if(posSpwans.Count == 0)
                {
                    break;
                }
            }

            if(posSpwans.Count == 0)
            {
                saida = true;
            }
            else
            {
                yield return new WaitForSeconds(waves[numWave].waveTime);
            }

        } while(!saida);

        if(numWave < 2)
        {
            timerControl.ResetTimer();
        }
        else
        {
            uiControl.AbrirVitoria(); // Chama a tela de vitória do UI_control
        }
    }
}
