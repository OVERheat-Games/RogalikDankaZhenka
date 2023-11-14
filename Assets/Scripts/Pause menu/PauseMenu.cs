using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public MusicGame musicGame;
    public AudioSource EndPause;
    public AudioSource startPause;
    public AudioSource soundPause;

    public VideoPlayer videoPlayer;

    public GameObject Hud;
    public GameObject pauseMenuUI;
    public GameObject spritePlay;

    public Button continueButton;
    public Button restartButton;

    public TextMeshProUGUI continueButtonText;
    public TextMeshProUGUI restartButtonText;
    public TextMeshProUGUI gameTimeText;

    private bool isPaused = false;
    private Button selectedButton;
    private float timer = 0f;
    private bool endPauseAudioPlayed = false;

    private Restart_Button restartButtonScript;

    public bool IsPaused
    {
        get { return isPaused; }
    }

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    else
        {
            UnityEngine.Debug.LogError("VideoPlayer is not assigned!");
        }


        restartButtonScript = GetComponentInChildren<Restart_Button>();

        continueButton.onClick.AddListener(ContinueButton);
        restartButton.onClick.AddListener(RestartButton);

        selectedButton = continueButton;
        SelectButton(selectedButton);
    }

    void Update()
    {
        if (!isPaused)
        {
            timer += Time.deltaTime;
            UpdateGameTimeUI();
        }

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

    void UpdateGameTimeUI()
    {
        if (gameTimeText != null)
        {
            gameTimeText.text = FormatTime(timer);
        }
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
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
            musicGame.PauseAudio();
            startPause.Play();
            soundPause.Play();
            Time.timeScale = 0f;
            Hud.SetActive(false);
            pauseMenuUI.SetActive(true);
            videoPlayer.Play();
            UnityEngine.Debug.Log("Пауза");

            continueButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);

            // Включаем объект при снятии паузы
            spritePlay.SetActive(false);

            selectedButton = continueButton;
            SelectButton(selectedButton);

            // Сбрасываем флаг, когда входим в паузу
            endPauseAudioPlayed = false;
        }
        else
        {
            if (!endPauseAudioPlayed)
            {
                EndPause.Play();
                StartCoroutine(DisableEndPauseAudio(EndPause.clip.length));
                endPauseAudioPlayed = true;
            }

            musicGame.PlayAudio();
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            videoPlayer.Pause();
            Hud.SetActive(true);
            UnityEngine.Debug.Log("Продолжение");

            continueButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);

            spritePlay.SetActive(true);
        }
    }

    void ContinueButton()
    {
        EndPause.Play();
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        musicGame.PlayAudio();

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

        spritePlay.SetActive(true);
    }

    void RestartButton()
    {
        Invoke("PlayRestartAudio", 0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPaused = false;
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        spritePlay.SetActive(true);

        if (restartButtonScript != null)
        {
            restartButtonScript.PlayAudio();
        }
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
        colors.normalColor = Color.white;
        button.colors = colors;

        TextMeshProUGUI buttonText = GetButtonText(button);
        if (buttonText != null)
        {
            buttonText.color = Color.white;
        }
    }

    private void DeselectButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.gray;
        button.colors = colors;

        TextMeshProUGUI buttonText = GetButtonText(button);
        if (buttonText != null)
        {
            buttonText.color = Color.gray;
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

    private IEnumerator DisableEndPauseAudio(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (EndPause != null && EndPause.isPlaying)
        {
            EndPause.Stop();
        }
    }
}
