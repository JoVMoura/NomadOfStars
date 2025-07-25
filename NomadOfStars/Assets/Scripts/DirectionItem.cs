using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionItem : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [SerializeField]private float range;
    [SerializeField]private float deslocamentoX;
    [SerializeField]private float deslocamentoY;
    [SerializeField]private Animator item_Animator;
    [SerializeField]private SpriteRenderer item_Sprite;
    private Vector2 ajuste;
    private float mouseAngle;
    private bool left;
    private Vector2 mousePos;
    private InputAction pointAction;

    void Start()
    {
        ajuste = new Vector2(0.5f,0.5f);
        left = false;
        pointAction = InputSystem.actions.FindAction("Point");
    }

    void Update()
    {
        if (Time.timeScale == 1f)
        {
            Vector2 look = cam.ScreenToViewportPoint(Input.mousePosition);
            look = look - ajuste;

            if (this.transform.GetChild(0).GetChild(0) != null)
            {
                deslocamentoY = this.transform.GetChild(0).GetChild(0).position.y;
                deslocamentoY = this.transform.GetChild(0).GetChild(0).position.x;
            }

            mousePos = cam.ScreenToWorldPoint(new Vector3(pointAction.ReadValue<Vector2>().x, pointAction.ReadValue<Vector2>().y, cam.nearClipPlane));

            if ((look.x <= 0f - range || look.x >= 0f + range) || (look.y <= 0f - range || look.y >= 0f + range))
            {
                mouseAngle = (Mathf.Atan2(this.transform.position.y - mousePos.y/*+ deslocamentoY*/, this.transform.position.x - mousePos.x /*+ deslocamentoX*/) * Mathf.Rad2Deg) + 180;
                //Debug.Log("Angulo: "+ mouseAngle);
                if (left && (Mathf.Abs(mouseAngle - 180) > 90))
                {
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x * (-1f), this.transform.localPosition.y, this.transform.localPosition.z);
                    this.transform.localRotation = Quaternion.Euler(0, 0, this.transform.localRotation.z);
                    item_Sprite.flipY = false;
                    //item_Animator.SetBool("Left", false);
                    left = false;
                    if (this.transform.GetChild(0).GetChild(0) != null)
                    {
                        this.transform.GetChild(0).GetChild(0).localPosition = new Vector3(49.5f, 18.6f, this.transform.localPosition.z);
                    }
                }
                else if (!left && (Mathf.Abs(mouseAngle - 180) < 90))
                {
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x * (-1f), this.transform.localPosition.y, this.transform.localPosition.z);
                    this.transform.localRotation = Quaternion.Euler(0, 180, this.transform.localRotation.z);
                    item_Sprite.flipY = true;
                    //item_Animator.SetBool("Left", true);
                    left = true;
                    if (this.transform.GetChild(0).GetChild(0) != null)
                    {
                        this.transform.GetChild(0).GetChild(0).localPosition = new Vector3(49.5f, -18.6f, this.transform.localPosition.z);
                    }
                }
                this.transform.localRotation = Quaternion.Euler(0, 0, mouseAngle);
            }
        }
        //Debug.Log("Angulo: "+ Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg);
    }
}