using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
	void Start ()
    {
        _Time.StartDeltaTime ();
	}
	
	void Update ()
    {
        _Time.UpdateDeltaTime ();
	}
}
