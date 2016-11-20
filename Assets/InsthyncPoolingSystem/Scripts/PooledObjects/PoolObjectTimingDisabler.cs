using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObjectTimingDisabler : PoolObject
    {
        public float disableTime;
        TimingDeactivator timeDisabler;
        void Awake()
        {
            timeDisabler = GetComponent<TimingDeactivator>();
            if (timeDisabler == null)
                timeDisabler = gameObject.AddComponent<TimingDeactivator>();
            timeDisabler.deactivatingTime = disableTime;
        }
    }
}