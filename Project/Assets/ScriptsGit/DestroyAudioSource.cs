using System;
using UnityEngine;
using System.Collections;

public class DestroyAudioSource : MonoBehaviour
{
    public float Duration { get; set; }

	void Start ()
	{
	    Invoke("Destroy", Duration);        
	}

	void Destroy()
    {
	    Destroy(gameObject);
	}
}
