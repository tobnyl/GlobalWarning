using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 100;
    public Text HpText;
    public Color FlashColor;
    public int NumberFlashes = 2;

    private int _currentHealth;
    private Vector3 _startPosition;

	// Use this for initialization
	void Start ()
	{
	    _startPosition = Camera.main.transform.position;

	    if (HpText == null)
	    {
	        Debug.LogError("Error: HpText not assigned");
	    }

        _currentHealth = StartingHealth;
        UpdateHpUiText(_currentHealth);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        UpdateHpUiText(_currentHealth);
        StartCoroutine(Flash());
        StartCoroutine(ShakeScreen());

        if (_currentHealth <= 0)
        {
            Application.LoadLevel("gameOverScreen");
        }

        Debug.Log(_currentHealth);
    }

    private void UpdateHpUiText(int health)
    {
        HpText.text = _currentHealth.ToString();
    }

    private IEnumerator ShakeScreen()
    {
        int numberShakes = 10;

        for (int i = 0; i < numberShakes; i++)
        {
            float minRange = -0.1f;
            float maxRange = 0.2f;
            var randomVector = new Vector3(Random.Range(minRange, maxRange), Random.Range(minRange, maxRange), 0);
            Camera.main.transform.position = _startPosition + randomVector;
            yield return new WaitForSeconds(0.1f);
        }

        Camera.main.transform.position = _startPosition;
    }

    private IEnumerator Flash()
    {
        var numberSecondsToWait = 0.05f;
        var enemySpriteRenderer = transform.parent.parent.gameObject.GetComponentInChildren<SpriteRenderer>(); // VERY GREAT CODE...

        for (var i = 0; i < NumberFlashes; i++)
        {
            enemySpriteRenderer.material.color = FlashColor;
            yield return new WaitForSeconds(numberSecondsToWait);
            enemySpriteRenderer.material.color = Color.white;
            yield return new WaitForSeconds(numberSecondsToWait);
        }

    }
}
