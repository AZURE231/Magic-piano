using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private const string SCORED = "Scored";
    private const string LIGHTEN = "Lighten";
    private const string PERFECT_TEXT = "Up";
    private const string START = "Start";
    private Color color1 = new Color(117f / 255f, 255f / 255f, 184f / 255f);
    private Color color2 = new Color(33f / 255f, 255f / 255f, 141f / 255f);



    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject decoImage;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject perfectText;
    [SerializeField] private GameObject greatText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject comboText;

    private Color initialBgColor;


    private void Start()
    {
        GameManager.Instance.OnScoreChanged += Instance_OnScoreChanged;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
        GameManager.Instance.OnRestartGame += Instance_OnRestartGame;
        GameManager.Instance.OnGameWin += Instance_OnGameWin;
        initialBgColor = backgroundImage.color;
    }

    private void Instance_OnGameWin(object sender, System.EventArgs e)
    {
        gameWinPanel.SetActive(true);
    }

    private void Instance_OnRestartGame(object sender, System.EventArgs e)
    {
        gameOverPanel.SetActive(false);
        backgroundImage.color = initialBgColor;
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
            comboText.GetComponent<Animator>().SetTrigger(PERFECT_TEXT);
            comboText.GetComponent<TMP_Text>().text = "x" + e.combo.ToString();
        }
        else
        {
            greatText.GetComponent<Animator>().SetTrigger(PERFECT_TEXT);
        }

        // Change background color
        if (e.score >= 20)
        {
            backgroundImage.color = color1;

        }
        else if (e.score >= 100)
        {
            backgroundImage.color = color2;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameAfterSecond());
    }

    private IEnumerator StartGameAfterSecond()
    {
        startGamePanel.GetComponent<Animator>().SetTrigger(START);
        yield return new WaitForSeconds(3);
        startGamePanel.SetActive(false);

    }
}
