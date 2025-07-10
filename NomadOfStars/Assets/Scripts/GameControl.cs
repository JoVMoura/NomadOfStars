using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControl : MonoBehaviour
{
    private bool inBase;
    private List<Transform> SpawnPlanet;
    [SerializeField] private GameObject mouseObject;
    [SerializeField] TempoMenuConfig tempoMenuConfig;
    [SerializeField] private UI_control ui_control;
    [SerializeField] private TimerControl timerControl;
    [SerializeField] private WaveControl waveControl;
    private InputAction BaseAction;

    void Start()
    {
        SpawnPlanet = new List<Transform>();
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
        inBase = true;
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
        SpawnPlanet.Add(transform);
        if (SpawnPlanet.Count == 1)
        {
            //Setar jogador
            //Setar camera follow
        }
    }

    public Transform GetSpawn(int planet)
    {
        return SpawnPlanet[planet];
    }

    public void TrasportarPlayer(int planet)
    {
        //Setar currentplanet
        //Setar player para planeta
    }
}
