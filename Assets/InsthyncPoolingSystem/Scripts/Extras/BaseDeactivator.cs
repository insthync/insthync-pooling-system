using UnityEngine;
using UnityEngine.Events;

namespace Insthync.PoolingSystem
{
    public abstract class BaseDeactivator : MonoBehaviour
    {
        public bool isForceDeactivate;
        public UnityEvent onDisable;
        protected virtual void Update()
        {
            UpdateLogic();
            if (IsDeactivating() || isForceDeactivate)
            {
                isForceDeactivate = false;
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnEnable()
        {
            isForceDeactivate = false;
        }

        protected virtual void OnDisable()
        {
            RaiseOnDisable();
        }

        void RaiseOnDisable()
        {
            if (onDisable != null)
                onDisable.Invoke();
        }

        public void ForceDeactive()
        {
            isForceDeactivate = true;
        }

        public abstract void UpdateLogic();
        public abstract bool IsDeactivating(); 
    }
}