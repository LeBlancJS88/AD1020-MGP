using UnityEngine;
using UnityEngine.EventSystems;

public class UITapHandler : MonoBehaviour, IPointerClickHandler
{
    public StressManager stressManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("I'm being touched.");
        stressManager.StressClick();
    }
}
