using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public static GameOverManager instance;

    private GameObject gameOverPanel;
    private Animator gameOverAnim;

    private Button playAgainBtn, backBtn;

    private GameObject scoreText;
    private Text finalScore;
    
    void Awake()
    {
        MakeInstance();
        InitializeVariables();
    }

    void MakeInstance()
    {
        if (GameOverManager.instance == null)
            GameOverManager.instance = this;
    }

    public void GameOverShowPanel()
    {
        scoreText.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverAnim.Play("Fade In");

        finalScore.text = "Score:\n" + ScoreManager.instance.GetScore();
    }

    void InitializeVariables()
    {
        gameOverPanel = GameObject.Find("Game Over Panel Holder");
        gameOverAnim = gameOverPanel.GetComponent<Animator>();
        scoreText = GameObject.Find("Score Text");

        playAgainBtn = GameObject.Find("Play Again Button").GetComponent<Button>();
        backBtn = GameObject.Find("Back Button").GetComponent<Button>();

        playAgainBtn.onClick.AddListener(() => PlayAgain());
        backBtn.onClick.AddListener(() => BackToMenu());

        finalScore = GameObject.Find("Final Score").GetComponent<Text>();

        gameOverPanel.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
