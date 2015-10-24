using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float RotationSpeed = 1f;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = 0;

        transform.LookAt(worldMousePosition, transform.up);
    }
}
