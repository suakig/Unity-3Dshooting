using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Equipment : MonoBehaviour {

    public enum ID : int {
        Player              =   1,
        Enemy               =   2,

        MultiplayPlayer1    =   3,
        MultiplayPlayer2    =   4,
        MultiplayPlayer3    =   5,
        MultiplayPlayer4    =   6,
    };
        
    [System.Serializable]
    public class PointData
    {
        public Path.Bullets.ID bulletsID = Path.Bullets.ID.Ballet3Way;
        public Transform equipTransform;
        [System.NonSerialized] public Gun gun;
    }

    public PointData[] pointDataArray;

    void Start()
    {
        string weaponPath = "";
        foreach (PointData pointData in pointDataArray)
        {
            weaponPath = Path.Bullets.GetPathByID (pointData.bulletsID);

            GameObject weapoin = Instantiate (Resources.Load (weaponPath)) as GameObject;
            weapoin.transform.parent = this.transform;
            weapoin.transform.position = pointData.equipTransform.position;
//            weapoin.transform.localPosition = Vector3.zero;
            weapoin.transform.localRotation = Quaternion.identity;

            pointData.gun = weapoin.GetComponent<Gun> ();
            pointData.gun.whoEquip = this.gameObject;
        }
////        gunList [0].gameObject.SetActive(false);
        FindObjectOfType<sample> ().gun = pointDataArray [0].gun;
    }
}
