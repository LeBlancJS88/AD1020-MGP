using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class StressManager : MonoBehaviour
{

    // Display Text

    // Stress Text
    [SerializeField] internal TMP_Text stressText;
    [SerializeField] public double stressCount;

    // Stress Upgrade Button
    [SerializeField] internal TMP_Text stressUpgradeText;
    [SerializeField] internal double stressUpgradeCost;
    internal double stressUpgradeAmount;


    // Stress Multiplier Button
    [SerializeField] internal TMP_Text stressMultiplierText;
    [SerializeField] internal double stressMultiplierCost;
    internal double stressMultiplierAmount;


    public void Start()
    {
        //stressCount = 0;
        //stressUpgradeCost = 10;
        //stressUpgradeAmount = 0.1;
        //stressMultiplierAmount = 1;
        //stressMultiplierCost = 100;
    }

    public void Update()
    {
        stressUpgradeText.text = "Increase Stress Load.\n Cost: " + stressUpgradeCost.ToString("F2") + "\n" + math.round(100* stressUpgradeAmount) / 100;
        stressMultiplierText.text = "Stress Multiplier:\n Current: " + stressMultiplierAmount + "x" + "\n" + "Cost: " + stressMultiplierCost.ToString("F0");
        stressText.text = "Stress: " + stressCount.ToString("F2");
    }

    public void StressClick()
    {
        stressCount += stressUpgradeAmount * stressMultiplierAmount;
    }

    public void StressUpgradeClick()
    {
        if (stressCount >= stressUpgradeCost && (stressCount - stressUpgradeCost) >= 0)
        {
        stressCount -= stressUpgradeCost;
        stressUpgradeAmount += 0.1;
        stressUpgradeCost *= 1.5;
        }
    }

    public void StressMultiplierClick()
    {
        if (stressCount >= stressMultiplierCost && (stressCount - stressMultiplierCost) >= 0)
        {
            stressCount -= stressMultiplierCost;
            stressMultiplierAmount += 1;
            stressMultiplierCost *= 2;
        }
    }
}
