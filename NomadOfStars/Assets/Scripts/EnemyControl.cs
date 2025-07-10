using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    //Atributos do inimigo
    [SerializeField]private float maxHealth;
    [SerializeField]private float damage;
    [SerializeField]private float movementSpeed;
    [SerializeField]private bool towerFocus; //Se false o inimgo só irá focar na base e irá ignorar o resto das torres

    [SerializeField] private GameObject canvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator enemyAnimator;

    private float actualHealth;
    private float porcent;

    private Rigidbody2D enemyRigidbody;
    private GameObject target;
    private bool move;
    private List<GameObject> queue = new List<GameObject>();
    private int queueSize;

    [SerializeField] private string tag_nave;

    void Start()
    {
        actualHealth = maxHealth;
        porcent = maxHealth/100;

        enemyRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag(tag_nave);
        move = true;
        queueSize = -1;
    }

    void Update()
    {
        if(target == null)
        {
            if(queueSize != -1)
            {
                target = queue[queueSize];
                queue.RemoveAt(queueSize);
                queueSize--;
            }
            else
            {
                //Caso a base seja destruida
                move = false;
            }
        }

        if(!move)
        {
            enemyAnimator.SetBool("Move", false);
            enemyAnimator.SetBool("Attack", true);
        }
        else if(!enemyAnimator.GetBool("Move") || enemyAnimator.GetBool("Attack"))
        {
            enemyAnimator.SetBool("Move", true);
            enemyAnimator.SetBool("Attack", false);
        }

        if(enemyAnimator.GetBool("WasAttacked"))
        {
            enemyAnimator.SetBool("WasAttacked", false);
            if(target.tag == "Tower")
            {
                if(target.GetComponentInParent<TowerShooter2D>().towerTakeDamage(damage))
                {
                    move = true;
                }
            }
            else if(target.tag == "Base")
            {
                target.GetComponent<BaseControl>().BaseTakeDamage(damage);
            }
        }
    }

    void FixedUpdate()
    {
        if(move)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;

            enemyRigidbody.MovePosition(enemyRigidbody.position + (direction * movementSpeed * Time.fixedDeltaTime));
        }
    }

    public void AddTarget(GameObject newFollowTarget)
    {
        if(towerFocus)
        {
            if(target.tag == "Base")
            {
                queue.Add(target);
                target = newFollowTarget;
                queueSize++;
            }
            else
            {
                queue.Add(newFollowTarget);
                queueSize++;
            }
        }
    }

    public void AttackTarget(GameObject newAtackTarget)
    {
        if(newAtackTarget == target)
        {
            move = false;
        }
    }

    public void enemyTakeDamage(float _damage)
    {
        actualHealth -= _damage;
        
        if(actualHealth > 0)
        {
            if(!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
            healthBar.value = actualHealth/(porcent*100);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
