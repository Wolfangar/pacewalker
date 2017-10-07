using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap : MonoBehaviour {

    SpriteRenderer sr;

    public float backgroundZ = 18;

    // Use this for initialization
    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        sr.sortingOrder = (int) ((Mathf.Abs(backgroundZ - transform.position.z)) * 100);
    }
}
