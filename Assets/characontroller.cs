using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characontroller : MonoBehaviour {

	public bool camtrigger;
	public float xblock;
	// Use this for initialization
	void Start () {
		camtrigger = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
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

}
