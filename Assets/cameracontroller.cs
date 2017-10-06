using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour {

	public GameObject controller;
	public bool camtrigger;
	public float offsety;
	public float xblock;


	// Use this for initialization
	void Start () {

		camtrigger = controller.GetComponent<characontroller>().camtrigger;
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector2 (controller.transform.position.x, controller.transform.position.y + offsety) ;


		if (camtrigger == true){


					gameObject.transform.position = new Vector2 (xblock, controller.transform.position.y + offsety) ;


		}

	}

	
}
