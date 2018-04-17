using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour {
    public GameObject ForeGround;
    Image backGround;
	// Use this for initialization
	void Start () {
        backGround = ForeGround.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void rotate() {

        this.transform.Rotate(new Vector3(0, 0, 180));
        backGround.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));

    }
}
