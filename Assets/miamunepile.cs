using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miamunepile : MonoBehaviour {

	public float petitepile, grossepile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "petite_pile")
		{
			Debug.Log("touche une petite pile");
			gameObject.GetComponent<StaminaManager>().currentHealth += petitepile;
			Destroy(collision.gameObject);
		}
		if (collision.gameObject.tag == "grosse_pile")
		{
			gameObject.GetComponent<StaminaManager>().currentHealth += grossepile;
			Destroy(collision.gameObject);

		}
	}
}
