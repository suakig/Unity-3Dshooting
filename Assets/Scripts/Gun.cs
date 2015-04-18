using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public enum InputType {
        Push  = 1,
        Hover = 2,
        Pull  = 4,
        Auto  = 8,
        Call  = 16
    };

    private TimeDo reloadTimeDo;
    private TimeDo coolTimeDo;
    private TimeDo intervalTimeDo;
    private bool shotInput;
    private bool isReloaded;
    private bool isCooltimeEnd;

    public InputType inputType = InputType.Hover;
    public GameObject bulletPrefab;
    public int maxMmagazine = 5;
    public int maxPulse = 3;
    public float reloadTime = 2.0f;
    public float coolTime = 0.5f;
    public float intervalTime = 0.1f;

    public int Magazine {
        private set;
        get;
    }

    public int Pulse {
        private set;
        get;
    }

    public float ReloadRate {
        get {
            return reloadTimeDo.Rate;
        }
    }

    void Start ()
    {
        reloadTimeDo = new TimeDo (reloadTime, true);
        coolTimeDo = new TimeDo (coolTime, true);
        intervalTimeDo = new TimeDo (intervalTime, true);

        Magazine = maxMmagazine;
        Pulse = maxPulse;
        shotInput = false;
        isReloaded = false;
        isCooltimeEnd = false;
    }

    void Update()
    {
        switch (inputType) {
        case InputType.Push:
            if (Input.GetMouseButtonDown (0)) {
                shotInput = true;
            }
            break;
        case InputType.Pull:
            if (Input.GetMouseButtonUp (0)) {
                shotInput = true;
            }
            break;
        case InputType.Hover:
            if (Input.GetMouseButton (0)) {
                shotInput = true;
            }
            break;
        case InputType.Auto:
            shotInput = true;
            break;
        }
    }

    /// <summary>
    /// Updateで行うと時間がずれる
    /// </summary>
    void FixedUpdate ()
    {
        if (ShotSetUpTime ()) {
            return;
        }

        CreateBullet ();

        CalculateElasticAftereffect ();
    }

    private bool ShotSetUpTime()
    {
        if (!reloadTimeDo.FixedUpdate (TimeDo.Type.Forever)) {
            //リロードタイム中
            shotInput = false;
            isReloaded = false;
            return true;
        }

        if (!isReloaded) {
            //マガジンの補充
            isReloaded = true;
            Magazine = maxMmagazine;
            Pulse = maxPulse;
        }

        if (!coolTimeDo.FixedUpdate (TimeDo.Type.Forever)) {
            //クールタイム中
            shotInput = false;
            isCooltimeEnd = false;
            return true;
        }

        if (!isCooltimeEnd) {
            //パルスの補充
            isCooltimeEnd = true;
            Pulse = maxPulse;
        }

        if (!intervalTimeDo.FixedUpdate (TimeDo.Type.Loop)) {
            //発射間隔
            return true;
        }

        if (!shotInput) {
            //入力がない
            return true;
        }

        return false;
    }

    private void CalculateElasticAftereffect()
    {
        if (--Pulse > 0) {
            //パルスが残ってる
            return;
        }

        shotInput = false;
        coolTimeDo.ReSet ();

        if (--Magazine > 0) {
            //マガジンが残ってる
            return;
        }
        reloadTimeDo.ReSet ();
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate (bulletPrefab) as GameObject;
        bullet.GetComponent<Bullet>().Init (this.gameObject);
    }

    public void CallShotInput()
    {
        shotInput = true;
    }
}