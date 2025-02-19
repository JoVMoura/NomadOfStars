using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class HoldingControl : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [HideInInspector]public bool shoot;
    [HideInInspector]public bool usable;
    public Animator item_Animator;
    private InputAction useAction;
    private InputAction pointAction;
    private string targetTag = "Finish";
    [SerializeField] private LayerMask layerMask;
    private Vector2 mousePos;
    private Vector3 point;

    void Start()
    {
        useAction = InputSystem.actions.FindAction("Use");
        pointAction = InputSystem.actions.FindAction("Point");

        shoot = false;
        usable = true;
    }

    void Update()
    {
        if(useAction.IsPressed())
        {
            if(usable)
            {
                item_Animator.SetBool("Use", true);
            }
        }
        else
        {
            if(usable)
            {
                item_Animator.SetBool("Use", false);
            }
        }

        mousePos = pointAction.ReadValue<Vector2>();
        Vector2 center = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        // Enfase q precisa dessa layermask viu, pq isso aqui custaria mt
        // de performance sem a layermask para fechar quais layers o raycast pode ver
        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Objeto que o mouse achou => " + hitObject.name);
        }
    }

    void TrocarItem(HeldItem item)
    {
        if(item.shoot)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }
    }
}
