using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Add this line for access to Unity UI elements

public class DailyRewards : MonoBehaviour
{
    [SerializeField] private CurrencyController currencyController;
    [SerializeField] private Button[] takeRewardButtons; // Reference to your reward buttons
    [SerializeField] private Image currentDayBar;
    [SerializeField] private Text currentDayText;

    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }

    private int currentDay = 1;
    private int maxStreakCount = 7;
    private float claimCooldown = 24f / 24 / 60 / 6 / 2;
    private float claimDeadline = 48f / 24 / 60 / 6 / 2;
    private bool canClaimReward;

    private bool buttonWasInteractable;

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");

        }
    }

    private void Start()
    {
        StartCoroutine(RewardsStateUpdater());
    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            UpdateButtonInteractivity();
            UpdateBarFillAmount();
            UpdateCurrentDayText();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardsState()
    {
        canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;
            if (timeSpan.TotalHours > claimDeadline)
            {
                lastClaimTime = null;
                currentStreak = 0;
                currentDay = 1;
            }
            else if (timeSpan.TotalHours < claimCooldown)
            {
                canClaimReward = false;
            }
        }
    }

    private void UpdateButtonInteractivity()
    {
        for (int i = 0; i < takeRewardButtons.Length; i++)
        {
            buttonWasInteractable = takeRewardButtons[i].interactable;

            takeRewardButtons[i].interactable = i == currentStreak && canClaimReward;

            if (takeRewardButtons[i].interactable && !buttonWasInteractable && currentStreak > 0)
            {
                currentDay++;
            }
            
        }
    }

    private void UpdateBarFillAmount()
    {
        float fillValue = (float)currentDay / maxStreakCount;
        currentDayBar.fillAmount = fillValue;
    }

    private void UpdateCurrentDayText()
    {
        currentDayText.text = $"{currentDay}/{maxStreakCount}";
    }

    public void ClaimReward(int count)
    {
        if (!canClaimReward)
            return;

        currencyController.AddCurrency(count);

        lastClaimTime = DateTime.UtcNow;

        currentStreak = (currentStreak + 1) % maxStreakCount;

        if (currentDay >= maxStreakCount)
        {
            currentDay = 1;
            lastClaimTime = null;
            currentStreak = 0;
        }

        UpdateRewardsState();
        UpdateBarFillAmount();
        UpdateCurrentDayText();
    }
}
