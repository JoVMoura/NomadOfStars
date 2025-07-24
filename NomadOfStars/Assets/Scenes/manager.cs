using UnityEngine;
using UnityEngine.SceneManagement;

// Corrigido e melhorado para gerenciar os painéis da UI
public class MenuManager : MonoBehaviour
{
    // Arraste aqui o GameObject do painel de Configurações
    [SerializeField] private GameObject menuConfig; 
    
    // Arraste aqui o GameObject do painel com os botões principais
    [SerializeField] private GameObject menuBotoes; 

    void Start()
    {
        // Garante que o estado inicial está correto
        menuConfig.SetActive(false);
        menuBotoes.SetActive(true);
    }

    public void Jogar()
    {
        // Lembre-se de mudar o som da cena ANTES de carregar
        // Time.timeScale = 1f; // Se você usa isso para pausar, é bom garantir que o tempo volte ao normal
        SceneManager.LoadScene("Intro"); // Altere para o nome correto da sua cena
    }

    public void Sair()
    {
        Application.Quit();
        // Se estiver no editor da Unity, a linha abaixo ajuda a visualizar
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
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