using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using Mono.Cecil;

public class ItenControl : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    [SerializeField] private Camera cam;
    [SerializeField] private float tamanhoMaximoLaser;
    [SerializeField] private ColiderLaser cLaser;
    private bool foraRotina;
    private bool pararRotina;
    private InputAction pointAction;
    private Vector2 mousePos;
    void Start()
    {
        foraRotina = true;
        pointAction = InputSystem.actions.FindAction("Point");

    }

    public void UsarItem()
    {
        if (foraRotina)
        {
            foraRotina = false;
            StartCoroutine(UsandoLaser());
        }
        else if (pararRotina)
        {
            pararRotina = false;
        }
    }

    public void ParaUsarItem()
    {
        if (!foraRotina)
        {
            pararRotina = true;
        }
    }

    private IEnumerator UsandoLaser()
    {
        GameObject laser = this.transform.GetChild(0).gameObject;
        GameObject coliderLaser = laser.transform.GetChild(0).gameObject;
        GameObject objQuebrar;
        float distanciaMouse = 0;
        bool iniciandoLaser = true;

        while (!pararRotina && iniciandoLaser)
        {
            mousePos = cam.ScreenToWorldPoint(new Vector3(pointAction.ReadValue<Vector2>().x, pointAction.ReadValue<Vector2>().y, cam.nearClipPlane));
            distanciaMouse = Mathf.Sqrt(Mathf.Pow(mousePos.x - laser.transform.position.x, 2) + Mathf.Pow(mousePos.y - laser.transform.position.y, 2));
            laser.GetComponent<SpriteRenderer>().size = new Vector2(laser.GetComponent<SpriteRenderer>().size.x + 2, laser.GetComponent<SpriteRenderer>().size.y);
            coliderLaser.transform.localPosition = new Vector3(coliderLaser.transform.localPosition.x + 2, coliderLaser.transform.localPosition.y, coliderLaser.transform.localPosition.z);
            if (laser.GetComponent<SpriteRenderer>().size.x >= distanciaMouse)
            {
                laser.GetComponent<SpriteRenderer>().size = new Vector2(distanciaMouse, laser.GetComponent<SpriteRenderer>().size.y);
                coliderLaser.transform.localPosition = new Vector3(distanciaMouse, coliderLaser.transform.localPosition.y, coliderLaser.transform.localPosition.z);
                iniciandoLaser = false;
            }
            /*Debug.Log("Distancia Laser pro mouse: " + distanciaMouse);
            Debug.Log("X do Laser: " + laser.transform.position.x +" || Y do Laser: " +laser.transform.position.y);
            Debug.Log("X do Mouse: " + mousePos.x +" || Y do Mouse: " + mousePos.y);*/
            yield return null;
        }

        while (!pararRotina)
        {
            mousePos = cam.ScreenToWorldPoint(new Vector3(pointAction.ReadValue<Vector2>().x, pointAction.ReadValue<Vector2>().y, cam.nearClipPlane));
            distanciaMouse = Mathf.Sqrt(Mathf.Pow(mousePos.x - laser.transform.position.x, 2) + Mathf.Pow(mousePos.y - laser.transform.position.y, 2));
            laser.GetComponent<SpriteRenderer>().size = new Vector2(distanciaMouse, laser.GetComponent<SpriteRenderer>().size.y);
            coliderLaser.transform.localPosition = new Vector3(distanciaMouse, coliderLaser.transform.localPosition.y, coliderLaser.transform.localPosition.z);

            objQuebrar = cLaser.QuebrarObjeto();
            if (objQuebrar != null)
            {
                objQuebrar.GetComponent<Breakable>().Break(1);
            }
            yield return null;
        }

        laser.GetComponent<SpriteRenderer>().size = new Vector2(0, laser.GetComponent<SpriteRenderer>().size.y);
        coliderLaser.transform.localPosition = new Vector3(0, coliderLaser.transform.localPosition.y, coliderLaser.transform.localPosition.z);
        foraRotina = true;
        pararRotina = false;
    }
}
