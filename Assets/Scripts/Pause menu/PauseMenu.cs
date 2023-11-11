using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Для работы с сценами

public class PauseMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject pauseMenuUI;
    public Button continueButton;
    public Button restartButton;

    private bool isPaused = false;
    private Button selectedButton; // Ссылка на текущую выбранную кнопку

    void Start()
    {
        if (videoPlayer == null)
        {
            UnityEngine.Debug.LogError("VideoPlayer is not assigned!");
            return;
        }

        videoPlayer.Pause();

        // Добавляем слушателей для кнопок
        continueButton.onClick.AddListener(ContinueButton);
        restartButton.onClick.AddListener(RestartButton);

        // Начинаем с выбранной кнопки "ContinueButton"
        selectedButton = continueButton;
        SelectButton(selectedButton);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Обработка клавиш вверх и вниз
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
                // Вызываем метод для выбранной кнопки при нажатии Enter
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
        }
    }

    // Метод для кнопки "Continue"
    public void ContinueButton()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);

        // Проверяем, включен ли VideoPlayer, прежде чем вызывать Pause()
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

        // Выключаем кнопки после их использования
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Метод для кнопки "Restart"
    public void RestartButton()
    {
        // Перезагрузка текущей сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Метод для изменения выбранной кнопки
    private void ChangeSelectedButton(Button newButton)
    {
        DeselectButton(selectedButton);
        selectedButton = newButton;
        SelectButton(selectedButton);
    }

    // Метод для выделения кнопки (например, изменение цвета)
    private void SelectButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.yellow; // Меняем цвет при выделении
        button.colors = colors;
    }

    // Метод для снятия выделения с кнопки
    private void DeselectButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.white; // Возвращаем обычный цвет
        button.colors = colors;
    }
}
