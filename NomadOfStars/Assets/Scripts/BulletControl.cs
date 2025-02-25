using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float damage;
    [SerializeField]private float duration;
    [SerializeField]private bool angleMatter;

    private Transform target;

    void Start()
    {
        if(duration > 0)
        {
            StartCoroutine(ShootDuration());
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            if(angleMatter)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 120f;

                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyControl>().enemyTakeDamage(damage);
            if(duration <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator ShootDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
