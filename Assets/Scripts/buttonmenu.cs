using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonmenu : MonoBehaviour {

	AudioSource src;
	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void levelThisLovingMachine()
    {
        SceneManager.LoadScene("Level");
    }

    public void exitThisShit()
    {
        Application.Quit();
    }

	public void clickbutton()
	{

		src.Play();
	}
}
