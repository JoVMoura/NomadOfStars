using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private int tipo; // 1 para madeira, 2 para pedra

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                if (tipo == 1)
                {
                    inventory.drop_madeira_count();
                }
                else if (tipo == 2)
                {
                    inventory.drop_pedra_count();
                }
                else if (tipo == 3)
                {
                    GameObject.Find("Brain").GetComponent<GameControl>().PegarCristal();
                }
            }

            // Toca o som de pegar o item usando nosso sistema centralizado
            // O som agora vai respeitar o volume de SFX
            if (pickupSound != null && AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFXAtPoint(pickupSound, transform.position);
            }

            // Destroi o objeto de drop
            Destroy(gameObject);
        }
    }
}