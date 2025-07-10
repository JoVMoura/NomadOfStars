using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlanetWaves
{
    public Wave[] waves = new Wave[3];
}

public class WaveControl : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private TimerControl timerControl;
    [SerializeField] private UI_control uiControl;

    [Header("Configuração das Waves")]
    [SerializeField] private PlanetWaves[] allPlanetWaves = new PlanetWaves[3];

    private bool[] inWave = { false, false, false };
    private int[] waveCounters = { 0, 0, 0 };

    public void WaveStart(int planetIndex)
    {
        if (inWave[planetIndex])
        {
            Debug.LogWarning($"Tentativa de iniciar wave para o planeta {planetIndex + 1} enquanto outra já está em andamento no mesmo planeta.");
            return;
        }

        if (waveCounters[planetIndex] < 3)
        {
            Debug.Log(planetIndex);
            StartCoroutine(WaveCoroutine(planetIndex));
        }
        else
        {
            Debug.Log($"Todas as waves para o planeta {planetIndex + 1} já foram completadas.");
        }
    }

    private IEnumerator WaveCoroutine(int planetIndex)
    {
        inWave[planetIndex] = true;

        int currentWaveIndex = waveCounters[planetIndex];
        Wave currentWave = allPlanetWaves[planetIndex].waves[currentWaveIndex];

        Debug.Log($"Iniciando Wave {currentWaveIndex + 1} do Planeta {planetIndex + 1}");

        List<GameObject> enemiesToSpawn = new List<GameObject>();
        foreach (var waveData in currentWave.waveData)
        {
            for (int i = 0; i < waveData.Amount; i++)
            {
                enemiesToSpawn.Add(waveData.Enemy);
            }
        }

        while (enemiesToSpawn.Count > 0)
        {
            int spawnCount = Mathf.Min(enemiesToSpawn.Count, currentWave.waveDensity);

            for (int i = 0; i < spawnCount; i++)
            {
                int enemyIndex = Random.Range(0, enemiesToSpawn.Count);
                GameObject enemyPrefab = enemiesToSpawn[enemyIndex];
                Vector3 spawnPosition = GetRandomSpawnPosition(planetIndex);
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemiesToSpawn.RemoveAt(enemyIndex);
            }

            if (enemiesToSpawn.Count > 0)
            {
                yield return new WaitForSeconds(currentWave.waveTime);
            }
        }

        Debug.Log($"Wave {currentWaveIndex + 1} do Planeta {planetIndex + 1} finalizada.");
        waveCounters[planetIndex]++;

        if (waveCounters[planetIndex] >= 3)
        {
            Debug.Log($"Planeta {planetIndex + 1} derrotado!");
            uiControl.AbrirVitoria();
        }
        else
        {
            // AVISA O TIMERCONTROL PARA RESETAR O TIMER PARA A PRÓXIMA WAVE
            timerControl.ResetTimer(planetIndex);
        }

        inWave[planetIndex] = false;
    }

    private Vector3 GetRandomSpawnPosition(int planetIndex)
    {
        float x = 0, y = 0;
        float offset = planetIndex * 5376f;
        int side = Random.Range(1, 5);

        switch (side)
        {
            // CORREÇÃO: Adicionado o "+ offset" à coordenada Y em todos os casos.
            case 1: // Top
                y = Random.Range(1492f, 2048f);
                x = Random.Range(498f, 2048f) + offset;
                break;
            case 2: // Right
                y = Random.Range(498f, 2048f);
                x = Random.Range(1492f, 2048f) + offset;
                break;
            case 3: // Bottom
                y = Random.Range(498f, 754f);
                x = Random.Range(498f, 2048f) + offset;
                break;
            case 4: // Left
                y = Random.Range(498f, 2048f);
                x = Random.Range(498f, 754f) + offset;
                break;
        }

        return new Vector3(x, y, 0);
    }
}