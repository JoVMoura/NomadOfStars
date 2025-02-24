using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //Atributos do inimigo
    [SerializeField]private float healh;
    [SerializeField]private float damage;
    [SerializeField]private float movementSpeed;
    [SerializeField]private float attackSpeed;
    [SerializeField]private bool towerFocus; //Se false o inimgo só irá focar na base e irá ignorar o resto das torres
    
    private Rigidbody2D enemyRigidbody;
    private GameObject target;
    private bool move;
    private List<GameObject> queue = new List<GameObject>();
    private int queueSize;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();

        target = GameObject.FindWithTag("Base");

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
            //Executar Ataque
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
}
