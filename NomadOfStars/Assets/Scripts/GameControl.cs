using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControl : MonoBehaviour
{
    private bool inBase;
    [SerializeField] private GameObject mouseObject;
    [SerializeField] TempoMenuConfig tempoMenuConfig;
    [SerializeField] private UI_control ui_control;
    private InputAction BaseAction;

    void Start()
    {
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
}
