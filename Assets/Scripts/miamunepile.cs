using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miamunepile : MonoBehaviour {

	public float petitepile, grossepile;
	AudioSource src;
	public AudioClip sonpetitepile, songrossepile;
	 
	
	// Use this for initialization

    private StaminaManager stam;



	void Start () {
        stam = GetComponent<StaminaManager>();
		src = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter (Collider coll)
	{
		if(coll.gameObject.tag == "petite_pile")
		{
			src.PlayOneShot(sonpetitepile);
			Debug.Log("touche une petite pile");
            stam.currentHealth += petitepile;
			Destroy(coll.gameObject);
		}
		if (coll.gameObject.tag == "grosse_pile")
		{
			src.PlayOneShot(songrossepile);
			stam.currentHealth += grossepile;
			Destroy(coll.gameObject);
		}
	}
}
