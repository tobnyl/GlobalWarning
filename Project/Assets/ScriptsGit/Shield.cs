using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
	public void Deactivate()
	{
		gameObject.SetActive(false);
        AudioManager.Instance.ShieldDown();
		Invoke("Reactivate",3f);
	}

	void Reactivate()
	{
		gameObject.SetActive(true);
        AudioManager.Instance.ShieldUp();
	}

}
