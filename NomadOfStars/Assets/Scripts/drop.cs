using TMPro;
using UnityEngine;
using System;

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

            // Toca o som de pegar o item no ponto onde o objeto está
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 10.5f);

            // Destroi o objeto de drop
            Destroy(gameObject);
        }
    }
}
