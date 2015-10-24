using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerKills : MonoBehaviour
{

    public Text killsText;
    private int _kills;

	// Use this for initialization
	void Start ()
	{
	    _kills = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UpdateKills()
    {
        _kills += 1;
        HighScore.HiScore = _kills;
        killsText.text = _kills.ToString();
    }
}
