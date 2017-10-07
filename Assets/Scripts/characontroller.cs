using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characontroller : MonoBehaviour {

	public bool camtrigger;
	public float xblock;
	public float speed;
	public float dash;
	public float dashspeed;
	public float xmove;
	public float ymove;
	public bool recover;

	// Use this for initialization
	void Start () {
		camtrigger = false;
		dash = 0;
		recover = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") && recover == true)
		{
			recover = false;
			dash = dashspeed;
			xmove = Input.GetAxis("Horizontal") * speed * dash;
			ymove = Input.GetAxis("Vertical") * speed * dash;
			StartCoroutine(Wait());
		}
		else
			{
			dash = 0;
			xmove = Input.GetAxis("Horizontal") * speed;
			ymove = Input.GetAxis("Vertical") * speed;
		}
		transform.Translate(xmove, ymove, 0);

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
		
		yield return new WaitForSeconds(3.0f);
		recover = true;
	
	}

}
