using UnityEngine;

public class BulletFollow : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;
    private Transform target;

    // Método para definir o alvo do tiro (chamado pela torre)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            // Mover em direção ao alvo
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Rotacionar o tiro para apontar na direção do inimigo
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 120f;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            // Se o alvo for destruído antes de ser atingido, destruir o tiro
            Destroy(gameObject);
        }
    }

    // Detecta colisão com o inimigo e aplica dano
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("inimigo"))
        {
            // Aplicar dano ao inimigo (se houver um script de vida)
            // EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            // if (enemy != null) enemy.TakeDamage(damage);

            Debug.Log("Tiro atingiu " + other.name + " e causou " + damage + " de dano.");
            Destroy(gameObject);
        }
    }
}
