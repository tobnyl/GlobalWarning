using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
	public void Deactivate()
	{
		gameObject.SetActive(false);
        GetAudioManager().ShieldDown();
		Invoke("Reactivate",3f);
	}

	void Reactivate()
	{
		gameObject.SetActive(true);
        GetAudioManager().ShieldUp();
	}

    private AudioManager GetAudioManager()
    {
        var audioGameObject = GameObject.Find("AudioManager");

        if (audioGameObject != null)
        {
            var audioManager = audioGameObject.GetComponent<AudioManager>();

            return audioManager;
        }

        return null;
    }

}
