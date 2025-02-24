using UnityEngine;

public class RangeTrigger2D : MonoBehaviour
{
    // Referência para o script TowerShooter2D no objeto pai
    public TowerShooter2D towerShooter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (towerShooter != null)
        {
            towerShooter.OnEnemyEnter(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (towerShooter != null)
        {
            towerShooter.OnEnemyExit(other);
        }
    }
}
