using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashbutton : MonoBehaviour
{

	public GameObject controller;
	public bool recover;
	Image img;
	characontroller3D ctrl;

	// Use this for initialization
	void Start()
	{


		ctrl = controller.GetComponent<characontroller3D>();
		img = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		recover = ctrl.recover;

		if (recover == true)
		{
			//Debug.Log("Je montre l'image");
			img.enabled = true;
		}
		else
		{
			//Debug.Log("Je cache l'image");
			img.enabled = false;
		}

	}
}
