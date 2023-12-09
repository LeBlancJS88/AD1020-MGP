using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public StressManager stressManager;
    public IdleManager idleManager;

    [SerializeField] internal GameObject stressTabButton;
    [SerializeField] internal GameObject anxietyTabButton;
    [SerializeField] internal GameObject depressionTabButton;
    [SerializeField] internal GameObject burnoutTabButton;

    [SerializeField] internal GameObject stressTab;
    [SerializeField] internal GameObject anxietyTab;
    [SerializeField] internal GameObject depressionTab;
    [SerializeField] internal GameObject burnoutTab;

    [SerializeField] internal GameObject stressUpgradeButton;
    [SerializeField] internal GameObject stressUpgradeTab;
    [SerializeField] internal GameObject stressGeneratorsButton;
    [SerializeField] internal GameObject stressGeneratorsTab;


    [SerializeField] internal GameObject optionsButton;
    [SerializeField] internal GameObject optionsMenuCloseButton;
    [SerializeField] internal GameObject optionsPanel;

    [SerializeField] internal GameObject ClickAreaPanel;

    [SerializeField] internal GameObject deleteDataConfirmationPanel;

    [SerializeField] internal TMP_Text pulsingTextElement;
    private Coroutine pulsingCoroutine;


    internal float tickTimer = 0f;

    internal bool stressGeneratorUnlocked = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stressManager = FindObjectOfType<StressManager>();
        idleManager = FindObjectOfType<IdleManager>();
    }
    private void Start()
    {
        stressTabButton.SetActive(true);
        stressTab.SetActive(true);
        stressUpgradeTab.SetActive(true);
        StartPulsingText();
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer >= 1f)
        {
            tickTimer = 0f;
            if (stressManager.stressCount >= 1000)
            {
                CheckForStressGeneratorsUnlock();
            }
            if (stressManager.stressCount >= 10000 && !idleManager.anxietyResourceEnabled)
            {
                CheckForAnxietyTabUnlock();
            }
            if (idleManager.anxietyCount >= 100000 && !idleManager.depressionResourceEnabled)
            {
                CheckForDepressionTabUnlock();
            }
        }
    }

    private void CheckForStressGeneratorsUnlock()
    {
        if (stressManager.stressCount >= 1000 && !stressGeneratorUnlocked)
        {
            stressGeneratorUnlocked = true;
            stressGeneratorsButton.SetActive(true);
        }
    }

    private void CheckForAnxietyTabUnlock()
    {
        if (stressManager.stressCount >= 10000)
        {
            anxietyTabButton.SetActive(true);
        }
    }

    private void CheckForDepressionTabUnlock()
    {
        if (stressManager.stressCount >= 100000)
        {
            depressionTabButton.SetActive(true);
        }
    }

    public void StressTabClick()
    {
        stressTab.SetActive(true);
        anxietyTab.SetActive(false);
        depressionTab.SetActive(false);
        burnoutTab.SetActive(false);
    }

    public void StressUpgradeTabClick()
    {
        stressUpgradeTab.SetActive(true);
        stressGeneratorsTab.SetActive(false);
    }

    public void StressGeneratorTabClick()
    {
        stressUpgradeTab.SetActive(false);
        stressGeneratorsTab.SetActive(true);
    }

    public void AnxietyTabClick()
    {
        stressTab.SetActive(false);
        anxietyTab.SetActive(true);
        depressionTab.SetActive(false);
        burnoutTab.SetActive(false);
    }

    public void DepressionTabClick()
    {
        stressTab.SetActive(false);
        anxietyTab.SetActive(false);
        depressionTab.SetActive(true);
        burnoutTab.SetActive(false);
    }

    public void BurnoutTabClick()
    {
        stressTab.SetActive(false);
        anxietyTab.SetActive(false);
        depressionTab.SetActive(false);
        burnoutTab.SetActive(true);
    }

    public void OptionsMenuClick()
    {
        optionsPanel.SetActive(true);
        ClickAreaPanel.SetActive(false);
    }

    public void OptionsMenuClose()
    {
        optionsPanel.SetActive(false);
        ClickAreaPanel.SetActive(true);
    }

    public void DeleteDataClick()
    {
        deleteDataConfirmationPanel.SetActive(true);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();        
        PlayerPrefs.Save();
        gameManager.Load();
        Debug.Log("All PlayerPrefs data deleted.");
        deleteDataConfirmationPanel.SetActive(false);
        SceneManager.LoadScene(0);
        
    }

    public void CancelDeleteDataClick()
    {
        deleteDataConfirmationPanel.SetActive(false);
    }

    public void StartPulsingText()
    {
        if (pulsingCoroutine == null)
        {
            pulsingCoroutine = StartCoroutine(PulseText());
        }
    }

    public void StopPulsingText()
    {
        if (pulsingCoroutine != null)
        {
            StopCoroutine(pulsingCoroutine);
            pulsingCoroutine = null;
            pulsingTextElement.gameObject.SetActive(false);
        }
    }

    private IEnumerator PulseText()
    {
        float totalDuration = 10f; // Total duration for pulsing
        float elapsedTime = 0f; // Timer to track elapsed time

        while (elapsedTime < totalDuration)
        {
            // Increase alpha over 1 second, pause for 0.2, Decrease alpha over 1 second, pause for 0.2
            yield return ChangeAlpha(1f, 1f);
            yield return new WaitForSeconds(0.2f);
            yield return ChangeAlpha(0f, 1f);
            yield return new WaitForSeconds(0.2f);

            elapsedTime += 2.4f; // Update elapsed time (1s + 0.2s + 1s + 0.2s)
        }

        StopPulsingText(); // Optional, to clean up after the coroutine finishes
    }

    private IEnumerator ChangeAlpha(float targetAlpha, float duration)
    {
        Color currentColor = pulsingTextElement.color;
        float startAlpha = currentColor.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            pulsingTextElement.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }
    }
}
