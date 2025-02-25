using UnityEngine;
using UnityEngine.UI;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int damageType; // 0 para inquebravel, 1 para machado, 2 para picareta
    [SerializeField] private float maxHealth;
    private float actualHealth;
    private float porcent; 

    void Start()
    {
        actualHealth = maxHealth;
        porcent = maxHealth/100;
    }

    public bool Break(int damage)
    {
        actualHealth -= damage;

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
            Instantiate(drop, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            return true;
        }
    }
}
