﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Gun.csクラスによって生成された弾丸の処理クラス
/// </summary>
public class Bullet : MonoBehaviour
{
    protected GameObject whoMake = null;
    protected GameObject target = null;    // ロックオンしたターゲット
    public float speed;
    public float deleteTime;

    public virtual void Start()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="gun">Gun.</param>
    public virtual void Init(Transform gunBattery, GameObject whoMake)
    {
        this.name = this.GetType ().Name;
        this.transform.rotation = gunBattery.rotation;
        this.transform.position = gunBattery.position;
        Destroy (this.gameObject, deleteTime);
        this.transform.parent = Stage.Instance.transform;

        if (whoMake) {
            this.whoMake = whoMake;
            target = whoMake.GetComponent<Lock> ().GetTarget ();
        }
    }
}