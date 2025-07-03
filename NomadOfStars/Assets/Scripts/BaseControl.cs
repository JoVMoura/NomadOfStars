using UnityEngine;
using UnityEngine.UI;

public class BaseControl : MonoBehaviour
{
    [SerializeField] private TimerControl timerControl;
    [SerializeField] private WaveControl waveControl;

    [SerializeField] private GameObject canvasInfo;
    [SerializeField] private UI_control uiControl;
    [SerializeField] private GameObject canvasHB;
    [SerializeField] private Slider healthBar;

    [SerializeField] private float maxHealth;
    private float actualHealth;
    private float porcent;


    void Start()
    {
        actualHealth = maxHealth;
        porcent = maxHealth / 100;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canvasInfo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canvasInfo.SetActive(false); 
        }
    }*/

    public void BaseTakeDamage(float _damage)
    {
        actualHealth -= _damage;
        
        if(actualHealth > 0)
        {
            if(!canvasHB.activeSelf)
            {
                canvasHB.SetActive(true);
            }
            healthBar.value = actualHealth / (porcent * 100);
        }
        else
        {
            Destroy(this.gameObject);
            // Derrota: Abre o menu de derrota
            uiControl.AbrirDerrota();
        }
    }
}
