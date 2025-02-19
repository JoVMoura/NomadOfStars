using NUnit.Framework.Constraints;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]private Animator sprite_Animator;
    [SerializeField]private float speed_Player;
    [SerializeField]private float speed_Animation;
    private PlayerControls playerControls;
    private Rigidbody2D rb_Player;
    private Vector3 diretion;
    private bool mov;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        rb_Player = this.transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        diretion = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
        diretion.Normalize();

        mov = diretion.x != 0 || diretion.y != 0;

        if(mov)
        {
            sprite_Animator.SetBool("Mov", true);
        }
        else
        {
            if(sprite_Animator.GetBool("Mov")) 
            {
                sprite_Animator.SetBool("Mov", false);  
            }
        }
        
    }

    void FixedUpdate()
    {
        rb_Player.MovePosition(this.transform.position + (diretion * speed_Player * Time.deltaTime)); 
    }
}
