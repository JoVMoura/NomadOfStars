using UnityEngine;

public class ExplosionBomb : MonoBehaviour
{
    public float explosionRadius = 3f; // Raio da explosão
    public float damage = 20f; // Dano aplicado
    public GameObject explosionEffect; // Prefab do efeito de explosão
    public float explosionDuration = 1f; // Tempo antes de destruir a bomba e o efeito

    void Start()
    {
        // Criar efeito visual da explosão
        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Debug.Log("Efeito de explosão instanciado na posição: " + effect.transform.position);
            Destroy(effect, explosionDuration);
        }

        // Encontrar todos os inimigos no raio de explosão
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("inimigo"))
            {
                // Aplica dano ao inimigo (se ele tiver um script de vida)
                // EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                // if (enemyHealth != null)
                // {
                //     enemyHealth.TakeDamage(damage);
                // }

                Debug.Log("Bomba atingiu: " + enemy.name + " causando " + damage + " de dano.");
            }
        }

        Debug.Log("Destruindo a bomba em " + explosionDuration + " segundos.");
        // Destroi a bomba após o tempo de explosão
        Destroy(gameObject, explosionDuration);
    }

    // Debug para ver o raio da explosão no editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
