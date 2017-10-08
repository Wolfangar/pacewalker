using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setinactive : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void inactive()
	{
		gameObject.transform.parent.gameObject.SetActive(false);
	}
}
