using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public StressManager stressManager;

    [SerializeField] internal GameObject stressTabButton;
    [SerializeField] internal GameObject anxietyTabButton;
    [SerializeField] internal GameObject depressionTabButton;
    [SerializeField] internal GameObject burnoutTabButton;

    [SerializeField] private GameObject stressTab;
    [SerializeField] private GameObject anxietyTab;
    [SerializeField] private GameObject depressionTab;
    [SerializeField] private GameObject burnoutTab;

    [SerializeField] private GameObject stressUpgradeButton;
    [SerializeField] private GameObject stressUpgradeTab;
    [SerializeField] private GameObject stressGeneratorsButton;
    [SerializeField] private GameObject stressGeneratorsTab;


    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject optionsMenuCloseButton;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private GameObject ClickAreaPanel;

    internal float tickTimer = 0f;

    internal bool stressGeneratorUnlocked = false;

    private void Start()
    {
        stressTab.SetActive(true);
        anxietyTab.SetActive(false);
        depressionTab.SetActive(false);
        burnoutTab.SetActive(false);
        stressGeneratorsButton.SetActive(false);
        stressGeneratorsTab.SetActive(false);
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer >= 1f)
        {
            tickTimer = 0f;
            CheckForAnxietyTabUnlock();

            CheckForStressGeneratorsUnlock();
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
        if (stressManager.stressCount >= 1000)
        {
            anxietyTabButton.SetActive(true);
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
}
