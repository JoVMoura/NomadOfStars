using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Este componente precisa de um 'Image' para funcionar.
// A linha abaixo garante que um componente 'Image' seja adicionado automaticamente.
[RequireComponent(typeof(Image))]
public class SpriteAnimator : MonoBehaviour
{
    // --- Campos para configurar a animação no Inspector ---

    // A lista de sprites (frames) que compõem a animação.
    [SerializeField] private Sprite[] frames;
    
    // A velocidade da animação.
    [SerializeField] private float framesPerSecond = 10f;
    
    // --- Variáveis de Controle ---
    private Image imageComponent;
    private Coroutine animationCoroutine;

    // Awake é chamado antes de Start. Ótimo para pegar referências.
    void Awake()
    {
        // Pega o componente 'Image' que está no mesmo GameObject deste script.
        imageComponent = GetComponent<Image>();
    }

    // OnEnable é chamado sempre que o objeto se torna ativo.
    void OnEnable()
    {
        // Começa a animação assim que o objeto é ativado.
        if (frames != null && frames.Length > 0)
        {
            StartAnimation();
        }
    }
    
    // OnDisable é chamado quando o objeto é desativado.
    void OnDisable()
    {
        // Para a animação para não consumir recursos desnecessariamente.
        StopAnimation();
    }

    // Inicia (ou reinicia) a animação.
    private void StartAnimation()
    {
        // Se já houver uma animação rodando, pare-a primeiro.
        StopAnimation();
        // Inicia a nova coroutine de animação.
        animationCoroutine = StartCoroutine(Animate());
    }
    
    // Para a animação que estiver rodando.
    private void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    // A Coroutine que faz a "mágica" de trocar os frames.
    private IEnumerator Animate()
    {
        int currentFrame = 0;
        // Calcula o tempo que cada frame deve ficar na tela.
        float delay = 1f / framesPerSecond;

        // Loop infinito que só para quando a Coroutine é interrompida.
        while (true)
        {
            // Define o sprite da imagem para o frame atual.
            imageComponent.sprite = frames[currentFrame];
            
            // Avança para o próximo frame, usando o operador de módulo (%)
            // para voltar ao início quando chegar ao fim da lista.
            currentFrame = (currentFrame + 1) % frames.Length;
            
            // Pausa a execução pelo tempo calculado.
            yield return new WaitForSeconds(delay);
        }
    }
}