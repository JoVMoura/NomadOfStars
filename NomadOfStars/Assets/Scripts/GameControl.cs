using UnityEngine;

public class GameControl : MonoBehaviour
{
    
    [SerializeField] private GameObject mouseObject;

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
        if(mouseObject != null)
        {
            if(mouseObject.GetComponent<Breakable>().Break(20))
            {
                mouseObject = null;
            }
        }
    }
}
