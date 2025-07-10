using UnityEngine;
using UnityEngine.EventSystems;

public class TempoMenuConfig : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator this_Animator;

    void Start()
    {
        this_Animator = this.GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this_Animator.SetBool("Over", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this_Animator.SetBool("Over", false);
    }

    public void SetOpen(bool state)
    {
        this_Animator.SetBool("Open", state);
    }
}
