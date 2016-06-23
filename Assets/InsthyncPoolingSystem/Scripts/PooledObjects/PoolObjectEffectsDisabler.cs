using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObjectParticleSystemDisabler : PoolObject
    {
        public ParticleSystem particles;
        public AudioSource audioSource;
        EffectsDisabler effectsDisabler;
        void Awake()
        {
            effectsDisabler = GetComponent<EffectsDisabler>();
            if (effectsDisabler == null)
                effectsDisabler = gameObject.AddComponent<EffectsDisabler>();
            effectsDisabler.particles = particles;
            effectsDisabler.audioSource = audioSource;
            effectsDisabler.onDisable.AddListener(OnDisableEvent);
        }

        void OnDisableEvent()
        {
            transform.parent = poolingSystem.transform;
        }
    }
}