using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int drop_madeira;
    public int drop_pedra;
    [SerializeField] private TMP_Text textoMadeira;
    [SerializeField] private TMP_Text textoPedra;

    public void drop_madeira_count()
    {
        drop_madeira++;
        textoMadeira.text = drop_madeira.ToString();
    }
    public void drop_pedra_count()
    {
        drop_pedra++;
        textoPedra.text = drop_pedra.ToString();
    }

    public void tirar_madeira(int menos)
    {
        drop_madeira-=menos;
        textoMadeira.text = drop_madeira.ToString();
    }
    public void tirar_pedra(int menos)
    {
        drop_pedra-=menos;
        textoPedra.text = drop_pedra.ToString();
    }
}
