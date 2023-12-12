using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController Instanse;

    [SerializeField] private Text currencyCountText;

    private void Start()
    {
        Instanse = this;

        int currencyCount = PlayerPrefs.GetInt("CurrencyCount");

        UpdateCurrencyCount(currencyCount);
    }

    public void AddCurrency(int amount)
    {
        int currencyCount = PlayerPrefs.GetInt("CurrencyCount");
        currencyCount += amount;

        PlayerPrefs.SetInt("CurrencyCount", currencyCount);

        UpdateCurrencyCount(currencyCount);
    }

    public void SpendCurrency(int amount)
    {
        int currencyCount = PlayerPrefs.GetInt("CurrencyCount");

        if (currencyCount >= amount)
        {
            currencyCount -= amount;

            PlayerPrefs.SetInt("CurrencyCount", currencyCount);

            UpdateCurrencyCount(currencyCount);
        }
    }

    public void UpdateCurrencyCount(int amount)
    {
        currencyCountText.text = amount.ToString();
    }
}
