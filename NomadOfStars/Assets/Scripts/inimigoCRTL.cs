using UnityEngine;

public class SlimeFollowPlayer : MonoBehaviour
{
    public float moveSpeed = 3.5f;  // Velocidade do slime
    public Transform player;  // Referência para o jogador

    private Rigidbody2D rb;

    void Start()
    {
        // Obter o Rigidbody2D do slime
        rb = GetComponent<Rigidbody2D>();

        // Certificar-se de que o player está atribuído (caso contrário, procura o player com a tag "Player")
        if (player == null)
        {
            player = GameObject.FindWithTag("Base").transform;
        }
    }

    void FixedUpdate()
    {
        // Calcular a direção em relação ao player
        Vector2 direction = (player.position - transform.position).normalized;

        // Mover o slime em direção ao player
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
