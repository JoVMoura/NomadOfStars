using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void AbrirCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
