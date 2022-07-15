using System;
using UnityEngine;

namespace Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioSource gunEffects;
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

        public void PlayAudio(AudioClip clip)
        {
            gunEffects.PlayOneShot(clip);
        }
    }
}
