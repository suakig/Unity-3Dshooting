using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float deleteTime;

    void Start()
    {
        this.rigidbody.velocity = this.transform.forward * speed;
    }

    public void Init(GameObject gun)
    {
        this.name = this.GetType().Name;
        this.transform.rotation = gun.transform.rotation;
        this.transform.position = gun.transform.position;
        Destroy (this.gameObject, deleteTime);
        this.transform.parent = Stage.Instance.transform;
    }
}