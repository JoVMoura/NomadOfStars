using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound; // Som ao pegar o item

    // Este método é chamado quando outro collider entra em contato com o collider deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu possui a tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Tenta obter o componente PlayerInventory do objeto Player
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                // Verifica se este drop é de madeira ou de pedra e atualiza a variável correspondente
                if (gameObject.CompareTag("dropM"))
                {
                    inventory.drop_madeira++; // Atualiza a variável de drop de madeira
                }
                else if (gameObject.CompareTag("dropP"))
                {
                    inventory.drop_pedra++; // Atualiza a variável de drop de pedra
                }
            }

            // Toca o som de pegar o item no ponto onde o objeto está
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 10.5f);

            // Destroi o objeto de drop
            Destroy(gameObject);
        }
    }
}
