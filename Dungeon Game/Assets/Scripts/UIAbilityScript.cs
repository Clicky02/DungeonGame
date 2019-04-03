using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIAbilityScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int ability = 1;
    public Player p;


    public void OnPointerDown(PointerEventData eventData)
    {
        Control.c.abilityActive = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Control.c.abilityActive = false;
        if (GetComponent<Button>().interactable)
            Control.c.nextAbility = ability;
    }
}