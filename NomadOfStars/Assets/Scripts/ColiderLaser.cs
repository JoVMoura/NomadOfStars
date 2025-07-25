using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColiderLaser : MonoBehaviour
{
    private List<GameObject> MaterialColidido = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Breakable")
        {
            MaterialColidido.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Breakable")
        {
            MaterialColidido.Remove(collision.gameObject);
        }
    }

    public GameObject QuebrarObjeto()
    {
        if (MaterialColidido.Count > 0)
        {
            return MaterialColidido[0];
        }
        return null;
    }
}
