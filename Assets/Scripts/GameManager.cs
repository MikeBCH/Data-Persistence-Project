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


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (DataManager.dataLoaded)
        {
            if (DataManager.playerNames[0] != null)
            {
                highScoreOne.text = "#1 " + DataManager.playerNames[0] + " " + DataManager.playerScores[0];
            }
            if (DataManager.playerNames[1] != null)
            {
                highScoreTwo.text = "#2 " + DataManager.playerNames[1] + " " + DataManager.playerScores[1];
            }
            if (DataManager.playerNames[2] != null)
            {
                highScoreThree.text = "#3 " + DataManager.playerNames[2] + " " + DataManager.playerScores[2];
            }
        }
    }

    public void StartGame()
    {
        if (nameInput.text != "")
        {
            Debug.Log("Input text is " + nameInput.text);
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
            Application.Quit;
#endif
    }
}
