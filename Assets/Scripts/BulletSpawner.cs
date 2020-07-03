using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

    [SerializeField]
    private Transform Bullet_prefab;

    public float Firerate = 300.0f;
    private float timer = 0;

	void Update () {
        timer += Time.deltaTime;

        if(timer >= 60.0f / Firerate)
        {
            timer = 0;
            Shoot();
        }
	}

    void Shoot()
    {
        Transform bullet = (Transform)Instantiate(Bullet_prefab, transform.position, transform.rotation);

    }
}
