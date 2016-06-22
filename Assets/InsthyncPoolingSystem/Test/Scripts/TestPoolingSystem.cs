using UnityEngine;
using System.Collections;
using Insthync.PoolingSystem;
public class TestPoolingSystem : MonoBehaviour {
    public GameObject poolingObject;
    public float timeBeforeDestroy;
    public float fireDuration;
    PoolingSystem poolingSystem;

    void Awake () {
        PoolObjectTimingDisabler pooler = poolingObject.AddComponent<PoolObjectTimingDisabler>();
        pooler.disableTime = timeBeforeDestroy;

        poolingSystem = PoolingSystem.CreatePoolingSystem(poolingObject, 50);
    }

	void Start () {
        Fire();
	}

	void Update () {
	    
	}

    void Fire() {
        poolingSystem.GetNextObject(Vector3.zero, Quaternion.identity);
        Invoke("Fire", fireDuration);
    }
}
