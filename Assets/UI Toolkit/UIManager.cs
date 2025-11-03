using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private Label scoreLabel;
    private VisualElement scoreView;
    private VisualElement gameOverView;
    private VisualElement countdownTimer;
    private Label countdownTimerLabel;

    [SerializeField]
    private VisualTreeAsset uiAsset;

    private int currentScore = 0;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        scoreLabel = root.Q<Label>("ScoreText");
        scoreView = root.Q<VisualElement>("Score");
        countdownTimer = root.Q<VisualElement>("BeforeStartCounter");
        countdownTimerLabel = root.Q<Label>("BeforeStartCounterText");
        gameOverView = root.Q<VisualElement>("GameOver");
        Debug.Log(gameOverView);

        UpdateScore(currentScore);
        HideGameOver();
        countdownTimer.style.display = DisplayStyle.None;
        StartCountdown();
    }

    public void UpdateScore(int newScore = -1)
    {
        if (newScore == -1) currentScore += 1;
        else currentScore = newScore;
        scoreLabel.text = $"Счёт: {currentScore}";
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScore(0);
    }

    public void ShowGameOver()
    {
        ResetScore();
        gameOverView.style.display = DisplayStyle.Flex;

        scoreView.style.display = DisplayStyle.None;
    }

    private void HideGameOver()
    {
        gameOverView.style.display = DisplayStyle.None;
        scoreView.style.display = DisplayStyle.Flex;
    }

    public void StartCountdown()
    {
        HideGameOver();

        StartCoroutine(CountdownSequence(3));
    }

    private IEnumerator CountdownSequence(int startValue)
    {
        countdownTimer.style.display = DisplayStyle.Flex;

        for (int i = startValue; i > 0; i--)
        {
            countdownTimerLabel.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }

        countdownTimerLabel.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        countdownTimer.style.display = DisplayStyle.None;
    }

    void Update()
    {
        // Тест: Нажмите 'S' чтобы обновить счет
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateScore(currentScore + 1);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowGameOver();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCountdown();
        }
    }
}