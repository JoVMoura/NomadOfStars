using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Rendering;

public class GameControl : MonoBehaviour
{
    private bool inBase;
    private int currentPlanet;
    private int cristais;
    private string[] planetName = { "Banguerata", "Apoluno", "Odisercio" };
    [SerializeField] private List<Transform> SpawnPlanet;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mouseObject;
    [SerializeField] TempoMenuConfig tempoMenuConfig;
    [SerializeField] private UI_control ui_control;
    [SerializeField] private TimerControl timerControl;
    [SerializeField] private WaveControl waveControl;
    [SerializeField] private TMP_Text txtPlanetName;
    [SerializeField] private TMP_Text txtCristais;
    [SerializeField] private GameObject[] timerMiniMenu;
    private InputAction BaseAction;

    void Start()
    {
        Time.timeScale = 0f;
        cristais = 0;
        txtPlanetName.text = "Planeta Atual: " + planetName[0];
        currentPlanet = 0;
        BaseAction = InputSystem.actions.FindAction("Base");
    }
    void Update()
    {
        if (inBase && BaseAction.WasPressedThisFrame())
        {
            ui_control.AbriBase();
        }
    }

    public void SetMouseObject(GameObject _object)
    {
        mouseObject = _object;
    }

    public GameObject GetMouseObject()
    {
        return mouseObject;
    }

    public void Hited()
    {
        if (mouseObject != null)
        {
            if (mouseObject.GetComponent<Breakable>().Break(20))
            {
                mouseObject = null;
            }
        }
    }

    public void SetInBase(bool state)
    {
        inBase = state;
        tempoMenuConfig.SetOpen(state);
    }

    public void SetSpawn(Transform transform)
    {
        if (SpawnPlanet == null)
        {
            SpawnPlanet = new List<Transform>();
        }
        SpawnPlanet.Add(transform);
        if (SpawnPlanet.Count == 1)
        {
            player.transform.position = transform.position;
        }
    }

    public Transform GetSpawn(int planet)
    {
        return SpawnPlanet[planet];
    }

    public void TrasportarPlayer(int planet)
    {
        currentPlanet = planet;
        txtPlanetName.text = "Planeta Atual: " + planetName[planet];

        timerControl.SetCurrentPlanet(planet);
        timerMiniMenu[planet].transform.localPosition = new Vector3(1410, timerMiniMenu[planet].transform.localPosition.y, 0);
        if (planet == 2)
        {
            timerMiniMenu[0].transform.localPosition = new Vector3(870, timerMiniMenu[planet].transform.localPosition.y, 0);
            timerMiniMenu[1].transform.localPosition = new Vector3(330, timerMiniMenu[planet].transform.localPosition.y, 0);
        }
        else if (planet == 1)
        {
            timerMiniMenu[2].transform.localPosition = new Vector3(870, timerMiniMenu[planet].transform.localPosition.y, 0);
            timerMiniMenu[0].transform.localPosition = new Vector3(330, timerMiniMenu[planet].transform.localPosition.y, 0);
        }
        else
        {
            timerMiniMenu[1].transform.localPosition = new Vector3(870, timerMiniMenu[planet].transform.localPosition.y, 0);
            timerMiniMenu[2].transform.localPosition = new Vector3(330, timerMiniMenu[planet].transform.localPosition.y, 0);
        }

        player.transform.position = SpawnPlanet[planet].position;
        ui_control.FecharPlanetas();
    }

    public void PegarCristal()
    {
        cristais++;
        txtCristais.text = "Cristais adquiridos: " + cristais +"/3";
        if (cristais == 3)
        {
            ui_control.AbrirVitoria();
        }
    }

    public void Derrota()
    {
        ui_control.AbrirDerrota();
    }
}
