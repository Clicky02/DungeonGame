using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIAbilityScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int ability = 1;
    public Player p;
    public Control c;


    public void OnPointerDown(PointerEventData eventData)
    {
        c.abilityActive = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        c.abilityActive = false;
        if (GetComponent<Button>().interactable)
            c.nextAbility = ability;
    }
}