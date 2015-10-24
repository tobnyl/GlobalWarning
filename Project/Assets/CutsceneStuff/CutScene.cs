using UnityEngine;
using System.Collections;

public class CutScene : MonoBehaviour 
{
	public GameObject[] ScreenShot;
	public int index;
	//public int turn;

	// Use this for initialization
	void Start () {
		index = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if(index!=8)
			{
				StartCoroutine("Fade");
			} 
			else 
			{
				Application.LoadLevel("dummyscene");
			}

		}
	
	}

	IEnumerator Fade() {
		for (float f = 1f; f >= 0; f -= 0.1f) {
			ScreenShot [index].GetComponent<SpriteRenderer> ().color = new Color(1,1,1,f);
			yield return null;
		}
		ScreenShot [index].GetComponent<SpriteRenderer> ().color = new Color(1,1,1,0);
		index ++;
	}
}



