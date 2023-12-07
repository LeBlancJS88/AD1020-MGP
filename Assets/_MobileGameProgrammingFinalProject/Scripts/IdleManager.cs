using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdleManager : MonoBehaviour
{

    [SerializeField] private StressManager stressManager;
    [SerializeField] private UIManager uiManager;
    private GameManager gameManager;

    #region Anxiety Resource
    [SerializeField] private TMP_Text anxietyCountText;
    [SerializeField] private TMP_Text anxietyUpgradeText;
    [SerializeField] private TMP_Text anxietyMultiplierText;
    internal double anxietyCount;

    internal double anxietyUpgradeCost;
    internal double anxietyMultiplier;
    internal double anxietyMultiplierCost;
    internal double anxietyUpgradeAmount;
    internal double anxietyMultiplierAmount;
    #endregion

    #region Depression Resource
    [SerializeField] private TMP_Text depressionCountText;
    [SerializeField] private TMP_Text depressionUpgradeText;
    [SerializeField] private TMP_Text depressionMultiplierText;
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
    }
    private void Start()
    {

        //anxietyUpgradeCost = 100;
        //anxietyUpgradeAmount = 0.1;
        //anxietyMultiplierAmount = 1;
        //anxietyMultiplierCost = 1500;
        //anxietyResourceEnabled = false;

        //depressionUpgradeCost = 10000;
        //depressionUpgradeAmount = 1;
        //depressionMultiplierAmount = 1;
        //depressionMultiplierCost = 100000;
        //depressionResourceEnabled = false;

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

    private void AnxietyUnlock()
    {
        if (stressManager.stressCount >= 10)
        {
            uiManager.anxietyTabButton.SetActive(true);
            anxietyResourceEnabled = true;
            anxietyCountText.enabled = true;
        }
    }

    private void DepressionUnlock()
    {
        if (anxietyCount >= 10)
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
        if (stressManager.stressCount >= anxietyUpgradeCost)
        {
            stressManager.stressCount -= anxietyUpgradeCost;
            anxietyUpgradeAmount *= 1.25;
            anxietyUpgradeCost *= 1.5;
            anxietyUpgradeText.text = "Overthink: \n" + "Cost: " + anxietyUpgradeCost.ToString("F2") + "\n" + anxietyUpgradeAmount.ToString("F2");
        }

    }

    public void AnxietyMultiplierClick()
    {
        if (anxietyCount >= anxietyMultiplierCost)
        {
            anxietyCount -= anxietyMultiplierCost;
            anxietyMultiplierAmount += 1;
            anxietyMultiplierCost *= 2;
            anxietyMultiplierText.text = "Stress Multiplier:\n " + "Current: " + anxietyMultiplierAmount + "x" + "\n" + "Cost: " + anxietyMultiplierCost.ToString("F0");
        }
    }
    #endregion

    #region Depression Upgrades

    public void DepressionUpgradeClick()
    {
        if (anxietyCount >= depressionUpgradeCost)
        {
            anxietyCount -= depressionUpgradeCost;
            depressionUpgradeAmount *= 1.25;
            depressionUpgradeCost *= 1.5;
            depressionUpgradeText.text = "Wallow: \n" + "Cost: " + depressionUpgradeCost.ToString("F2") + "\n" + depressionUpgradeAmount.ToString("F2");
        }
    }

    public void DepressionMultiplierClick()
    {
        if (depressionCount >= depressionMultiplierCost)
        {
            depressionCount -= depressionMultiplierCost;
            depressionMultiplierAmount += 1;
            depressionMultiplierCost *= 2;
            depressionMultiplierText.text = "Self-sabotage:\n " + "Current: " + depressionMultiplierAmount + "x" + "\n" + "Cost: " + depressionMultiplierCost.ToString("F0");
        }
    }
    #endregion
}
