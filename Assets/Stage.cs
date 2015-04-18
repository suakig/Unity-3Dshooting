using UnityEngine;

public class Stage : SingletonMonoBehaviour<Stage>
{

    public void Awake()
    {
        if(this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }    

}