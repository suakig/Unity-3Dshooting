using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour
{
    Status[] lockArray;
    GameObject target = null;

    void Start ()
    {
        lockArray = FindObjectsOfType<Status>();
        LockTarget ();
	}

    public GameObject GetTarget()
    {
        return target;
    }

    public void LockTarget()
    {
        if (lockArray.Length <= 1)
        {
            return;
        }

        Debug.Log (lockArray.Length);

        foreach(Status lockedGameObject in lockArray) {
            if (this.gameObject == lockedGameObject.gameObject) {
                continue;
            }
            target = lockedGameObject.gameObject;
        }
    }
}
