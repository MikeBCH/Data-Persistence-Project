using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public TMP_InputField nameInput;

    public TextMeshProUGUI highScoreOne;
    public TextMeshProUGUI highScoreTwo;
    public TextMeshProUGUI highScoreThree;

    void Start()
    {
        DataManager.instance.Load();
        UpdateScores();

    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    void UpdateScores()
    {
        if (DataManager.dataLoaded)
        {
            if (DataManager.playerNames != null)
            {
                for (int i = 0; i < DataManager.playerNames.Count; i++)     //not the best way to implement but having outofbounds issues with the old null checking
                {

                    //if (DataManager.playerNames[0] != null)
                    if(i == 0) 
                    {
                        highScoreOne.text = "#1: " + DataManager.playerNames[0] + " : " + DataManager.playerScores[0];
                    }
                    //if (!string.IsNullOrEmpty(DataManager.playerNames[1]) && DataManager.playerNames[1] != null)
                    if(i == 1)
                    {
                        highScoreTwo.text = "#2: " + DataManager.playerNames[1] + " : " + DataManager.playerScores[1];
                    }
                    //if (DataManager.playerNames[2] != null)
                    if(i == 2)
                    {
                        highScoreThree.text = "#3: " + DataManager.playerNames[2] + " : " + DataManager.playerScores[2];
                    }
                }
            }
            else        //redundant, can be cleaned up alter
            {
                highScoreOne.text = "#1:";
                highScoreTwo.text = "#2:";
                highScoreThree.text = "#3:";
            }
        }
        else
        {
            highScoreOne.text = "#1:";
            highScoreTwo.text = "#2:";
            highScoreThree.text = "#3:";
        }
    }

    public void StartGame()
    {
        if (nameInput.text != "")
        {
            DataManager.currentPlayerName = nameInput.text;
            SceneManager.LoadScene("main");
        }
    }

    public void ExitGame()
    {
        DataManager.instance.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            //Application.Quit;
            Application.Quit();
#endif
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }
}
