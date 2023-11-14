using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenManager : MonoBehaviour
{
    public float splashScreenDuration = 3f; // Длительность отображения заставки в секундах.

    void Start()
    {
        // Запустить корутину для отображения заставки.
        StartCoroutine(ShowSplashScreen());
    }

    IEnumerator ShowSplashScreen()
    {
        // Подождать splashScreenDuration секунд.
        yield return new WaitForSeconds(splashScreenDuration);

        // Загрузить следующую сцену (вашу основную игровую сцену).
        LoadGameScene();
    }

    void LoadGameScene()
    {
        // Загрузить следующую сцену по её названию.
        SceneManager.LoadScene("Game");
    }
}
