using UnityEngine;

public class DirectionItem : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [SerializeField]private float range;
    [SerializeField]private Animator item_Animator;
    private Vector2 ajuste;
    private float mouseAngle;
    private bool left;

    void Start()
    {
        ajuste = new Vector2(0.5f,0.5f);
        left = false;
    }

    void Update()
    {
        Vector2 look = cam.ScreenToViewportPoint(Input.mousePosition);
        look = look - ajuste;

        if((look.x <= 0f - range || look.x >= 0f + range) || (look.y <= 0f - range || look.y >= 0f + range))
        {
            mouseAngle = Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg;
            if(left && (Mathf.Abs(mouseAngle)<90))
            {
                this.transform.localPosition = this.transform.localPosition * (-1f);
                item_Animator.SetBool("Left", false);
                left = false;
            }
            else if(!left && (Mathf.Abs(mouseAngle)>90))
            {
                this.transform.localPosition = this.transform.localPosition * (-1f);
                item_Animator.SetBool("Left", true);
                left = true;
            }
            this.transform.localRotation = Quaternion.Euler(0, 0, mouseAngle);
        }

        //Debug.Log("Angulo: "+ Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg);
    }
}