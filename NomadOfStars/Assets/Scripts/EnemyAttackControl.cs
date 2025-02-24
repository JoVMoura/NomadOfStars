using UnityEngine;

public class EnemyAttackControl : MonoBehaviour
{
    [SerializeField]private EnemyControl enemyControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Base")
        {
            enemyControl.AttackTarget(collision.gameObject);
        }
    }
}
