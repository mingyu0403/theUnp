using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour {

	public Slider healthBarSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (healthBarSlider.value <= 0) {
			
		}
	}

	public void playerhit(float damage){
		healthBarSlider.value += -damage;
	}
}
