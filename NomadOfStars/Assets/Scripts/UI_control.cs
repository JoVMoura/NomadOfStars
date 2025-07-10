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

    public void AbriBuild()
    {
        menuBuild.SetActive(true);
    }

    public void FecharBuild()
    {
        menuBuild.SetActive(false);
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
        menuPause.SetActive(true);
        menurBotes.SetActive(true);
    }

    public void FecharPause()
    {
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
