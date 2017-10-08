using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characontroller3D : MonoBehaviour {

    [HideInInspector]
	public bool camtrigger = false;
    [HideInInspector]
    public float xblock;
    
    private Rigidbody rigidBody;
    private Vector3 dashTarget;
    private bool recover = true;
    private bool isDashing = false;

    public float speed;
    public float dashSpeed = 20;
    public float dashDistance = 3;
    public float recoverTime = 0.5f;
    public float dashDmg;
	public AudioClip dashsound1, dashsound2, dashsound3;
	[HideInInspector]
	AudioClip dash;
	AudioClip[] dashsounds;


	AudioSource src;
	Animator anim;
    StaminaManager stam;

	// Use this for initialization
	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		stam = GetComponent<StaminaManager>();
		src = GetComponent<AudioSource>();
		dashsounds = new AudioClip[] {
		dashsound1, dashsound2, dashsound3 };
	

}

private Vector3 lastVelo;

    private void Dash()
    {
        Vector3 direction = velocity.normalized;

        //if (direction.sqrMagnitude == 0)//dash a l'arret
           // direction = lastVelo.normalized;

        isDashing = true;

        Debug.Log("direction: " + direction);
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        Physics.Raycast(ray, out hit, dashDistance);
        Debug.DrawRay(transform.position, direction * dashDistance, Color.green, 5.0f);
        if (hit.collider != null)
        {
            Debug.Log("hit" + hit.distance);
            float distanceReal = Vector3.Distance(transform.position, hit.point);
            dashTarget = transform.position + (Vector3)((direction * dashDistance)
                * ((distanceReal - 0.3f) / dashDistance) );
        }
        else
        {
            dashTarget = transform.position + (Vector3)(direction * dashDistance);
        }
        rigidBody.velocity = Vector3.zero;
        velocity = Vector3.zero;
    }

    private Vector3 velocity = new Vector3(1, 0, 0);

    private void Update()
    {
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;
        if (velocity.sqrMagnitude != 0)
            lastVelo = velocity;

        if (velocity.x != 0)
		{
			//anim.SetBool("Movehoriz", true);
			anim.SetBool("Move", true);

		}
	
		if (velocity.z != 0)
		{
			anim.SetBool("Movevert", true);
			anim.SetBool("Move", true);

		}
		if (velocity == new Vector3 (0,0,0))
		{
			anim.SetBool("Movehoriz", false);
			anim.SetBool("Movevert", false);
			anim.SetBool("Move", false);

		}
		if (Input.GetKeyDown(KeyCode.Space) && !isDashing && recover)
        {
			src.PlayOneShot(dash = dashsounds[Random.Range(0,dashsounds.Length)]);
			anim.SetBool("Dash", true);
            stam.loseHealth(dashDmg);
            Dash();
        }

		if (velocity.x > 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (velocity.x < 0)
		{
            transform.localScale = new Vector3(1, 1, 1);
		}
    }

    void endDashing()
    {
        anim.SetBool("Dash", false);
        isDashing = false;
        recover = false;
        StartCoroutine(Wait());
        dashTarget = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isDashing)
        {
            rigidBody.velocity = Vector2.zero;

            float distSqr = (dashTarget - transform.position).sqrMagnitude;
            if (distSqr < 0.1f)
            {
                endDashing();
            }
            else
            {
                Debug.Log("dashing");
                rigidBody.MovePosition(Vector3.Lerp(transform.position, dashTarget, dashSpeed * Time.deltaTime));
            }
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
            //Debug.Log("force is there" + velocity);
            rigidBody.AddForce(velocity, ForceMode.VelocityChange);
        }

        /*
        if (!recover)//isDashing
        {

        }
        else
        {

        }

		if (Input.GetKeyDown("space") && recover == true)
		{
			recover = false;
			dash = dashspeed;
			xmove = Input.GetAxisRaw("Horizontal");
			ymove = Input.GetAxisRaw("Vertical");


            //rigidBody.velocity = new Vector3(xmove, 0.0f, ymove).normalized * dashspeed;
            rigidBody.MovePosition(transform.position + new Vector3(xmove, 0.0f, ymove).normalized * dashspeed);
            //rigidBody.AddForce(new Vector3(xmove, 0.0f, ymove).normalized * 50, ForceMode.VelocityChange);

            StartCoroutine(Wait());

            return;
        }
		else
			{
			dash = 0;
			xmove = Input.GetAxisRaw("Horizontal");
			ymove = Input.GetAxisRaw("Vertical");
		}
        Vector3 newDirection = new Vector3(xmove, 0.0f, ymove).normalized * speed * Time.deltaTime;

        transform.Translate(newDirection);
        //rigidBody.MovePosition(transform.position + newDirection);
        */
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		camtrigger = true;
		xblock = gameObject.transform.position.x;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		camtrigger = false;
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(recoverTime);
		recover = true;
	}

    private void OnCollisionStay(Collision collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Obstacle"))
        {
            endDashing();
            Debug.Log("touch obstacle");
            Vector2 dir = collision.transform.position - transform.position;
            rigidBody.velocity = dir.normalized * -15;
        }
    }
}
