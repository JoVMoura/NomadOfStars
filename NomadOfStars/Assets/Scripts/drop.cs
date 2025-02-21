using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound; // Som ao pegar o item

    // Método chamado quando outro collider entra em contato com o collider deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu possui a tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Toca o som de pegar o item no ponto onde o objeto está
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 5.5f);

            
            // Destroi o objeto de drop
            Destroy(gameObject);
        }
    }
}
