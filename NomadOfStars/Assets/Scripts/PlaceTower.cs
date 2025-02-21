using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceTower : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Grid grid;
    [SerializeField] private  PlayerInventory playerInventory;
    [SerializeField] private Torres towers;
    private GameObject actualTower;
    private InputAction pointAction;
    void Start()
    {
        pointAction = InputSystem.actions.FindAction("Point");
    }

    public void StartPlacement(int ID)
    {
        if(towers.objectsData[ID].WoodPrice >= playerInventory.drop_madeira && towers.objectsData[ID].StonePrice >= playerInventory.drop_pedra)
        {
            //Fechar tela de seleção
            actualTower = towers.objectsData[ID].Struct;
            SpriteRenderer actualTowerSP = actualTower.transform.GetChild(0).GetComponent<SpriteRenderer>();
            actualTowerSP.color = new Vector4(actualTowerSP.color.r, actualTowerSP.color.g, actualTowerSP.color.b, 0.5f);
            actualTower.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lastDetectedPosition = new Vector3(10000,10000,10000);
        Vector3 mousePosition = GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if(lastDetectedPosition != gridPosition)
        {
            actualTower.transform.position = gridPosition;
            lastDetectedPosition = gridPosition;
        }
    }

     public Vector3 GetSelectedMapPosition()
    {
        Vector3 lastPosition = new Vector3(0,0,0);
        Vector2 mousePos = pointAction.ReadValue<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        // Realiza o raycast utilizando a layermask para otimizar a performance
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, layerMask);

        // Se o raycast atingiu um objeto e o botão de uso foi pressionado neste frame
        if (hit.collider != null)
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}
