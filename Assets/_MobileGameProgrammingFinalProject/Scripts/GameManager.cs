using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StressManager stressManager;
    private IdleManager idleManager;
    private GeneratorManager generatorManager;
    private UIManager uiManager;

    [SerializeField] private TMP_Text generatedResourcesText; 
    [SerializeField] private GameObject generatedResourcesPanel; 

    internal float saveTimer = 0f;
    private const string LastSaveTimeKey = "LastSaveTime";

    private void Awake()
    {
        stressManager = FindObjectOfType<StressManager>();
        idleManager = FindObjectOfType<IdleManager>();
        generatorManager = FindObjectOfType<GeneratorManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnApplicationQuit()
    {
        Save();
        SaveCurrentTime();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
            SaveCurrentTime();
        }
    }

    private void Start()
    {
        Load();
        LoadAndCalculateResources();
        saveTimer = 0f;
    }

    private void Update()
    {
        saveTimer += Time.deltaTime;
        if (saveTimer > 0.1f)
        {
            saveTimer = 0f;
            Save();
        }

    }

    public void Load()
    {
        // Stress Manager Variables
        stressManager.stressCount = double.Parse(PlayerPrefs.GetString("stressCount", "0"));
        stressManager.stressUpgradeCost = double.Parse(PlayerPrefs.GetString("stressUpgradeCost", "10"));
        stressManager.stressUpgradeAmount = double.Parse(PlayerPrefs.GetString("stressUpgradeAmount", "0.1"));
        stressManager.stressMultiplierAmount = double.Parse(PlayerPrefs.GetString("stressMultiplierAmount", "1"));
        stressManager.stressMultiplierCost = double.Parse(PlayerPrefs.GetString("stressMultiplierCost", "100"));
        idleManager.AnxietyUnlock();

        // Idle Manager Variables
        // Anxiety Resource
        idleManager.anxietyCount = double.Parse(PlayerPrefs.GetString("anxietyCount", "0"));
        idleManager.anxietyUpgradeCost = double.Parse(PlayerPrefs.GetString("anxietyUpgradeCost", "100"));
        idleManager.anxietyUpgradeAmount = double.Parse(PlayerPrefs.GetString("anxietyUpgradeAmount", "0.1")); ;
        idleManager.anxietyMultiplierAmount = double.Parse(PlayerPrefs.GetString("anxietyMultiplierAmount", "1"));
        idleManager.anxietyMultiplierCost = double.Parse(PlayerPrefs.GetString("anxietyMultiplierCost", "1500"));
        idleManager.anxietyResourceEnabled = PlayerPrefs.GetInt("anxietyResourceEnabled", 0) == 1;
        idleManager.DepressionUnlock();
        // Depression Resource
        idleManager.depressionCount = double.Parse(PlayerPrefs.GetString("depressionCount", "0"));
        idleManager.depressionUpgradeCost = double.Parse(PlayerPrefs.GetString("depressionUpgradeCost", "10000"));
        idleManager.depressionUpgradeAmount = double.Parse(PlayerPrefs.GetString("depressionUpgradeAmount", "1"));
        idleManager.depressionMultiplierAmount = double.Parse(PlayerPrefs.GetString("depressionMultiplierAmount", "1"));
        idleManager.depressionMultiplierCost = double.Parse(PlayerPrefs.GetString("depressionMultiplierCost", "100000"));
        idleManager.depressionResourceEnabled = PlayerPrefs.GetInt("depressionResourceEnabled", 0) == 1;

        // Generator Manager Variables
        // Level One Stress Generator Variables
        generatorManager.stressGeneratorLevelOneAmount = double.Parse(PlayerPrefs.GetString("stressGeneratorLevelOneAmount", "0"));
        generatorManager.stressGeneratorLevelOneCost = double.Parse(PlayerPrefs.GetString("stressGeneratorLevelOneCost", "1000"));

        // Level Two Stress Generator Variables
        generatorManager.stressGeneratorLevelTwoAmount = double.Parse(PlayerPrefs.GetString("stressGeneratorLevelTwoAmount", "0"));
        generatorManager.stressGeneratorLevelTwoCost = double.Parse(PlayerPrefs.GetString("stressGeneratorLevelTwoCost", "10000"));

        // UI Manager Variables
        uiManager.anxietyTab.SetActive(PlayerPrefs.GetInt("anxietyTab", 0) == 1);
        uiManager.depressionTab.SetActive(PlayerPrefs.GetInt("depressionTab", 0) == 1);
        uiManager.burnoutTab.SetActive(PlayerPrefs.GetInt("burnoutTab", 0) == 1);
        uiManager.stressGeneratorsButton.SetActive(PlayerPrefs.GetInt("stressGeneratorsButton", 0) == 1);
        uiManager.stressGeneratorsTab.SetActive(PlayerPrefs.GetInt("stressGeneratorsTab", 0) == 1);
        uiManager.stressGeneratorUnlocked = PlayerPrefs.GetInt("stressGeneratorUnlocked", 0) == 1;
    }

    public void Save()
    {
        // Stress Manager Variables
        PlayerPrefs.SetString("stressCount", stressManager.stressCount.ToString());
        PlayerPrefs.SetString("stressUpgradeCost", stressManager.stressUpgradeCost.ToString());
        PlayerPrefs.SetString("stressUpgradeAmount", stressManager.stressUpgradeAmount.ToString());
        PlayerPrefs.SetString("stressMultiplierAmount", stressManager.stressMultiplierAmount.ToString());
        PlayerPrefs.SetString("stressMultiplierCost", stressManager.stressMultiplierCost.ToString());

        // Idle Manager Variables
        // Anxiety Resource
        PlayerPrefs.SetString("anxietyCount", idleManager.anxietyCount.ToString());
        PlayerPrefs.SetString("anxietyUpgradeCost", idleManager.anxietyUpgradeCost.ToString());
        PlayerPrefs.SetString("anxietyUpgradeAmount", idleManager.anxietyUpgradeAmount.ToString());
        PlayerPrefs.SetString("anxietyMultiplierAmount", idleManager.anxietyMultiplierAmount.ToString());
        PlayerPrefs.SetString("anxietyMultiplierCost", idleManager.anxietyMultiplierCost.ToString());
        PlayerPrefs.SetInt("anxietyResourceEnabled", idleManager.anxietyResourceEnabled ? 1 : 0);
        PlayerPrefs.SetInt("anxietyCountText", idleManager.anxietyCountText ? 1 : 0);

        // Depression Resource
        PlayerPrefs.SetString("depressionCount", idleManager.depressionCount.ToString());
        PlayerPrefs.SetString("depressionUpgradeCost", idleManager.depressionUpgradeCost.ToString());
        PlayerPrefs.SetString("depressionUpgradeAmount", idleManager.depressionUpgradeAmount.ToString());
        PlayerPrefs.SetString("depressionMultiplierAmount", idleManager.depressionMultiplierAmount.ToString());
        PlayerPrefs.SetString("depressionMultiplierCost", idleManager.depressionMultiplierCost.ToString());
        PlayerPrefs.SetInt("depressionResourceEnabled", idleManager.depressionResourceEnabled ? 1 : 0);
        PlayerPrefs.SetInt("depressionCountText", idleManager.depressionCountText ? 1 : 0);

        // Generator Manager Variables
        // Level One Stress Generator Variables
        PlayerPrefs.SetString("stressGeneratorLevelOneAmount", generatorManager.stressGeneratorLevelOneAmount.ToString());
        PlayerPrefs.SetString("stressGeneratorLevelOneCost", generatorManager.stressGeneratorLevelOneCost.ToString());

        // Level Two Stress Generator Variables
        PlayerPrefs.SetString("stressGeneratorLevelTwoAmount", generatorManager.stressGeneratorLevelTwoAmount.ToString());
        PlayerPrefs.SetString("stressGeneratorLevelTwoCost", generatorManager.stressGeneratorLevelTwoCost.ToString());

        // UI Manager Variables
        PlayerPrefs.SetInt("anxietyTab", uiManager.anxietyTab.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("depressionTab", uiManager.depressionTab.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("burnoutTab", uiManager.burnoutTab.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("stressGeneratorsButton", uiManager.stressGeneratorsButton.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("stressGeneratorsTab", uiManager.stressGeneratorsTab.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("stressGeneratorUnlocked", uiManager.stressGeneratorUnlocked ? 1 : 0);
        // Save the game every 0.1 seconds
    }

    private void SaveCurrentTime()
    {
        var currentTime = DateTime.UtcNow;
        PlayerPrefs.SetString(LastSaveTimeKey, currentTime.ToString());
        PlayerPrefs.Save();
    }

    private void LoadAndCalculateResources()
    {
        string lastSaveTimeString = PlayerPrefs.GetString(LastSaveTimeKey, string.Empty);
        string resourcesText = ""; // String to hold the text for generated resources

        if (!string.IsNullOrEmpty(lastSaveTimeString))
        {
            DateTime lastSaveTime = DateTime.Parse(lastSaveTimeString);
            TimeSpan timeElapsed = DateTime.UtcNow - lastSaveTime;

            // Calculate Stress
            double generatedStress = CalculateIdleResource(generatorManager.stressGeneratorLevelOneAmount, timeElapsed);
            if (generatedStress > 0)
            {
                resourcesText += $"Stress: {generatedStress:F2}\n";
                stressManager.stressCount += generatedStress;
            }

            // Calculate Anxiety
            if (idleManager.anxietyResourceEnabled)
            {
                double generatedAnxiety = CalculateIdleResource(idleManager.anxietyUpgradeAmount * idleManager.anxietyMultiplierAmount, timeElapsed);
                if (generatedAnxiety > 0)
                {
                    resourcesText += $"Anxiety: {generatedAnxiety:F2}\n";
                    idleManager.anxietyCount += generatedAnxiety;
                }
            }

            // Calculate Depression
            if (idleManager.depressionResourceEnabled)
            {
                double generatedDepression = CalculateIdleResource(idleManager.depressionUpgradeAmount * idleManager.depressionMultiplierAmount, timeElapsed);
                if (generatedDepression > 0)
                {
                    resourcesText += $"Depression: {generatedDepression:F2}\n";
                    idleManager.depressionCount += generatedDepression;
                }
            }

            // Update and show the text if there are generated resources
            if (!string.IsNullOrEmpty(resourcesText))
            {
                generatedResourcesText.text = resourcesText;
                generatedResourcesPanel.SetActive(true);
            }
            else
            {
                generatedResourcesPanel.SetActive(false);
            }
        }
    }

    public void OKButtonClick()
    {
        generatedResourcesPanel.SetActive(false);
    }

    private double CalculateIdleResource(double generationRate, TimeSpan timeElapsed)
    {
        return generationRate * timeElapsed.TotalSeconds;
    }
}
