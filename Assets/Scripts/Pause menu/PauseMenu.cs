using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Start()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        videoPlayer.Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            videoPlayer.Play();
            Debug.Log("Пауза");
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            videoPlayer.Pause();
            Debug.Log("Продолжение");
        }
    }
}