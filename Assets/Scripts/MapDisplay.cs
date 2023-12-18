﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text mapName;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockImage;


    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapImage.sprite = map.mapImage;


        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= map.mapIndex;
        lockImage.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;
        if (mapUnlocked)
            mapImage.color = Color.white;
        else
            mapImage.color = Color.grey;

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(map.sceneLoad.name));
    }
}
