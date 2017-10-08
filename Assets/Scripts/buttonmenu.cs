using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonmenu : MonoBehaviour {

	AudioSource src;
	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickbutton()
	{

		src.Play();
	}
}
