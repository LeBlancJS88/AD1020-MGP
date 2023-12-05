using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private StressManager stressManager;
    [SerializeField] private UIManager uiManager;

    // Stress Generator Level One Button
    [SerializeField] private GameObject stressGeneratorLevelOneButton;
    [SerializeField] private TMP_Text stressGeneratorLevelOneButtonText;

    // Stress Generator Level Two Button
    [SerializeField] private GameObject stressGeneratorLevelTwoButton;
    [SerializeField] private TMP_Text stressGeneratorLevelTwoButtonText;

    // Level One Stress Generator Variables
    private double stressGeneratorLevelOneAmount = 0;
    private double stressGeneratorLevelOneCost = 1000;

    // Level Two Stress Generator Variables
    private double stressGeneratorLevelTwoAmount = 0;
    private double stressGeneratorLevelTwoCost = 10000;


    void Update()
    {

        if (uiManager.stressGeneratorUnlocked)
        {
            StressGeneration();
        }

        stressGeneratorLevelOneButtonText.text = "Stress Generator Level One\n" + stressGeneratorLevelOneAmount.ToString("F2") + "\n" + stressGeneratorLevelOneCost.ToString("F0");
        stressGeneratorLevelTwoButtonText.text = "Stress Generator Level Two\n" + stressGeneratorLevelTwoAmount.ToString("F2") + "\n" + stressGeneratorLevelTwoCost.ToString("F0");
    }

    public void StressGeneratorLevelOneClick()
    {

        if (uiManager.stressGeneratorUnlocked && stressManager.stressCount >= 1000)
        {
            stressManager.stressCount -= stressGeneratorLevelOneCost;
            stressGeneratorLevelOneAmount += 1;
            stressGeneratorLevelOneCost *= 1.5;
            stressGeneratorLevelOneButtonText.text = "Stress Generator Level One\n" + stressGeneratorLevelOneAmount.ToString("F2") + "\n" + stressGeneratorLevelOneCost.ToString("F0");
        }
    }

    public void StressGeneratorLevelTwoClick()
    {

        if (uiManager.stressGeneratorUnlocked && stressManager.stressCount >= 10000)
        {
            stressManager.stressCount -= stressGeneratorLevelTwoCost;
            stressGeneratorLevelTwoAmount += 1;
            stressGeneratorLevelTwoCost *= 1.5;
            stressGeneratorLevelTwoButtonText.text = "Stress Generator Level Two\n" + stressGeneratorLevelTwoAmount.ToString("F2") + "\n" + stressGeneratorLevelTwoCost.ToString("F0");
        }
    }

    private void StressGeneration()
    {

            stressManager.stressCount += stressGeneratorLevelOneAmount * Time.deltaTime;

        if (stressGeneratorLevelTwoAmount >= 1)
        {
            stressGeneratorLevelOneAmount += stressGeneratorLevelTwoAmount * Time.deltaTime;
        }
    }
}
