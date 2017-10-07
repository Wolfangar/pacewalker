using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characontroller3D : MonoBehaviour {

	public bool camtrigger;
	public float xblock;
	public float speed;
	public float dash;
	public float dashspeed;
	public float xmove;
	public float ymove;
	public bool recover;

    private Rigidbody rigidBody;

    protected Vector3 dashTarget;
    protected float dashSpeed = 20;
    public float dashDistance = 3;

    // Use this for initialization
    void Start () {
		camtrigger = false;
		dash = 0;
		recover = true;

        rigidBody = GetComponent<Rigidbody>();
    }


    private bool isDashing = false;

    private void Dash()
    {
        Vector3 direction = velocity.normalized;

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

    private Vector3 velocity;

    private void Update()
    {
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            Dash();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isDashing)
        {
            rigidBody.velocity = Vector2.zero;

            float distSqr = (dashTarget - transform.position).sqrMagnitude;
            if (distSqr < 0.1f)
            {
                isDashing = false;
                dashTarget = Vector2.zero;
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
		
		yield return new WaitForSeconds(0.5f);
		recover = true;
	
	}

}
