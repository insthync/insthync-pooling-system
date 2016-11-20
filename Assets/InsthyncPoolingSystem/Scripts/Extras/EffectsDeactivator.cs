using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class EffectsDeactivator : BaseDeactivator
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

        protected override void OnEnable()
        {
            if (particles != null)
                particles.Play(true);
            if (audioSource != null)
                audioSource.Play();
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            if (particles != null)
                particles.Stop(true);
            if (audioSource != null)
                audioSource.Stop();
            base.OnDisable();
        }

        public override void UpdateLogic()
        {
        }

        public override bool IsDeactivating()
        {
            return ((particles == null || !particles.IsAlive(true)) && (audioSource == null || !audioSource.isPlaying));
        }
    }
}
