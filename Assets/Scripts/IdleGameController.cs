using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdleGameController : MonoBehaviour
{
    [SerializeField] private TMP_Text depressionScoreText;
    [SerializeField] private Button multiplierButton;

    private float depressionScore = 0.0f;
    private float scoreMultiplier = 1.0f;
    private float updateInterval = 1.0f; // Update the depression score every second
    private float timeSinceLastUpdate = 0.0f;

    private void Start()
    {
        multiplierButton.onClick.AddListener(OnMultiplierButtonClick);
    }

    private void Update()
    {
        // Update the time since the last score update
        timeSinceLastUpdate += Time.deltaTime;

        // Check if it's time to update the depression score
        if (timeSinceLastUpdate >= updateInterval)
        {
            UpdateDepressionScore();
            timeSinceLastUpdate = 0.0f; // Reset the time
        }

        // Update the UI text
        depressionScoreText.text = "You have " + depressionScore.ToString("F0") + " depression";
    }

    private void UpdateDepressionScore()
    {
        depressionScore += 1.0f * scoreMultiplier; // Increment the score based on the multiplier
    }

    private void OnMultiplierButtonClick()
    {
        // When the button is clicked, double the score update rate
        scoreMultiplier *= 2.0f;
    }
}
