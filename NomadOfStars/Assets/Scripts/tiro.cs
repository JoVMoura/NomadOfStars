using UnityEngine;
using System.Collections;

public class TowerShooter2D : MonoBehaviour
{
    // Prefab do tiro a ser instanciado (arraste no Inspector)
    public GameObject shotPrefab;
    // Intervalo entre cada disparo
    public float shootDelay = 1.0f;
    // Transform que define a posição de saída dos tiros (objeto "saida")
    public Transform shootOrigin;

    // Contador de inimigos detectados
    private int enemiesCount = 0;
    // Referência para a coroutine de disparo
    private Coroutine shootCoroutine;

    private void Start()
    {
        Debug.Log("TowerShooter2D iniciado.");
        if (shootOrigin == null)
        {
            Debug.LogError("shootOrigin não definido. Atribua o objeto 'saida' no Inspector.");
        }
    }

    // Método chamado pelo script do range quando um inimigo entra
    public void OnEnemyEnter(Collider2D other)
    {
        if (other.CompareTag("inimigo"))
        {
            enemiesCount++;
            Debug.Log("Inimigo entrou: " + other.name + ". Total: " + enemiesCount);
            if (shootCoroutine == null)
            {
                Debug.Log("Iniciando rotina de disparo.");
                shootCoroutine = StartCoroutine(ShootRoutine());
            }
        }
    }

    // Método chamado pelo script do range quando um inimigo sai
    public void OnEnemyExit(Collider2D other)
    {
        if (other.CompareTag("inimigo"))
        {
            enemiesCount = Mathf.Max(0, enemiesCount - 1);
            Debug.Log("Inimigo saiu: " + other.name + ". Total: " + enemiesCount);
            if (enemiesCount == 0 && shootCoroutine != null)
            {
                Debug.Log("Parando rotina de disparo.");
                StopCoroutine(shootCoroutine);
                shootCoroutine = null;
            }
        }
    }

    // Coroutine que instancia o tiro enquanto houver inimigos no alcance
    private IEnumerator ShootRoutine()
    {
        while (enemiesCount > 0)
        {
            // Encontrar um inimigo dentro do alcance (pode ser o primeiro da lista, mais próximo, etc.)
            GameObject targetEnemy = GameObject.FindGameObjectWithTag("inimigo");

            if (targetEnemy != null)
            {
                // Instanciar o tiro e atribuir o alvo
                GameObject newShot = Instantiate(shotPrefab, shootOrigin.position, Quaternion.identity);
                BulletFollow bullet = newShot.GetComponent<BulletFollow>();

                if (bullet != null)
                {
                    bullet.SetTarget(targetEnemy.transform);
                }

                Debug.Log("Instanciando tiro no inimigo: " + targetEnemy.name);
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }

}
