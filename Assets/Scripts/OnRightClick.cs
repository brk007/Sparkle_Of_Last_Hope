using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnRightClick : MonoBehaviour, IPointerClickHandler
{
    public DialogueTrigger dialogueTrigger;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            dialogueTrigger.TriggerDialogue();
    }
}