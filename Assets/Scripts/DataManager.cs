using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;
    public static string currentPlayerName;
    public static int currentScore;
    public static int highScore;
    public static string playerNameHighScore;

    public static string highScorePlayerName;
    public static int highScorePlayerScore = 0;

    public static List<string> playerNames;
    public static List<int> playerScores;
    public static List<int> rank;
    public static float gameDifficulty;

    public static bool dataLoaded = false;

    [System.Serializable]
    class PlayerScore
    {
        public List<SaveData> players = new List<SaveData>();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = new DataManager();
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int rank;
        public string playerName;
        public int playerScore;
    }

    public void Save()
    {
        int newRank = -1;
        if (playerNames == null)
        {       //should only be the case if there are no high scores
            playerNames = new List<string>();
        }
        if (playerScores == null)
        {      //should only be the case if there are no high scores
            playerScores = new List<int>();
        }
        if (rank == null)
        {      //should only be the case if there are no high scores
            rank = new List<int>();
            newRank = 0;        //if we need to initialise it means there are no scores
        }

        for (int i = playerScores.Count - 1; i >= 0; i--)
        {
            if (currentScore > playerScores[i])
            {
                newRank = i;
            }

        }


        // if a new rank is more than -1, its default value, then there must be a score to add
        if (newRank > -1)
        {
            playerNames.Insert(newRank, DataManager.currentPlayerName);
            playerScores.Insert(newRank, currentScore);
            rank.Insert(newRank, newRank);
            //TODO - reorder i.e. if the new score gets inserted into position 2 then the old 2 should become 3 etc

            // create a new scores object that is a list of SaveData objects, the create the SaveData objects containing the top 3 scores and add them to
            // the PlayerScores list to then be converted to JSON and saved
            PlayerScore scores = new PlayerScore();
            int listCountOrThreeMax;
            if (playerNames.Count > 3)
            {
                listCountOrThreeMax = 3;
            }
            else
            {
                listCountOrThreeMax = playerNames.Count;
            }
            for (int i = 0; i < listCountOrThreeMax; i++)     //set to less than 3 as we only want to save the top 3 and nothing else
            {
                //  foreach (string name in playerNames) {
                SaveData data = new SaveData();
                data.playerName = playerNames[i];
                //foreach (int score in playerScores) {
                data.playerScore = playerScores[i];
                // }
                //foreach (int rank in rank) {
                data.rank = rank[i];
                //  }
                scores.players.Add(data);
                // }     
            }
            string json = JsonUtility.ToJson(scores);
            File.WriteAllText(Application.persistentDataPath + "/saveFile1.json", json);
        }

    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/saveFile1.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerScore scores = JsonUtility.FromJson<PlayerScore>(json);
            playerNames = new List<string>();
            playerScores = new List<int>();
            rank = new List<int>();
            //  SaveData data = JsonUtility.FromJson<SaveData>(json);
            for (int i = 0; i < scores.players.Count; i++)
            {
                playerNames.Add(scores.players[i].playerName);
                playerScores.Add(scores.players[i].playerScore);
                rank.Add(scores.players[i].rank);
                if (scores.players[i].playerScore > highScorePlayerScore)
                {
                    highScorePlayerScore = scores.players[i].playerScore;
                    highScorePlayerName = scores.players[i].playerName;
                }
            }

            dataLoaded = true;
        }
    }

    public void DeleteScore()
    {

        File.Delete(Application.persistentDataPath + "/saveFile1.json");
    }

    public void SetDifficulty(float difficulty)
    {
        gameDifficulty = difficulty;
    }

}
