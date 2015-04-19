using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour
{
    GameObject[] enemyArray;
    Gun[] gun;

    void Start()
    {
        Gun[] gun = transform.FindChild ("Equipment").GetComponentsInChildren<Gun> ();
        for (int i = 0; i < gun.Length; i++)
        {
            gun [i].whoMake = this.gameObject;
        }
    }

	void Update ()
    {
        enemyArray = GameObject.FindGameObjectsWithTag ("Enemy");
	}

    public GameObject GetTarget()
    {
        if (enemyArray.Length == 0) {
            return null;
        }

        return enemyArray [0];
    }
}
