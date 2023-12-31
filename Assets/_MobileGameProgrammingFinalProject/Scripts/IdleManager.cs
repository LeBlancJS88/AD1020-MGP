using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdleManager : MonoBehaviour
{

    [SerializeField] private StressManager stressManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;

    #region Anxiety Resource
    [SerializeField] internal TMP_Text anxietyCountText;
    [SerializeField] internal TMP_Text anxietyUpgradeText;
    [SerializeField] internal TMP_Text anxietyMultiplierText;
    internal double anxietyCount;

    internal double anxietyUpgradeLevel;
    internal double anxietyUpgradeCost;
    internal double anxietyUpgradeAmount;
    internal double anxietyMultiplier;
    internal double anxietyMultiplierCost;
    internal double anxietyMultiplierAmount;
    #endregion

    #region Depression Resource
    [SerializeField] internal TMP_Text depressionCountText;
    [SerializeField] internal TMP_Text depressionUpgradeText;
    [SerializeField] internal TMP_Text depressionMultiplierText;
    internal double depressionCount;

    internal double depressionUpgradeCost;
    internal double depressionMultiplier;
    internal double depressionMultiplierCost;
    internal double depressionUpgradeAmount;
    internal double depressionMultiplierAmount;

    #endregion

    private float checkTimer = 0.1f;
    internal bool anxietyResourceEnabled;
    internal bool depressionResourceEnabled;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log(gameManager == null ? "GameManager not found" : "GameManager found");

        if (anxietyCountText != null)
        {
            anxietyCountText.enabled = false;
        }

        if (depressionCountText != null)
        {
            depressionCountText.enabled = false;
        }

        if (stressManager == null || uiManager == null)
        {
            Debug.LogError("Dependencies are not set");
            return;
        }
    }
    private void Start()
    {   
        StartCoroutine(IdleResource());
    }

    private void Update()
    {
        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0)
        {
            if (!anxietyResourceEnabled)
            {
            AnxietyUnlock();
            }
            if (!depressionResourceEnabled)
            {
            DepressionUnlock();
            }
            checkTimer = 0.1f;
        }

        if (anxietyResourceEnabled)
        {
            anxietyCountText.text = "Anxiety: " + anxietyCount.ToString("F2");
        }

        if (depressionResourceEnabled)
        {
            depressionCountText.text = "Depression: " + depressionCount.ToString("F2");
        }
    }

    internal void AnxietyUnlock()
    {
        if (stressManager.stressCount >= 10000)
        {
            uiManager.anxietyTabButton.SetActive(true);
            anxietyResourceEnabled = true;
            anxietyCountText.enabled = true;
        }
    }

    internal void DepressionUnlock()
    {
        if (anxietyCount >= 100000)
        {
            uiManager.depressionTabButton.SetActive(true);
            depressionResourceEnabled = true;
            depressionCountText.enabled = true;
        }
    }

    private IEnumerator IdleResource()
    {
        while (true)
        {
            if (anxietyResourceEnabled)
            {
                anxietyCount += (anxietyUpgradeAmount * anxietyMultiplierAmount) * Time.deltaTime;
            }

            if (anxietyCountText != null && anxietyCount >= 10)
            {
                depressionCount += (depressionUpgradeAmount * depressionMultiplierAmount) * Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    #region Anxiety Upgrades
    public void AnxietyUpgradeClick()
    {
        if (stressManager.stressCount >= anxietyUpgradeCost && (stressManager.stressCount - anxietyUpgradeCost) >= 0)
        {
            stressManager.stressCount -= anxietyUpgradeCost;
            anxietyUpgradeAmount += 0.10;
            anxietyUpgradeCost *= 1.5;
            anxietyUpgradeText.text = "Start Overthinking \n" + " + " + anxietyUpgradeLevel.ToString("F2") + " Anxiety / Second"+ "\n" + "Current: " + anxietyUpgradeAmount.ToString("F2") + " / Second" + "\n Cost: " + anxietyUpgradeCost.ToString("F2") + " Stress";
        }

    }

    public void AnxietyMultiplierClick()
    {
        if (anxietyCount >= anxietyMultiplierCost && (anxietyCount - anxietyMultiplierCost) >= 0)
        {
            anxietyCount -= anxietyMultiplierCost;
            anxietyMultiplierAmount += 1;
            anxietyMultiplierCost *= 2;
            anxietyMultiplierText.text = "Stress Multiplier\n " + "Current: " + anxietyMultiplierAmount + "x" + "\n" + "Cost: " + anxietyMultiplierCost.ToString("F0");
        }
    }
    #endregion

    #region Depression Upgrades

    public void DepressionUpgradeClick()
    {
        if (anxietyCount >= depressionUpgradeCost && (anxietyCount - depressionUpgradeCost) >= 0)
        {
            anxietyCount -= depressionUpgradeCost;
            depressionUpgradeAmount *= 1.25;
            depressionUpgradeCost *= 1.5;
            depressionUpgradeText.text = "Begin Wallowing \n" + "+ 0.01 Depression / Second" + "\n" + "Current: " + "\n" + depressionUpgradeAmount.ToString("F2") + " / Second" + "\n Cost: " + depressionUpgradeCost.ToString("F2") ;
        }
    }

    public void DepressionMultiplierClick()
    {
        if (depressionCount >= depressionMultiplierCost && (depressionCount - depressionMultiplierCost) >= 0)
        {
            depressionCount -= depressionMultiplierCost;
            depressionMultiplierAmount += 1;
            depressionMultiplierCost *= 2;
            depressionMultiplierText.text = "Begin Self Sabotaging \n" + "Depression Multiplier + 1 \n"+ "Current: " + depressionMultiplierAmount + "x" + "\n" + "Cost: " + depressionMultiplierCost.ToString("F0");
        }
    }
    #endregion
}
