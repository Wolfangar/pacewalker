using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timerset : MonoBehaviour {
    public Counter counter;
    public Text timerText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        counter.stop = true;
        int min = Mathf.FloorToInt(counter.timer / 60);
        int sec = Mathf.FloorToInt(counter.timer % 60);
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
