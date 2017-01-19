using UnityEngine;

namespace Insthync.PoolingSystem
{
    [RequireComponent(typeof(EffectsDeactivator))]
    public class PoolObjectEffectsDisabler : PoolObject
    {
        public ParticleSystem particles;
        public AudioSource audioSource;
        EffectsDeactivator effectsDeactivator;
        void Awake()
        {
            if (particles == null)
                particles = GetComponentInChildren<ParticleSystem>();
            if (audioSource == null)
                audioSource = GetComponentInChildren<AudioSource>();
            effectsDeactivator = GetComponent<EffectsDeactivator>();
            if (effectsDeactivator == null)
                effectsDeactivator = gameObject.AddComponent<EffectsDeactivator>();
            effectsDeactivator.particles = particles;
            effectsDeactivator.audioSource = audioSource;
        }
    }
}