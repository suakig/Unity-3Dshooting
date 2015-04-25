using UnityEngine;
using System.Collections;

public class Homing : Bullet
{
    public float rotSpeed = 180.0f;  // 1秒間に回転する角度

    public override void Start()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    void FixedUpdate()
    {
        if (target)
        {
            HomingRotation ();
        }

        this.GetComponent<Rigidbody>().velocity = this.transform.forward * this.GetComponent<Rigidbody>().velocity.magnitude;
    }

    /// <summary>
    /// 追尾
    /// </summary>
    void HomingRotation()
    {
        // ターゲットまでの角度を取得
        Vector3    vecTarget  = target.transform.position - transform.position; // ターゲットへのベクトル
        Vector3    vecForward = transform.TransformDirection(Vector3.forward);  // 弾の正面ベクトル
        float      angleDiff  = Vector3.Angle(vecForward, vecTarget);           // ターゲットまでの角度
        float      angleAdd   = (rotSpeed * Time.fixedDeltaTime);               // 回転角
        Quaternion rotTarget  = Quaternion.LookRotation(vecTarget);             // ターゲットへ向けるクォータニオン

        if (angleDiff <= angleAdd)
        {
            // ターゲットが回転角以内なら完全にターゲットの方を向く
            transform.rotation = rotTarget;
        }
        else
        {
            // ターゲットが回転角の外なら、指定角度だけターゲットに向ける
            float t = (angleAdd / angleDiff);
            this.GetComponent<Rigidbody>().rotation = Quaternion.Slerp (transform.rotation, rotTarget, t);
        }
    }
}