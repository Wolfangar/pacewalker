using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miamunepile : MonoBehaviour {

	public float petitepile, grossepile;
	// Use this for initialization

    private StaminaManager stam;

	void Start () {
        stam = GetComponent<StaminaManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "petite_pile")
		{
			Debug.Log("touche une petite pile");
            stam.currentHealth += petitepile;
			Destroy(collision.gameObject);
		}
		if (collision.gameObject.tag == "grosse_pile")
		{
            stam.currentHealth += grossepile;
			Destroy(collision.gameObject);
		}
	}
}
