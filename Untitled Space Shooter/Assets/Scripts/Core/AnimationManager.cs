using System;
using UnityEngine;

namespace Core
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance;

        private Animator _animator;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }

        public void PlayAnimation(AnimationClip clip)
        {
            _animator.Play(clip.name);
        }
    }
}
