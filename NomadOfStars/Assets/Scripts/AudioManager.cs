using UnityEngine;
using UnityEngine.Audio;

// Este script gerencia todo o áudio do jogo.
// Ele deve ser colocado em um GameObject vazio na sua primeira cena.
public class AudioManager : MonoBehaviour
{
    // --- Padrão Singleton ---
    // Uma referência estática que pode ser acessada de qualquer script
    // usando AudioManager.instance
    public static AudioManager instance;

    private void Awake()
    {
        // Garante que exista apenas uma instância deste objeto
        if (instance != null && instance != this)
        {
            // Se outra instância já existe, esta é destruída
            Destroy(this.gameObject);
            return;
        }

        // Se não existe, esta se torna a instância principal
        instance = this;

        // Garante que este objeto não seja destruído ao carregar novas cenas
        DontDestroyOnLoad(this.gameObject);
    }
    // --- Fim do Padrão Singleton ---


    // --- Variáveis de Áudio (Para arrastar no Inspector) ---

    // Arraste aqui o seu asset "GameAudioMixer"
    [SerializeField] private AudioMixer gameAudioMixer;

    // Arraste aqui o grupo "SFX" de dentro da janela do Audio Mixer
    [SerializeField] private AudioMixerGroup sfxMixerGroup;


    // --- Funções para os Sliders de Volume ---

    public void SetMasterVolume(float volume)
    {
        // Se o volume for próximo de zero, define como silêncio (-80dB) para evitar erros matemáticos
        if (volume <= 0.0001f)
        {
            gameAudioMixer.SetFloat("MasterVolume", -80f);
        }
        else // Senão, calcula o logaritmo normalmente para uma sensação de volume natural
        {
            gameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            gameAudioMixer.SetFloat("MusicVolume", -80f);
        }
        else
        {
            gameAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            gameAudioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            gameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
    }


    // --- Função Auxiliar para Efeitos Sonoros ---

    // Função para tocar efeitos sonoros em qualquer ponto do mundo,
    // garantindo que o som saia pelo canal de SFX.
    public void PlaySFXAtPoint(AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        // 1. Cria um objeto temporário no local do som
        GameObject tempAudioObject = new GameObject("TempAudio");
        tempAudioObject.transform.position = position;

        // 2. Adiciona um componente AudioSource
        AudioSource audioSource = tempAudioObject.AddComponent<AudioSource>();

        // 3. Define as propriedades do som
        audioSource.clip = clip;
        audioSource.volume = volume;

        // 4. A PARTE MAIS IMPORTANTE: Define o grupo de saída para SFX
        audioSource.outputAudioMixerGroup = sfxMixerGroup;

        // 5. Toca o som
        audioSource.Play();

        // 6. Destrói o objeto temporário após o clipe terminar de tocar
        Destroy(tempAudioObject, clip.length);
    }
}