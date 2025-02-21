using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    private static MusicaManager instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto ao mudar de cena
        }
        else
        {
            Destroy(gameObject); // Se já existir, destrói para evitar duplicação
        }
    }
}
