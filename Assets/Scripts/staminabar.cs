using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminabar : MonoBehaviour {

	public float barstamina;
	public GameObject controller;
	float maxstamina;
	StaminaManager staminacur;
	RectTransform rect;
	public AudioClip alert;
	AudioSource src;
	bool notalert = true;

	// Use this for initialization
	void Start () {
		staminacur = controller.GetComponent<StaminaManager>();
		rect = GetComponent<RectTransform>();
		maxstamina = rect.sizeDelta.y;
		src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		barstamina = staminacur.currentHealth / staminacur.totalHealth;
		rect.sizeDelta = new Vector2 (rect.sizeDelta.x, maxstamina*barstamina);
		Debug.Log("result" + rect.sizeDelta.y);
	
		
	}
}
