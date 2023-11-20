using UnityEngine;

public class MusicManagers : MonoBehaviour
{
    private float currentPlaybackTime;
    
    public AudioSource gameMusic;
    public AudioSource bossMusic;

    private void Start()
    {
        // Получаем компоненты AudioSource при старте скрипта
        gameMusic = GameObject.Find("GameMusicGameObjectName").GetComponent<AudioSource>();
        bossMusic = GameObject.Find("BossMusicGameObjectName").GetComponent<AudioSource>();
    }

    public void PlayGameMusic()
      {
        // Включаем проигрывание звука с текущей позиции
        if (gameMusic != null && !gameMusic.isPlaying)
        {
            gameMusic.Play();
            gameMusic.time = currentPlaybackTime; // Устанавливаем текущую позицию воспроизведения
        }
    }

    public void PauseGameMusic()
    {
        // Запоминаем текущую позицию воспроизведения перед паузой
        if (gameMusic != null && gameMusic.isPlaying)
        {
            currentPlaybackTime = gameMusic.time;
            gameMusic.Pause();
        }
    }

    public void PlayBossMusic()
    {
        bossMusic.Play();
    }

    public void PauseBossMusic()
    {
        bossMusic.Pause();
    }

    public float GetCurrentPlaybackTime()
    {
        return currentPlaybackTime;
    }

    public void SetCurrentPlaybackTime(float time)
    {
        currentPlaybackTime = time;
    }
}