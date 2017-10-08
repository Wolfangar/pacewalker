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
	// Use this for initialization
	void Start () {
		staminacur = controller.GetComponent<StaminaManager>();
		rect = GetComponent<RectTransform>();
		maxstamina = rect.sizeDelta.y;
	}
	
	// Update is called once per frame
	void Update () {
		
		barstamina = staminacur.currentHealth / staminacur.totalHealth;
		rect.sizeDelta = new Vector2 (rect.sizeDelta.x, maxstamina*barstamina);
		Debug.Log("result" + rect.sizeDelta.y);
	
		
	}
}
