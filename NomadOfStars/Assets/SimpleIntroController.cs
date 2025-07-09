using UnityEngine;
using TMPro;
using UnityEngine.UI; // ESSENCIAL: Precisamos disso para controlar a Imagem
using UnityEngine.SceneManagement;

public class SimpleIntroController : MonoBehaviour
{
    // --- Nossos campos públicos para arrastar na Unity ---
    public TextMeshProUGUI textDisplay;
    public Image imageDisplay; // NOVO: Campo para a nossa imagem
    public StoryBlock[] storyBlocks; // MUDANÇA: Agora usamos "blocos de história"
    public string sceneToLoad = "Level1";

    // --- Variáveis de controle internas ---
    private int currentIndex = 0;

    // NOVO: Criamos uma "classe" para organizar melhor.
    // [System.Serializable] permite que isso apareça no Inspector da Unity.
    [System.Serializable]
    public class StoryBlock
    {
        [TextArea(3, 5)]
        public string sentence; // O texto do bloco
        public Sprite image;    // A imagem do bloco
    }

    void Start()
    {
        // Garante que a imagem comece desativada, caso você tenha esquecido
        imageDisplay.gameObject.SetActive(false);

        // Mostra o primeiro bloco da história
        ShowCurrentBlock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentIndex++;

            if (currentIndex < storyBlocks.Length)
            {
                // Mostra o próximo bloco da história
                ShowCurrentBlock();
            }
            else
            {
                // Acabou a introdução, carrega a próxima cena
                Debug.Log("Fim da introdução! Carregando cena: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    // NOVA FUNÇÃO: Para manter o código organizado
    void ShowCurrentBlock()
    {
        // Pega o bloco atual da nossa lista
        StoryBlock currentBlock = storyBlocks[currentIndex];

        // Atualiza o texto na tela
        textDisplay.text = currentBlock.sentence;

        // Verifica se este bloco TEM uma imagem associada
        if (currentBlock.image != null)
        {
            // Se tiver, ativa o objeto de imagem,
            // e define o sprite dele para a imagem do nosso bloco.
            imageDisplay.gameObject.SetActive(true);
            imageDisplay.sprite = currentBlock.image;
        }
        else
        {
            // Se não tiver imagem (o campo ficou como "None"),
            // simplesmente desativa o objeto de imagem.
            imageDisplay.gameObject.SetActive(false);
        }
    }
}