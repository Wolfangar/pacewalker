using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piston : MonoBehaviour {

	public GameObject ctrl;
	characontroller3D ctrlscript;
	Animator anim;
	public AudioClip recoversound;
	AudioSource src;
	// Use this for initialization
	void Start () {
		ctrlscript = ctrl.GetComponent<characontroller3D>();
		anim = GetComponent<Animator>();
		src = GetComponent<AudioSource>();
		//anim["piston"].speed = ctrlscript.recoverTime;
		//anim["piston"].wrapMode = WrapMode.Once;


	}

	// Update is called once per frame
	void Update () {
		anim.SetBool("Dash", ctrlscript.isDashing);
	}

	public void playrecoversound()
	{
		src.PlayOneShot(recoversound);
	}
}
