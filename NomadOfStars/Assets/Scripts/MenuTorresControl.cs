using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MenuTorresControl : MonoBehaviour
{
    [SerializeField] private PlaceTower placeTower;
    [SerializeField] private GameObject infos;
    [SerializeField] private GameObject btnVotlar;
    [SerializeField] private GameObject[] torres;
    [SerializeField] private Animator animator_Pagina;
    [SerializeField] private TMP_Text txtTitulo;
    [SerializeField] private TMP_Text[] txtDescrição;
    [SerializeField] private TMP_Text txtVida;
    [SerializeField] private TMP_Text txtDano;
    [SerializeField] private TMP_Text txtArea;
    [SerializeField] private TMP_Text txtTD;
    [SerializeField] private string[] titulos;
    [SerializeField] private string[] descricao;
    [SerializeField] private string[] dano;
    [SerializeField] private string[] vida;
    [SerializeField] private string[] area;
    [SerializeField] private string[] tD;

    private int torreAtual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        torres[0].SetActive(true);
        torres[1].SetActive(true);
        torres[2].SetActive(true);
        infos.SetActive(true);
        btnVotlar.SetActive(true);
        torreAtual = -1;
    }

    public void AbrirMenuTorre(int numTorre)
    {
        torreAtual = numTorre;
        animator_Pagina.SetInteger("torreAtual", numTorre);
        txtTitulo.text = titulos[numTorre];
        txtDescrição[0].text = descricao[numTorre];
        txtDescrição[1].text = descricao[numTorre];
        txtVida.text = vida[numTorre];
        txtDano.text = dano[numTorre];
        txtArea.text = area[numTorre];
        txtTD.text = tD[numTorre];
    }

    public void VoltarMenuTorre()
    {
        torreAtual = -1;
        animator_Pagina.SetInteger("torreAtual", -1);
    }

    public void ConstruirTorre()
    {
        if (torreAtual != -1)
        {
            placeTower.StartPlacement(torreAtual);
            torreAtual = -1;
        }
    }

    public void VoltarStart()
    {
        torres[0].SetActive(true);
        torres[1].SetActive(true);
        torres[2].SetActive(true);
        infos.SetActive(true);
        btnVotlar.SetActive(true);
    }
}
