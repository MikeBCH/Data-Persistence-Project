using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    public Text difficulty;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu() {
        EditorSceneManager.LoadScene("menu");
    }

    //not the best way to set the difficulty but a work around for now as the usual method was running into a number of isses
    public void Easy() {
        DataManager.gameDifficulty = 1.0f;
        difficulty.text = "Difficulty: Easy";
        difficulty.gameObject.SetActive(true);
    }

    public void Normal() {
        DataManager.gameDifficulty = 2.0f;
        difficulty.text = "Difficulty: Normal";
        difficulty.gameObject.SetActive(true);
    }

    public void Hard() {
        DataManager.gameDifficulty = 4.0f;
        difficulty.text = "Difficulty: Hard";
        difficulty.gameObject.SetActive(true);
    }

  /*  public void SetDifficulty() {
        if(gameObject.CompareTag("easy")) {
            DataManager.gameDifficulty = 1.0f;
        }
        if(gameObject.CompareTag("normal")) {
            DataManager.gameDifficulty = 2.0f;
        }
        if(gameObject.CompareTag("hard")) {
            DataManager.gameDifficulty = 4.0f;
        }
    }*/

        public void DeleteScore()
    {
          File.Delete(Application.persistentDataPath + "/saveFile1.json");
          DataManager.playerNames = null;
          DataManager.playerScores = null;
          DataManager.rank = null;
        difficulty.text = "Scores Reset";
        difficulty.gameObject.SetActive(true);
    }
}
