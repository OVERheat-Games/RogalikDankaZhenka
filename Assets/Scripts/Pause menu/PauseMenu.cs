using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Для работы с сценами
using TMPro;
using System.Diagnostics;


public class PauseMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject pauseMenuUI;
    public Button continueButton;
    public Button restartButton;
    public TextMeshProUGUI continueButtonText;
    public TextMeshProUGUI restartButtonText;

    private bool isPaused = false;
    private Button selectedButton;

    void Start()
    {
        if (videoPlayer == null)
        {
            UnityEngine.Debug.LogError("VideoPlayer is not assigned!");
            return;
        }

        videoPlayer.Pause();

        continueButton.onClick.AddListener(ContinueButton);
        restartButton.onClick.AddListener(RestartButton);

        selectedButton = continueButton;
        SelectButton(selectedButton);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeSelectedButton(continueButton);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeSelectedButton(restartButton);
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                selectedButton.onClick.Invoke();
            }
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (videoPlayer == null)
        {
            UnityEngine.Debug.LogError("VideoPlayer is not assigned!");
            return;
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            videoPlayer.Play();
            UnityEngine.Debug.Log("Пауза");

            // Активируем кнопки перед их использованием
            continueButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);

            // Начинаем с выбранной кнопки "ContinueButton"
            selectedButton = continueButton;
            SelectButton(selectedButton);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            videoPlayer.Pause();
            UnityEngine.Debug.Log("Продолжение");

            // Выключаем кнопки после их использования
            continueButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }
    }

    public void ContinueButton()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);

        if (videoPlayer.isActiveAndEnabled)
        {
            videoPlayer.Pause();
        }
        else
        {
            UnityEngine.Debug.LogWarning("VideoPlayer is disabled. Cannot pause.");
        }

        isPaused = false;
        UnityEngine.Debug.Log("Продолжение");

        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPaused = false;
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }


    private void ChangeSelectedButton(Button newButton)
    {
        DeselectButton(selectedButton);
        selectedButton = newButton;
        SelectButton(selectedButton);
    }

    private void SelectButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.yellow;
        button.colors = colors;

        TextMeshProUGUI buttonText = GetButtonText(button);
        if (buttonText != null)
        {
            buttonText.color = Color.yellow;
        }
    }

    private void DeselectButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;

        TextMeshProUGUI buttonText = GetButtonText(button);
        if (buttonText != null)
        {
            buttonText.color = Color.white;
        }
    }

    private TextMeshProUGUI GetButtonText(Button button)
    {
        if (button == continueButton)
        {
            return continueButtonText;
        }
        else if (button == restartButton)
        {
            return restartButtonText;
        }

        return null;
    }

    private void SetButtonColor(TextMeshProUGUI buttonText, Color color)
    {
        if (buttonText != null)
        {
            buttonText.color = color;
        }
    }
}