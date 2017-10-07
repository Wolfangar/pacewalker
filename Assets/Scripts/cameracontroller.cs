using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour {

	public GameObject controller;
	public bool camtrigger;
	public float offsety;
	public float xblock;
	public float ycam;
	public float camspeed;



	// Use this for initialization
	void Start () {

		camtrigger = controller.GetComponent<characontroller>().camtrigger;
		gameObject.transform.position = gameObject.transform.position + new Vector3(0, offsety, 0);


	}

	// Update is called once per frame
	void Update () {

		Vector3 screenPos = Camera.main.WorldToViewportPoint(controller.transform.position);


		if (screenPos.x > 0.55)
		{

			gameObject.transform.position = new Vector3(gameObject.transform.position.x + camspeed , offsety, ycam);

		}
		if ( screenPos.x < 0.45)
		{

			gameObject.transform.position = new Vector3(gameObject.transform.position.x - camspeed, offsety, ycam);

		}

		if (camtrigger == true){

			gameObject.transform.position = new Vector3 (xblock,offsety, ycam) ;

		}

	}

	
}
