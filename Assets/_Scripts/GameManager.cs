using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameStates { loading, inGame, gameOver }
public class GameManager : MonoBehaviour
{
    private const string MAX_SCORE = "MAX SCORE";

    public ParticleSystem particlePoint;
    private AudioSource _boomSound;
    private bool hasPlayingAudio;


    //Puntuacion
    public TextMeshProUGUI scoreText;
    private int _score;
    public  int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = Mathf.Clamp(value, 0, 99999);
        }
    }
    private int targetScore = 100;

    //Estados de juego
    public CanvasGroup imageGameOver;
    private float fadeDuration = 1;
    private float time;
    public TextMeshProUGUI scoreNowText;
    public TextMeshProUGUI maxScoreText;
    public Button restartButton;
    public Button quitButton;
    public GameStates states;

    //public GameStates states;

    [HideInInspector]
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        states = GameStates.inGame;
        _boomSound = GetComponent<AudioSource>();
        gameOver = false;

    }

    

    // Update is called once per frame
    void Update()
    {
        GameOver();
        UpdateScore(0);
        if (states == GameStates.inGame)
        {
            gameOver = false;
        }
    }

    public void UpdateScore(int score)
    {
        Score += score;
        scoreText.text = "Score: \n" + Score;
        if(Score == 25 || Score == 50)
        {
            if (!hasPlayingAudio)
            {
                particlePoint.Play();
                _boomSound.Play();
                hasPlayingAudio = true;
            }

        }else if(Score == targetScore)
        {
            targetScore += 100;
            if (!hasPlayingAudio)
            {
                particlePoint.Play();
                _boomSound.Play();
                hasPlayingAudio = true;
            }
        }

        if(Score == 25 || Score == 50 || Score == targetScore)
        {
            hasPlayingAudio = true;
        }
        else
        {
            hasPlayingAudio = false;
        }
    }

    private void SetMaxscore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        if(Score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, Score);
        }
        maxScoreText.text = "Max Score: \n" + maxScore;
    }

    private void GameOver()
    {
        if (gameOver)
        {
            states = GameStates.gameOver;
            time += Time.deltaTime;
            scoreText.enabled = false;
            scoreNowText.text = scoreText.text;
            SetMaxscore();
            quitButton.enabled = true;
            restartButton.enabled = true;
            imageGameOver.alpha = time / fadeDuration;
            if (imageGameOver.alpha == 1)
                Time.timeScale = 0;
        }
    }

    public bool showAnouncement;

    public void RestartGame()
    {
        Score = 0;
        targetScore = 100;
        states = GameStates.inGame;
        gameOver = false;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
