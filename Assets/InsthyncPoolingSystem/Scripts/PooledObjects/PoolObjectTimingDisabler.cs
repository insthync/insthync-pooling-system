using UnityEngine;

namespace Insthync.PoolingSystem
{
    [RequireComponent(typeof(TimingDeactivator))]
    public class PoolObjectTimingDisabler : PoolObject
    {
        public float disableTime;
        TimingDeactivator timingDeactivator;
        void Awake()
        {
            timingDeactivator = GetComponent<TimingDeactivator>();
            if (timingDeactivator == null)
                timingDeactivator = gameObject.AddComponent<TimingDeactivator>();
            timingDeactivator.deactivatingTime = disableTime;
        }
    }
}