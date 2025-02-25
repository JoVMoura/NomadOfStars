using UnityEngine;

public class UI_control : MonoBehaviour
{
    [SerializeField] private GameObject menuBuild;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menuDerrota;
    [SerializeField] private GameObject menuVitoria;
    [SerializeField] private GameObject menurBotoes;

    public void AbriBuild()
    {
        menuBuild.SetActive(true);
    }

    public void FecharBuild()
    {
        menuBuild.SetActive(false);
    }

    public void AbrirPause()
    {
        menuPause.SetActive(true);
        menurBotoes.SetActive(true);
    }

    public void FecharPause()
    {
        menuPause.SetActive(false);
        menurBotoes.SetActive(false);
    }

    public void AbrirDerrota()
    {
        menuDerrota.SetActive(true);
        menurBotoes.SetActive(true);
    }

    public void FecharDerrota()
    {
        menuDerrota.SetActive(false);
        menurBotoes.SetActive(false);
    }

    public void AbrirVitoria()
    {
        menuVitoria.SetActive(true);
        menurBotoes.SetActive(true);
    }

    public void FecharVitoria()
    {
        menuVitoria.SetActive(false);
        menurBotoes.SetActive(false);
    }
}
