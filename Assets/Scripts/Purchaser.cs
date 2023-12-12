using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using Product = UnityEngine.Purchasing.Product;

public class Purchaser : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
           
            case "com.serbull.iaptutorial.500coins":
                Add500Coins();
                break;
        }
    }

    
    

    private void Add500Coins()
    {
        int currencyCount = PlayerPrefs.GetInt("CurrencyCount");
        currencyCount += 500;
        PlayerPrefs.SetInt("CurrencyCount", currencyCount);
        Debug.Log("Purchase: get 500 coins");
        CurrencyController.Instanse.UpdateCurrencyCount(currencyCount);
    }
}