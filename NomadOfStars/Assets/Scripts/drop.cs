using UnityEngine;

public class DropItem : MonoBehaviour
{
    // Método chamado quando outro collider entra em contato com o collider deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu possui a tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Destroi o objeto de drop
            Destroy(gameObject);
        }
    }
}
