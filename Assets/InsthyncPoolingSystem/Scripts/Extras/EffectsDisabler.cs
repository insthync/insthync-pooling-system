using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Insthync.PoolingSystem
{
    public class EffectsDisabler : MonoBehaviour
    {
        public ParticleSystem particles;
        public AudioSource audioSource;
        public UnityEvent onDisable;

        void Awake()
        {
            if (particles == null)
                particles = GetComponentInChildren<ParticleSystem>();
            if (audioSource == null)
                audioSource = GetComponentInChildren<AudioSource>();
            if (particles == null && audioSource == null)
                Destroy(gameObject);
        }

        void OnEnable()
        {
            if (particles != null)
                particles.Play(true);
            if (audioSource != null)
                audioSource.Play();
        }

        void Update()
        {
            if ((particles == null || !particles.IsAlive(true)) &&
                (audioSource == null || !audioSource.isPlaying))
            {
                RaiseOnDisable();
                gameObject.SetActive(false);
            }
        }

        void RaiseOnDisable()
        {
            if (onDisable != null)
                onDisable.Invoke();
        }
    }
}
