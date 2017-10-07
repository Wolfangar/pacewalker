using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminabar : MonoBehaviour {

	public float barstamina;
	public GameObject controller;
	StaminaManager staminacur;
	RectTransform rect;
	// Use this for initialization
	void Start () {
		staminacur = controller.GetComponent<StaminaManager>();
		rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
		barstamina = staminacur.currentHealth;
		rect.sizeDelta = new Vector2 (barstamina / 2, rect.sizeDelta.y);
	
		
	}
}
