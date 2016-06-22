using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObjectParticleSystemDisabler : PoolObject
    {
        public ParticleSystem particles;
        public AudioSource audioSource;

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
                transform.parent = poolingSystem.transform;
                gameObject.SetActive(false);
            }
        }
    }
}