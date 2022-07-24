using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioSource backgroundMusic;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayAudio(AudioSource audioSource,AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
        
    }
}
