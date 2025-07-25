using UnityEngine;
using TMPro; // Não se esqueça de adicionar esta linha para usar TextMeshPro

public class TutorialCTRL : MonoBehaviour
{
    [Header("Componentes da UI")]
    [Tooltip("O Animator que controla as transições visuais do tutorial.")]
    [SerializeField] private Animator animatorTela;
    [Tooltip("O campo de texto para o título do passo atual.")]
    [SerializeField] private TextMeshProUGUI titleText;
    [Tooltip("O campo de texto para a descrição do passo atual.")]
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    [Header("Botões de Navegação")]
    [SerializeField] private GameObject BotaoProximo;
    [SerializeField] private GameObject BotaoAnterior;

    [Header("Conteúdo do Tutorial")]
    [Tooltip("Crie aqui cada passo do seu tutorial, com título e descrição.")]
    [SerializeField] private TutorialStep[] tutorialSteps;
    
    [Header("Controle Geral")]
    [Tooltip("O painel principal do tutorial, para ser desativado no final.")]
    [SerializeField] private GameObject PaginaTutorial;

    private int cenaAtual;

    // --- Estrutura de Dados para Cada Passo ---
    // [System.Serializable] faz com que esta classe apareça no Inspector da Unity.
    [System.Serializable]
    public class TutorialStep
    {
        public string title;
        [TextArea(3, 10)] // Deixa o campo de texto maior no Inspector
        public string description;
    }

    void OnEnable()
    {
        // OnEnable é chamado sempre que o objeto é ativado.
        // Isso garante que o tutorial reinicie corretamente se for fechado e reaberto.
        cenaAtual = 0;
        UpdateUI();
    }

    // Função central que atualiza toda a interface de uma vez
    private void UpdateUI()
    {
        // Garante que o índice esteja dentro dos limites da nossa lista de passos
        if (cenaAtual < 0 || cenaAtual >= tutorialSteps.Length)
        {
            
            return; // Sai da função se o índice for inválido
        }

        // Pega os dados do passo atual
        TutorialStep currentStep = tutorialSteps[cenaAtual];

        // Atualiza os textos na tela
        titleText.text = currentStep.title;
        descriptionText.text = currentStep.description;

        // Atualiza o Animator
        animatorTela.SetInteger("Cena", cenaAtual);

        // Atualiza a visibilidade dos botões de forma inteligente
        // O botão "Anterior" só aparece se não estivermos no primeiro passo (índice 0)
        BotaoAnterior.SetActive(cenaAtual > 0);
    }

    public void PassarTutorial()
    {
        cenaAtual++;

        // Verifica se passamos do último passo
        if (cenaAtual >= tutorialSteps.Length)
        {
            Time.timeScale = 1f;
            // Se sim, fecha a página do tutorial
            PaginaTutorial.SetActive(false);
        }
        else
        {
            // Senão, apenas atualiza a UI para o novo passo
            UpdateUI();
        }
    }

    public void VoltarTutorial()
    {
        cenaAtual--;
        
        // A lógica de desativar o botão já está no UpdateUI,
        // então só precisamos chamar a função.
        UpdateUI();
    }
}