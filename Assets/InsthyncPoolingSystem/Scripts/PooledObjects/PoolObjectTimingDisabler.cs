using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObjectTimingDisabler : PoolObject
    {
        public float disableTime;
        TimingDisabler timeDisabler;
        void Awake()
        {
            timeDisabler = GetComponent<TimingDisabler>();
            if (timeDisabler == null)
                timeDisabler = gameObject.AddComponent<TimingDisabler>();
            timeDisabler.disableTime = disableTime;
            timeDisabler.onDisable.AddListener(OnDisableEvent);
        }

        void OnDisableEvent()
        {
            transform.parent = poolingSystem.transform;
        }
    }
}