using UnityEngine;

public class UI_control : MonoBehaviour
{
    [SerializeField] private GameObject menuBuild;
    [SerializeField] private GameObject menuBase;
    [SerializeField] private GameObject menuPlanetas;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menuDerrota;
    [SerializeField] private GameObject menuVitoria;
    [SerializeField] private GameObject menurBotes;

    [SerializeField] private GameObject PerguntaTutorial;

    [SerializeField] private GameObject Tutorial;

    public void AbriBuild()
    {
        menuBuild.SetActive(true);
    }

    public void FecharBuild()
    {
        menuBuild.GetComponent<MenuTorresControl>().VoltarStart();
        menuBuild.SetActive(false);
    }

    public void AbrirTutorial()
    {
        // PerguntaTutorial.SetActive(false);
        Tutorial.SetActive(true);
        
    }

    public void FecharTutorial()
    {
        PerguntaTutorial.SetActive(false);
        Time.timeScale = 1f;
    }
    

    public void AbriBase()
    {
        menuBase.SetActive(true);
    }

    public void FecharBase()
    {
        menuBase.SetActive(false);
    }

    public void AbrirPause()
    {
        Time.timeScale = 0f;
        menuPause.SetActive(true);
        menurBotes.SetActive(true);
    }

    public void FecharPause()
    {
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        menurBotes.SetActive(false);
    }

    public void AbrirPlanetas()
    {
        menuPlanetas.SetActive(true);
        menuBase.SetActive(false);
    }

    public void FecharPlanetas()
    {
        menuPlanetas.SetActive(false);
    }


    public void AbrirDerrota()
    {
        menuDerrota.SetActive(true);
        menurBotes.SetActive(true);
    }

    public void FecharDerrota()
    {
        menuDerrota.SetActive(false);
        menurBotes.SetActive(false);
    }

    public void AbrirVitoria()
    {
        menuVitoria.SetActive(true);
        menurBotes.SetActive(true);
    }

    public void FecharVitoria()
    {
        menuVitoria.SetActive(false);
        menurBotes.SetActive(false);
    }

    
}
