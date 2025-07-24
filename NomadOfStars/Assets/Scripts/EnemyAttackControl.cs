using UnityEngine;

public class EnemyAttackControl : MonoBehaviour
{
    [SerializeField]private EnemyControl enemyControl;
    [SerializeField] private string tag_nave;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower" || collision.gameObject.tag == tag_nave)
        {
            enemyControl.AttackTarget(collision.gameObject);
        }
    }
}
