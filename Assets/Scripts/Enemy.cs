using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float speed, attackSpeed, hitdmg, selfdmg;
    [HideInInspector]
	public bool hit, presence;
	Animator anim;
	StaminaManager herostam;
    StaminaManager selfstam;
    private Rigidbody2D rigidBody;
	public GameObject hero;
	public AudioClip hitsound, attacksound;
	AudioSource src;
    //private float initScaleX;

    private NavMeshAgent navMesh;

    bool canAttack = true;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        selfstam = GetComponent<StaminaManager>();
        herostam = hero.GetComponent<StaminaManager>();
        navMesh = GetComponent<NavMeshAgent>();
		src = GetComponent<AudioSource>();
        navMesh.updateRotation = false;

        //initScaleX = transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
        movement();
        if(hit)
        {
            checkHit();
        }
    }

    private void movement()
    {
        Vector2 direction = (hero.transform.position - transform.position).normalized;
        //transform.position += direction * speed * Time.deltaTime;
        //rigidBody.AddForce(direction * speed * Time.deltaTime);
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2);
        /*
        RaycastHit2D hit = Physics2D.Raycast(newPos, direction, 2);
        Debug.DrawRay(newPos, direction * 2, Color.green);
        if (hit.collider != null)
        {
            if(Random.Range(0, 2) == 0)
            {
                //rigidBody.AddForce(Vector3.up * 10);
                //direction += Vector2.up * 10;
            }
        }
    */
        if(navMesh.isActiveAndEnabled)
            navMesh.SetDestination(hero.transform.position);
        
        //Debug.Log("velocity.normalized.x "+ GetComponent<NavMeshAgent>().velocity.normalized.x);
        if (canReverse && navMesh.velocity.normalized.sqrMagnitude != 0 && canAttack)
        {
            canReverse = false;
            int flip = (navMesh.velocity.normalized).x > 0 ? -1 : 1;
            transform.localScale = new Vector3(flip, transform.localScale.y, transform.localScale.z);
            StartCoroutine(reverseTimer());
        }
    }

    bool canReverse = true;

    IEnumerator reverseTimer()
    {
        yield return new WaitForSeconds(0.2f);
        canReverse = true;
    }

    private void checkHit()
    {
        if(presence && !hasAttacked)
        {
            hasAttacked = true;
            Debug.Log("damage");
            herostam.loseHealth(hitdmg);
			src.PlayOneShot(hitsound);
        }
    }

    bool hasAttacked = false;

	// comportement attaque
	private void OnTriggerStay(Collider collision)
	{
        if (collision.gameObject == this || selfstam.isDead)
            return;

        if (collision.gameObject.tag == "charac")
        {
            presence = true;
        }
        else
        {
            return;
        }

        if (!canAttack)
            return;
        

        canAttack = false;

        anim.SetBool("attack", true);
	
        if(navMesh.isActiveAndEnabled)
            navMesh.isStopped = true;
        navMesh.velocity = Vector3.zero;
		
		if (presence == true)//if cac is true par exemple a la place
		{
			attackcac();
		}
	
	}

	private void OnTriggerExit(Collider collision)
	{
        if (collision.gameObject == this)
            return;

        if (collision.gameObject.tag == "charac")
        {
            anim.SetBool("attack", false);
            presence = false;
        }
	}
	//Carac d'attaque
	void attackcac()
	{
        hasAttacked = false;

        selfstam.loseHealth(selfdmg);

        StartCoroutine(attackTimer());
	}

	public void playattacksound()
	{
		src.PlayOneShot(attacksound);

	}


	IEnumerator attackTimer()
	{
		yield return new WaitForSeconds(attackSpeed);
        if(navMesh.isActiveAndEnabled)
            navMesh.isStopped = false;
        canAttack = true;
        Debug.Log("attack over");
    }

	void FixedUpdate()
    {
        /*

        Vector2 le = new Vector2(0, 0.0f);
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2) + le;


        Vector2 direction = (target.transform.position - transform.position).normalized;

        
        rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);

        
        RaycastHit2D hit = Physics2D.Raycast(newPos, direction, 2.0f);
        Debug.DrawRay(newPos, direction * 2, Color.green);

        if (hit.collider != null)
        {
            Debug.Log("hit");
            //rigidBody.AddForce(Vector3.up * 10);
            Vector2 vv;
            vv.x = direction.y;
            vv.y = -direction.x;

            Vector2 direction2 = ((Vector2) target.transform.position - newPos).normalized;

            float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
            int leftRight = angle > 0 ? -1 : 1;

            Debug.DrawRay(newPos, vv * 2, Color.blue);
            Debug.DrawRay(newPos, leftRight * vv * 2, Color.red);
            //rigidBody.AddForce(leftRight * vv * 20 * Mathf.Pow((2 - hit.distance), 2));
            //rigidBody.AddForce(-direction * 10 * Mathf.Pow((2 - hit.distance), 2));



            //rigidBody.AddForce((new Vector3(hit.point.x, hit.point.y, transform.position.z) - transform.position).normalized * -20 * (2 - hit.distance));
            //rigidBody.AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0) * -10 * (2 - hit.distance));
        }
        */
    }
}
