using UnityEngine;
namespace Insthync.PoolingSystem
{
    public class TimingDeactivator : BaseDeactivator
    {
        public float deactivatingTime;

        float timer = 0;
        protected override void OnEnable()
        {
            ResetTimer();
            base.OnEnable();
        }

        public override void UpdateLogic()
        {
            timer += Time.deltaTime;
        }

        public override bool IsDeactivating()
        {
            return (timer > deactivatingTime);
        }

        public void ResetTimer()
        {
            timer = 0;
        }
    }
}
