using UnityEngine;

public class RangeTrigger2D : MonoBehaviour
{
    [SerializeField]private TowerShooter2D towerShooter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            towerShooter.OnEnemyEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            towerShooter.OnEnemyExit(); 
        }
    }
}
