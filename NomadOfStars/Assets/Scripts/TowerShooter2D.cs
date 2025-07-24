using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TowerShooter2D : MonoBehaviour
{
    [SerializeField]private List<GameObject> enemysIn;
    [SerializeField]private GameObject shotPrefab;
    [SerializeField]private float shootDelay;
    [SerializeField]private Transform shootOrigin;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Slider healthBar;

    [SerializeField] private float maxHealth;
    private float actualHealth;
    private float porcent; 
    private bool shooting;

    private void Start()
    {
        actualHealth = maxHealth;
        porcent = maxHealth/100;

        shooting = false;
    }

    public void OnEnemyEnter(GameObject Enemy)
    {
        enemysIn.Add(Enemy);
        if (shooting != true)
        {
            shooting = true;
            StartCoroutine(ShootRoutine());
        }
    }

    public void OnEnemyExit(GameObject Enemy)
    {
        enemysIn.Remove(Enemy);
    }

    private IEnumerator ShootRoutine()
    {
        while (enemysIn.Count > 0)
        {
            Debug.Log("Estou na rotina, esse é meu inimigo: " + enemysIn[0].name);

            if (enemysIn[0] != null)
            {
                GameObject newShot = Instantiate(shotPrefab, shootOrigin.position, Quaternion.identity);
                newShot.GetComponent<BulletControl>().SetTarget(enemysIn[0].transform);
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
