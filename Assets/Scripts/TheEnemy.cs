using UnityEngine;
using System.Collections;

public class TheEnemy : MonoBehaviour {

    [SerializeField]
    private NavMeshAgent NMA;

    public int HP = 1;
    public Animator animator;


	[SerializeField] 
	public Transform fireballPrefab;

	public int firetimes=0;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if(HP <= 0)
        {
            NMA.enabled = false;
            GetComponent<Collider>().enabled = false;
            animator.SetTrigger("Dead");
            Destroy(gameObject,1.8f);
        }
    }

    void Update()
    {

        if (GameManager.GameOver)
        {
            Destroy(gameObject);
        }
        NMA.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        if (!NMA.pathPending && NMA.enabled)
        {
            if (NMA.remainingDistance <= 4.5f)
            {
                animator.SetTrigger("Attack");
            }

            if (NMA.remainingDistance <= NMA.stoppingDistance)
            {
                if (!NMA.hasPath || NMA.velocity.sqrMagnitude == 0f)
                {
                    GameManager.LIFE--;
                    Destroy(gameObject);
                }
            }
        }


		Ray ray = new Ray(transform.position,transform.forward);//向前面发射一条射线
		RaycastHit hit;
		if(Physics.SphereCast(ray,0.5f,out hit))
		{
			GameObject hitObject = hit.transform.gameObject;
			if(hitObject.layer==8)//检测是否时玩家
			{
				if (firetimes <= 0) {
					Object fireball = Instantiate (fireballPrefab, transform.position, transform.rotation);
					firetimes += 1;
					Debug.Log(firetimes);
					Debug.Log("hit");
				}


			}

		}

        
    }
    
}
