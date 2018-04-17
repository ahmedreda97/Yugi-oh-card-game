using UnityEngine;
using System.Collections;

public class MouseTracking : MonoBehaviour {

    Transform MousePostion;
    Ray ray;
	// Use this for initialization
	void Start () {
        MousePostion = transform;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        MousePostion.position = ray.origin;    
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        MousePostion.position = ray.origin;
        //Debug.Log(MousePostion.position);
	}
}
