using Insthync.PoolingSystem;

namespace Suriyun.MultiplayerRPG
{
    public class DeadDeactivator : BaseDeactivator
    {
        public GameEntity target;

        public override void UpdateLogic()
        {
        }

        public override bool IsDeactivating()
        {
            return (target != null && target.IsDead());
        }
    }
}
