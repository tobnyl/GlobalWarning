using System;
using UnityEngine;
using System.Collections;

public class Headache : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	    Invoke("BadThings", 3f);

	}
	
	// Update is called once per frame
	void BadThings()
    {
	    Destroy(gameObject);
	}
}
