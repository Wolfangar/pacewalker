using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject target;
    public float speed;

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 direction = (target.transform.position - transform.position).normalized;
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
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;


        

        rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
        Vector2 le = new Vector2(0, 0.2f);
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2) + le;
        RaycastHit2D hit = Physics2D.Raycast(newPos, direction, 2);
        Debug.DrawRay(newPos, direction * 2, Color.green);
        if (hit.collider != null)
        {
            Debug.Log("hit");
            //rigidBody.AddForce(Vector3.up * 10);
            Vector2 vv;
            vv.x = direction.y;
            vv.y = -direction.x;

            Debug.DrawRay(newPos, vv * 2, Color.blue);
            if (hit.collider != null)

                rigidBody.AddForce(vv * 20 * (2 - hit.distance));
            //rigidBody.AddForce((new Vector3(hit.point.x, hit.point.y, transform.position.z) - transform.position).normalized * -20 * (2 - hit.distance));
            //rigidBody.AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0) * -10 * (2 - hit.distance));
        }
    }
}
