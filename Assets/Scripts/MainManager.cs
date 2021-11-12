using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text NameText;

    public TextMeshProUGUI menuButton;
    public TextMeshProUGUI quitButton;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.Load();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        SetScores();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * DataManager.gameDifficulty, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        DataManager.currentScore = m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        DataManager.instance.Save();
    }

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }

    public void Quit()
    {
        DataManager.instance.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    void SetScores()
    {
        if (DataManager.gameDifficulty == 0)
        {
            DataManager.gameDifficulty = 2.0f;      // set default difficulty if none has been set
        }
        string highScorePlayer = "";
        int highScore = 0;
        if (DataManager.playerNames != null)
        {
        //    if (DataManager.playerNames.Count > 0)
          //  {
                if (DataManager.playerNames[0] != null)
                {
                    highScorePlayer = DataManager.playerNames[0];
                    highScore = DataManager.playerScores[0];
                }
        //    }
        }


        NameText.text = "Highest Score: " + highScorePlayer + " : " + highScore;
    }

}
