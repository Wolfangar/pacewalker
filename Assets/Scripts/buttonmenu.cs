using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonmenu : MonoBehaviour {

	AudioSource[] src;
	// Use this for initialization
	void Start () {
		src = transform.parent.GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void levelThisLovingMachine()
    {
        src[0].Play();
        SceneManager.LoadScene("final");
    }

    public void exitThisShit()
    {
        src[0].Play();
        Application.Quit();
    }
    
}
