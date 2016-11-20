using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObjectEffectsDisabler : PoolObject
    {
        public ParticleSystem particles;
        public AudioSource audioSource;
        EffectsDeactivator effectsDisabler;
        void Awake()
        {
            if (particles == null)
                particles = GetComponentInChildren<ParticleSystem>();
            if (audioSource == null)
                audioSource = GetComponentInChildren<AudioSource>();
            effectsDisabler = GetComponent<EffectsDeactivator>();
            if (effectsDisabler == null)
                effectsDisabler = gameObject.AddComponent<EffectsDeactivator>();
            effectsDisabler.particles = particles;
            effectsDisabler.audioSource = audioSource;
        }
    }
}