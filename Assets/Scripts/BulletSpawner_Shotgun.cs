using UnityEngine;
using System.Collections;

public class BulletSpawner_Shotgun : MonoBehaviour {

    [SerializeField]
    private Transform Bullet_prefab;

    public float Firerate = 300.0f;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 60.0f / Firerate)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Quaternion newRotation = transform.rotation;
        newRotation.eulerAngles = new Vector3(0, 30.0f, 0);
        Transform bullet = (Transform)Instantiate(Bullet_prefab, transform.position, newRotation);
    }
}
