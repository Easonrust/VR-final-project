using UnityEngine;
using System.Collections;

public class LesserSpawner : MonoBehaviour {

    [SerializeField]
    private LineRenderer LR;

    public float time = 0.5f;
    private float timer = 0;

    private bool foo = false;

	void Start () {
        if(LR == null)
        {
            LR = GetComponent<LineRenderer>();
        }
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    void Update()
    {
        if (foo)
        {
            timer += Time.deltaTime;
            if (timer >= time * 0.4f)
            {
                timer = 0;
                LR.enabled = false;
                foo = false;
            }
        }
    }

    public void Shoot(Vector3 position)
    {
        LR.enabled = true;
        LR.SetPosition(0, transform.position);
        LR.SetPosition(1, position);
        foo = true;
    }
}
