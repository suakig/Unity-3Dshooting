using UnityEngine;
using System.Collections;

/// <summary>
/// 時間を計測してtrue,falseを返すクラス
/// </summary>
public class TimeDo
{
    /********************************************************
     * 型
    ********************************************************/
    public enum Type {
        Loop                =   1,  //一定の時間置きにtrueを出す。
        Forever             =   2   //一度trueになったらずっとTrueを返す
    };

    public enum When {
        Nomal       =   1,  //時間が止まったらやらない
        AnyTimeDo   =   2   //時間が止まってもやる
    };
    /********************************************************
     * 変数
    ********************************************************/
    private bool isActive;

    /********************************************************
     * 何これ
    ********************************************************/
    /// <summary>
    /// 実行時間
    /// </summary>
    /// <value>The call time.</value>
    public float CallTime{
        private set;
        get;
    }

    /// <summary>
    /// 経過時間
    /// </summary>
    /// <value>The count.</value>
    public float Count {
        private set;
        get;
    }

    /// <summary>
    /// 現在の状態を0.0〜1.0で返す
    /// </summary>
    /// <value>The rate.</value>
    public float Rate
    {
        get {
            if (this.CallTime == 0) {
                return 0;
            }
            return Count / this.CallTime;
        }
    }
    /********************************************************
     * 生成
    ********************************************************/
    /// <summary>
    /// 実行する時間設定と初期値をtrueにするかどうか
    /// </summary>
    /// <param name="end">End.</param>
    /// <param name="startState">If set to <c>true</c> start state.</param>
    public TimeDo(float CallTime, bool startState = false)
    {
        this.CallTime = CallTime;
        if (startState) {
            Count = CallTime;
        } else {
            ReSet ();
        }
        Start ();
    }

    /********************************************************
     * 関数
    ********************************************************/
    /// <summary>
    /// Ises the over.
    /// </summary>
    /// <returns><c>true</c>, if over was ised, <c>false</c> otherwise.</returns>
    /// <param name="countType">Count type.</param>
    private bool OverInit(Type countType)
    {
        if (Count >= this.CallTime) {
            if (countType == Type.Loop) {
                Count -= this.CallTime;// 0にすると、時間を越して処理していた場合越し分がリセットされるため減算している
            } else {
                Count = this.CallTime;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void ReSet()
    {
        Count = 0.0f;
    }

    /// <summary>
    /// カウントの開始
    /// </summary>
    public void Start()
    {
        isActive = true;
    }

    /// <summary>
    /// カウントの停止
    /// </summary>
    public void Stop()
    {
        isActive = false;
    }

    /// <summary>
    /// 実行される時間になっていたらtrue
    /// </summary>
    /// <returns><c>true</c>, if over was ised, <c>false</c> otherwise.</returns>
    public bool IsOver()
    {
        if (Count >= this.CallTime) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 時間を加算し、実行をtrueで知らせる
    /// </summary>
    /// <param name="countType">Count type.</param>
    /// <param name="when">When.</param>
    public bool Update(Type countType, When when)
    {
        if (isActive) {
            if (when == When.AnyTimeDo) {
                Count += _Time.deltaTime;
            } else {
                Count += Time.deltaTime;
            }
            return OverInit (countType);
        }
        return false;
    }

    /// <summary>
    /// カウントだけ行う
    /// </summary>
    /// <param name="when">When.</param>
    public void UpdateOnly(When when)
    {
        if (isActive) {
            if (when == When.AnyTimeDo) {
                Count += _Time.deltaTime;
            } else {
                Count += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Fixeds the count.
    /// </summary>
    /// <returns><c>true</c>, if count was fixeded, <c>false</c> otherwise.</returns>
    /// <param name="countType">Count type.</param>
    public bool FixedUpdate(Type countType)
    {
        if (isActive) {
            Count += Time.fixedDeltaTime;
            return OverInit (countType);
        }
        return false;
    }

    /// <summary>
    /// Fixeds the count.
    /// </summary>
    /// <returns><c>true</c>, if count was fixeded, <c>false</c> otherwise.</returns>
    /// <param name="countType">Count type.</param>
    public void FixedUpdateOnly(Type countType)
    {
        if (isActive) {
            Count += Time.fixedDeltaTime;
        }
    }
}