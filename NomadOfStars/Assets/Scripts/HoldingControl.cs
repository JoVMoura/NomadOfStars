using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
public class HoldingControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject dropArvorePrefab;
    [SerializeField] private GameObject dropPedraPrefab;
    [SerializeField] private LayerMask layerMask;
    
    public Animator item_Animator;
    [HideInInspector] public bool shoot;
    [HideInInspector] public bool usable;
    
    private InputAction useAction;
    private InputAction pointAction;
    private Vector2 mousePos;

    void Start()
    {
        useAction = InputSystem.actions.FindAction("Use");
        pointAction = InputSystem.actions.FindAction("Point");

        shoot = false;
        usable = true;
    }

    void Update()
    {
        // Controla a animação do item
        if (useAction.IsPressed())
        {
            if (usable)
                item_Animator.SetBool("Use", true);
        }
        else
        {
            if (usable)
                item_Animator.SetBool("Use", false);
        }

        // Obtém a posição do mouse e converte para coordenadas do mundo
        mousePos = pointAction.ReadValue<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        // Realiza o raycast utilizando a layermask para otimizar a performance
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, layerMask);

        // Se o raycast atingiu um objeto e o botão de uso foi pressionado neste frame
        if (hit.collider != null && useAction.WasPressedThisFrame())
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Objeto que o mouse achou => " + hitObject.name);

            // Verifica a tag do objeto e instancia o drop correspondente
            if (hitObject.CompareTag("arvore"))
            {
                Instantiate(dropArvorePrefab, hitObject.transform.position, Quaternion.identity);
            }
            else if (hitObject.CompareTag("pedra"))
            {
                Instantiate(dropPedraPrefab, hitObject.transform.position, Quaternion.identity);
            }

            // Destroi o objeto atingido
            Destroy(hitObject);
        }
    }

    void TrocarItem(HeldItem item)
    {
        shoot = item.shoot;
    }
}
