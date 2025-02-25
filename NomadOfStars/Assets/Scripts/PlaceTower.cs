using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceTower : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private UI_control ui_control;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Grid grid;
    [SerializeField] private  PlayerInventory playerInventory;
    [SerializeField] private Torres towers;
    private InputAction buildAction;
    private InputAction useAction;
    private InputAction pointAction;
    private bool possuiObjeto;
    private Vector3 mousePosition;
    private Vector3 lastDetectedPosition;
    private bool inMenu;
    private GameObject actualTower;
    void Start()
    {
        lastDetectedPosition = new Vector3(100000,100000,0);
        mousePosition = new Vector3(1,1,0);
        possuiObjeto = false;
        inMenu = false;
        useAction = InputSystem.actions.FindAction("use");
        buildAction = InputSystem.actions.FindAction("Build");
        pointAction = InputSystem.actions.FindAction("Point");
    }

    public void StartPlacement(int ID)
    {
        if(towers.objectsData[ID].WoodPrice <= playerInventory.drop_madeira && towers.objectsData[ID].StonePrice <= playerInventory.drop_pedra)
        {
            ui_control.FecharBuild();
            inMenu = false;
            possuiObjeto = true;
            actualTower = Instantiate(towers.objectsData[ID].Struct, mousePosition, Quaternion.identity);
            actualTower.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

            playerInventory.tirar_madeira(towers.objectsData[ID].WoodPrice);
            playerInventory.tirar_pedra(towers.objectsData[ID].StonePrice);
        }
        else
        {
            //error
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(buildAction.WasPressedThisFrame() && !inMenu && !possuiObjeto)
        {
            inMenu = true;
            ui_control.AbriBuild();
        }
        else if(buildAction.WasPressedThisFrame() && inMenu)
        {
            inMenu = false;
            ui_control.FecharBuild();
        }

        if(possuiObjeto)
        {
            mousePosition = GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition) * 64;
            if(lastDetectedPosition != gridPosition)
            {
                actualTower.transform.position = gridPosition;
                lastDetectedPosition = gridPosition;
            }
        }
        if(possuiObjeto && useAction.WasPressedThisFrame())
        {
            actualTower.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            actualTower = null;
            possuiObjeto = false;
        }
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector2 mousePos = pointAction.ReadValue<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        return worldPos;
    }
}
