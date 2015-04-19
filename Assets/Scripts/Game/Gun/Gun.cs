using UnityEngine;
using System.Collections;

/// <summary>
/// 弾丸を生成発射するクラス
/// リロード時間
/// クールタイム時間
/// パルス発射
/// 発射方法変更
/// </summary>
public class Gun : MonoBehaviour
{
    //発射方法を変更できる
    public enum InputType {
        Push  = 1,  //クリックしたら発射
        Hover = 2,  //クリック押しっぱなしで発射
        Pull  = 4,  //クリック離すと発射
        Auto  = 8,  //自動で発射
        Call  = 16  //スクリプト命令で発射
    };

    public InputType inputType = InputType.Hover;   //発射入力タイプ
    public GameObject bulletPrefab;                 //発射するオブジェクト
    public int maxMmagazine = 5;                    //マガジンの段数 なくなるとリロード
    public int maxPulse = 3;                        //一回ボタンを押すと生成される回数
    public float reloadTime = 2.0f;                 //リロード時間
    public float coolTime = 0.5f;                   //クールタイム
    public float intervalTime = 0.1f;               //パルスの発射間隔

    [System.NonSerialized] public GameObject whoMake;     //誰が作成したのか

    private TimeDo reloadTimeDo;    //リロードの命令
    private TimeDo coolTimeDo;      //クールタイムの命令
    private TimeDo intervalTimeDo;  //パルスの発射命令
    private bool shotInput;         //発射命令フラグ
    private bool isReloaded;        //Mmagazine弾丸補充フラグ
    private bool isCooltimeEnd;     //Pulse弾丸補充フラグ

    /// <summary>
    /// 現在の所持弾数
    /// </summary>
    /// <value>The magazine.</value>
    public int Magazine {
        private set;
        get;
    }

    /// <summary>
    /// 発射段数
    /// </summary>
    /// <value>The pulse.</value>
    public int Pulse {
        private set;
        get;
    }

    /// <summary>
    /// 消費した弾丸をパーセンテージで示す
    /// </summary>
    /// <value>The reload rate.</value>
    public float ReloadRate {
        get {
            return reloadTimeDo.Rate;
        }
    }

    void Start ()
    {
        Init ();
    }

    /// <summary>
    /// 入力処理はUpdateで行う
    /// </summary>
    void Update()
    {
        ShotInput ();
    }

    /// <summary>
    /// Updateで行うと時間がずれ、ショットの発射間隔が正常でなくなるためここで行う
    /// </summary>
    void FixedUpdate ()
    {
        if (ShotSetUpTime ()) {
            return;
        }

        CreateBullet ();

        CalculateElasticAftereffect ();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
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

    /// <summary>
    /// 龍直タイプに応じた処理を行う
    /// </summary>
    private void ShotInput()
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
    /// ショットが現在打てるかを返すクラス
    /// </summary>
    /// <returns><c>true</c>, if set up time was shoted, <c>false</c> otherwise.</returns>
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

    /// <summary>
    /// ショットを撃った後の残弾数に応じた処理を行う
    /// </summary>
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

    /// <summary>
    /// 弾丸の作成、作成した弾丸の移動は弾丸クラスで行う
    /// </summary>
    private void CreateBullet()
    {
        GameObject bullet = Instantiate (bulletPrefab) as GameObject;
        bullet.GetComponent<Bullet> ().Init (this.gameObject, whoMake);
    }

    /// <summary>
    /// 外部命令による入力処理
    /// </summary>
    public void CallShotInput()
    {
        if (inputType == InputType.Call) {
            shotInput = true;
        }
    }
}