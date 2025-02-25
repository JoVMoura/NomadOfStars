using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerShooter2D : MonoBehaviour
{
    [SerializeField]private GameObject shotPrefab;
    [SerializeField]private float shootDelay;
    [SerializeField]private Transform shootOrigin;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Slider healthBar;

    [SerializeField] private float maxHealth;
    private float actualHealth;
    private float porcent; 

    private int enemiesCount = 0;
    private bool shooting;

    private void Start()
    {
        actualHealth = maxHealth;
        porcent = maxHealth/100;

        shooting = false;
    }

    public void OnEnemyEnter()
    {
        enemiesCount++;
        if (shooting != true)
        {
            shooting = true;
            StartCoroutine(ShootRoutine());
        }
    }

    public void OnEnemyExit()
    {
        enemiesCount--;
    }

    private IEnumerator ShootRoutine()
    {
        while (enemiesCount > 0)
        {
            GameObject targetEnemy = GameObject.FindGameObjectWithTag("Enemy");

            if (targetEnemy != null)
            {
                GameObject newShot = Instantiate(shotPrefab, shootOrigin.position, Quaternion.identity);
                newShot.GetComponent<BulletControl>().SetTarget(targetEnemy.transform);
            }
            yield return new WaitForSeconds(shootDelay);
        }
        shooting = false;
    }

    public bool towerTakeDamage(float _damage)
    {
        actualHealth -= _damage;
        
        if(actualHealth > 0)
        {
            if(!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
            healthBar.value = actualHealth/(porcent*100);

            return false;
        }
        else
        {
            Destroy(this.gameObject);
            return true;
        }
    }

}
