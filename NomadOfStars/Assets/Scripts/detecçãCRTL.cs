using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detecçãCRTL : MonoBehaviour
{
    public string _tagTargetDetc = "Player";

    public List<Collider2D> detectedObjs = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _tagTargetDetc)
        {
            detectedObjs.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _tagTargetDetc)
        {
            detectedObjs.Remove(collision);
        }
    }
}
