using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sample : MonoBehaviour {

    public Gun gun;
    public Text magazineText;
    public Text pulseText;
    public Text reload;

	void Update ()
    {
        magazineText.text = gun.Magazine.ToString();
        pulseText.text = gun.Pulse.ToString();

        if (gun.ReloadRate == 1) {
            reload.enabled = false;
        } else {
            reload.enabled = true;
            reload.text = ((int)(gun.ReloadRate * 100)).ToString ("D3") + "%";
        }
	}
}
