﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public void Start()
    {
        GameManager instance = GameManager.instance;
        Button LevelZero = GameObject.Find("Level 0 Button").GetComponent<Button>();
        Button LevelOne = GameObject.Find("Level 1 Button").GetComponent<Button>();
        Button LevelTwo = GameObject.Find("Level 2 Button").GetComponent<Button>();
        Button LevelThree = GameObject.Find("Level 3 Button").GetComponent<Button>();

        instance.health = 0;
        if (GameManager.instance.levelZeroUnlocked)
            LevelZero.interactable = true;
        if (GameManager.instance.levelOneUnlocked)
            LevelOne.interactable = true;
        if (GameManager.instance.levelTwoUnlocked)
            LevelTwo.interactable = true;
        if (GameManager.instance.levelThreeUnlocked)
            LevelThree.interactable = true;
    }

    public void Intro()
    {
        SceneManager.LoadScene("Level 0 - 1");
    }

	public void LoadLevelOne()
    {
        SceneManager.LoadScene("Cutscene 1 - 1");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Cutscene 2 - 1");
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level 3 - 1");
    }
}
