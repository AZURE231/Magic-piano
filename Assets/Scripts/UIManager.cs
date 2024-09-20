using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private const string SCORED = "Scored";
    private const string LIGHTEN = "Lighten";
    private const string PERFECT_TEXT = "Up";

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject decoImage;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameWinPanel;
    [SerializeField] GameObject perfectText;
    [SerializeField] GameObject greatText;


    private void Start()
    {
        GameManager.Instance.OnScoreChanged += Instance_OnScoreChanged;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
        GameManager.Instance.OnRestartGame += Instance_OnRestartGame;
        GameManager.Instance.OnGameWin += Instance_OnGameWin;
    }

    private void Instance_OnGameWin(object sender, System.EventArgs e)
    {
        gameWinPanel.SetActive(true);
    }

    private void Instance_OnRestartGame(object sender, System.EventArgs e)
    {
        gameOverPanel.SetActive(false);
    }

    private void Instance_OnGameOver(object sender, System.EventArgs e)
    {
        gameOverPanel.SetActive(true);
    }

    private void Instance_OnScoreChanged(object sender, GameManager.OnScoreChangedEventArgs e)
    {
        scoreText.text = e.score.ToString();
        scoreText.GetComponent<Animator>().SetTrigger(SCORED);
        decoImage.GetComponent<Animator>().SetTrigger(LIGHTEN);
        if (e.isPerfect)
        {
            perfectText.GetComponent<Animator>().SetTrigger(PERFECT_TEXT);
        }
        else
        {
            greatText.GetComponent<Animator>().SetTrigger(PERFECT_TEXT);
        }
    }
}
