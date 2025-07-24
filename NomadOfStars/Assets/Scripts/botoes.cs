using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject menuConfig;
    [SerializeField] private GameObject menuBotoes;
    public void AbrirCena(string nomeCena)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeCena);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

        public void AbrirConfig()
    {
        // Ao abrir as configs, esconde os botões principais
        menuConfig.SetActive(true);
        menuBotoes.SetActive(false);
    }

    public void FecharConfig()
    {
        // Ao fechar as configs, mostra os botões principais novamente
        menuConfig.SetActive(false);
        menuBotoes.SetActive(true);
    }
}
