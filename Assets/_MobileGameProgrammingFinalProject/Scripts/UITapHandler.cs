using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class UITapHandler : MonoBehaviour, IPointerClickHandler
{
    public StressManager stressManager;
    [SerializeField] internal TMP_Text stressTextPrefab;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("I'm being touched.");
        stressManager.StressClick();

        double stressClickAmount = stressManager.stressUpgradeAmount * stressManager.stressMultiplierAmount;

        // Instantiate a new text element at the tap position
        TMP_Text spawnedText = Instantiate(stressTextPrefab, canvas.transform, false);
        spawnedText.text =  "+ " + stressClickAmount + "\n Stress";

        // Convert the tap position from screen space to canvas space
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out localPoint);
        spawnedText.transform.localPosition = localPoint;

        // Start the coroutine to move the text
        StartCoroutine(MoveTextUpwards(spawnedText));

        // Schedule the text to be hidden and destroyed
        Destroy(spawnedText.gameObject, 1f);
    }

    private IEnumerator MoveTextUpwards(TMP_Text textElement)
    {
        float duration = 0.5f;
        float distance = 10f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            textElement.transform.localPosition += Vector3.up * (distance * Time.deltaTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
