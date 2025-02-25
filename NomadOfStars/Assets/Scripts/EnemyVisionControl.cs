using UnityEngine;

public class EnemyVisionControl : MonoBehaviour
{
    [SerializeField]private EnemyControl enemyControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tower")
        {
            enemyControl.AddTarget(collision.gameObject);
        }
    }
}
