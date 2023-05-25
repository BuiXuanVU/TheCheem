using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject over;
    [SerializeField] private GameObject start;
    [SerializeField] private int score;
    [SerializeField] public bool isPlay = true;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    private void Start()
    {
        Time.timeScale = 0;
    }
    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }
    public void GetScore(int currentScore)
    {
        score = currentScore;
    }    
    public void GameOver()
    {
        over.SetActive(true);
        textScore.text = "Score: " + score.ToString();
        if(score > GetBestScore())
        {
            SaveScore();
        }    
        bestScore.text = "Best: " + GetBestScore().ToString();
        Time.timeScale = 0f;
    }   
    public void StartGame()
    {
        start.SetActive(false);
        Time.timeScale = 1;
    }    
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }    
    private void SaveScore()
    {
        PlayerPrefs.SetInt("bestScore", score);
    }    
    private int GetBestScore()
    {
        return PlayerPrefs.GetInt("bestScore");
    }    
}
