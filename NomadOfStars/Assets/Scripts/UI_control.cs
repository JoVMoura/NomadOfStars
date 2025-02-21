using UnityEngine;

public class UI_control : MonoBehaviour
{
    [SerializeField] private GameObject menuBuild;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void AbriBuild()
    {
        menuBuild.SetActive(true);
    }

    public void FecharBuild()
    {
        menuBuild.SetActive(false);
    }
}
