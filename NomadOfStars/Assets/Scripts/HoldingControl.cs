using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldingControl : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    //[SerializeField] private AudioClip breakSound;
    [SerializeField] private Animator item_Animator;

    [HideInInspector] public bool shoot;
    [HideInInspector] public bool usable;
    //private bool quebrando;

    private InputAction useAction;
    private InputAction pointAction;
    private Vector2 mousePos;

    void Start()
    {
        useAction = InputSystem.actions.FindAction("Use");
        pointAction = InputSystem.actions.FindAction("Point");

        //quebrando = false;
        shoot = false;
        usable = true;
    }

    void Update()
    {
        // Controla a animação do item
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

        if(item_Animator.GetBool("Break"))
        {
            gameControl.Hited();
            item_Animator.SetBool("Break", false);
        }

        // Obtém a posição do mouse e converte para coordenadas do mundo
        mousePos = pointAction.ReadValue<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, layerMask);

        if(hit.collider != null && hit.collider.CompareTag("Breakable"))
        {
            if(gameControl.GetMouseObject() == null || gameControl.GetMouseObject() != hit.collider.gameObject)
            {
                gameControl.SetMouseObject(hit.collider.gameObject);
            }
        }
        else
        {
            gameControl.SetMouseObject(null);
        }
    }

    /*IEnumerator BreakItem(GameObject hitObject)
    {
        // Toca o som de quebra no ponto do objeto
        AudioSource.PlayClipAtPoint(breakSound, hitObject.transform.position, 5.5f);


        // Aguarda 2 segundos antes de destruir e gerar o drop
        yield return new WaitForSeconds(2f);

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
        quebrando = false;
        Destroy(hitObject);
    }*/

    void TrocarItem(HeldItem item)
    {
        shoot = item.shoot;
    }
}
