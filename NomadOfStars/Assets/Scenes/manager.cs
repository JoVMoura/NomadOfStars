using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Jogo"); // Altere para o nome correto da sua cena
    }

    public void Sair()
    {
        Application.Quit();
    }
}
