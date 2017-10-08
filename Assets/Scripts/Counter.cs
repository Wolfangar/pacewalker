﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

	public float timer;
	public int killedcounter;

    public Text timerText;

	// Use this for initialization
	void Start () {
		
	}

    public bool stop = false;
	
	// Update is called once per frame
	void Update () {
        if (stop)
            return;

        timer += Time.deltaTime;


        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }


}
