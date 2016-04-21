using UnityEngine;
using System.Collections;

public class TestFindway : MonoBehaviour {

    private Seeker seek;
	// Use this for initialization
	void Start ()
    {
        seek = GetComponent<Seeker>();
        seek.StartPath(transform.position,new Vector3(50,0,100));
	}
	void Update () {
	
	}
}
