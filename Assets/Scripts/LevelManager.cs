using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private GameObject[] shopItemsToDelete;
    [SerializeField] private GameObject[] shopItemsToShow;

    void Start()
    {
        int levelToUnlock = PlayerPrefs.GetInt("LevelToUnlock");

        for (int i = 0; i <= levelToUnlock; i++)
        {
            if (levelButtons[i].transform.childCount > 1)
            {
                Destroy(levelButtons[i].transform.GetChild(1).gameObject);
                levelButtons[i].interactable = true;
                levelButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if(levelToUnlock >= 10)
        {
            shopItemsToShow[0].SetActive(true);
            Destroy(shopItemsToDelete[0]);

            shopItemsToShow[1].SetActive(true);
            Destroy(shopItemsToDelete[1]);
        }
    }
}
