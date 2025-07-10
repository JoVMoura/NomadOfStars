using UnityEngine;

public class BaseProx : MonoBehaviour
{
    private GameControl gameControl;

    void Start()
    {
        gameControl = GameObject.Find("Brain").GetComponent<GameControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameControl = GameObject.Find("Brain").GetComponent<GameControl>();
        if (collision.gameObject.tag == "Player")
        {
            gameControl.SetInBase(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameControl.SetInBase(false);
        }
    }
}
