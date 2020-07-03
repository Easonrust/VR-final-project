using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private Rigidbody RG;

    [SerializeField]
    private float forces = 200;

    void Start()
    {
        if(RG == null)
        {
            RG = GetComponent<Rigidbody>();
        }

        RG.AddForce(transform.forward * forces);

        Destroy(gameObject, 3.0f);
    }
}
