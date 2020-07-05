using UnityEngine;
using System.Collections;

public class TheEnemy : MonoBehaviour {

    [SerializeField]
    private NavMeshAgent NMA;

    public float HP = 1.0f;
    public Animator animator;


	[SerializeField] 
	public Transform fireballPrefab;

	public int firetimes=0;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        Vector3 direct2player = GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position;
        direct2player = direct2player.normalized;
        Vector3 ballPos = new Vector3(transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.z);
        Object fireball = Instantiate(fireballPrefab, ballPos, Quaternion.LookRotation(direct2player));

    }

    public void TakeDamage(float dmg)
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
        if (NMA.enabled)
        {
            NMA.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
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

    }
    
}
