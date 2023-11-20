// using UnityEngine;

// public class MusicGame : MonoBehaviour
// {
//     private AudioSource gameMusic;
//     private float currentPlaybackTime;

//     void Start()
//     {
//         // Получаем компонент AudioSource при старте скрипта
//         gameMusic = GetComponent<AudioSource>();
//     }

//     public void PlayAudio()
//     {
//         // Включаем проигрывание звука с текущей позиции
//         if (gameMusic != null && !gameMusic.isPlaying)
//         {
//             gameMusic.Play();
//             gameMusic.time = currentPlaybackTime; // Устанавливаем текущую позицию воспроизведения
//         }
//     }

//     public void PauseAudio()
//     {
//         // Запоминаем текущую позицию воспроизведения перед паузой
//         if (gameMusic != null && gameMusic.isPlaying)
//         {
//             currentPlaybackTime = gameMusic.time;
//             gameMusic.Pause();
//         }
//     }

//     public void StopAudio()
//     {
//         // Останавливаем проигрывание звука
//         if (gameMusic != null && gameMusic.isPlaying)
//         {
//             gameMusic.Stop();
//             currentPlaybackTime = 0f; // Сбрасываем текущую позицию воспроизведения при остановке
//         }
//     }

//     public float GetCurrentPlaybackTime()
//     {
//         return currentPlaybackTime;
//     }

//     public void SetCurrentPlaybackTime(float time)
//     {
//         currentPlaybackTime = time;
//     }
// }