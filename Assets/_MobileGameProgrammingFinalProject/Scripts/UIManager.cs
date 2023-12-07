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

    internal float tickTimer = 0f;

    internal bool stressGeneratorUnlocked = false;

    private void Start()
    {
        stressTabButton.SetActive(true);
        stressTab.SetActive(true);
        stressUpgradeTab.SetActive(true);
        //anxietyTab.SetActive(false);
        //depressionTab.SetActive(false);
        //burnoutTab.SetActive(false);
        //stressGeneratorsButton.SetActive(false);
        //stressGeneratorsTab.SetActive(false);
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
