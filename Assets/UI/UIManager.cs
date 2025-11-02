using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private Label scoreLabel;
    private Label gameOverText;
    private Label countdownTimerLabel;

    [SerializeField]
    private VisualTreeAsset uiAsset;

    private int currentScore = 0;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        scoreLabel = root.Q<Label>("Score");
        gameOverText = root.Q<Label>("GameOver");
        countdownTimerLabel = root.Q<Label>("BeforeStartCounter");

        UpdateScore(currentScore);
        HideGameOver();
        countdownTimerLabel.style.display = DisplayStyle.None;
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

    public void ShowGameOver(string finalMessage = "Вы проиграли!")
    {
        ResetScore(); // Сбрасываем счет
        gameOverText.text = finalMessage;
        gameOverText.style.display = DisplayStyle.Flex;

        scoreLabel.style.display = DisplayStyle.None;
    }

    private void HideGameOver()
    {
        gameOverText.style.display = DisplayStyle.None;
        scoreLabel.style.display = DisplayStyle.Flex;
    }

    public void StartCountdown()
    {
        HideGameOver();

        StartCoroutine(CountdownSequence(3));
    }

    private IEnumerator CountdownSequence(int startValue)
    {
        countdownTimerLabel.style.display = DisplayStyle.Flex;

        for (int i = startValue; i > 0; i--)
        {
            countdownTimerLabel.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }

        countdownTimerLabel.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        countdownTimerLabel.style.display = DisplayStyle.None;
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
            ShowGameOver("Вы набрали " + currentScore + " очков!");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCountdown();
        }
    }
}