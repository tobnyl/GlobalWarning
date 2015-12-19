using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CannonType
{
	Normal = 50,
	Super = 100
};
public class Cannon : MonoBehaviour
{
	public CannonType type;
    public GameObject BulletPrefab;
	public float FireRate = 2;
    public int DisabledTime = 1;
    
	private float _lastFire;

    private AudioSource _audioSource;

    private int _startingHealth;
    private int _currentHealth;

    private SpriteRenderer _spriteRenderer;

    private List<Color> colors;
    private Color _startColor;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _startColor = _spriteRenderer.material.color;

        var alpha = 150/255f;

        colors = new List<Color>();
        colors.Add(new Color(1.0f, 0, 0, alpha));
        colors.Add(new Color(1.0f, 0.647f, 0, alpha));
        colors.Add(new Color(1.0f, 1.0f, 0, alpha));

        _startingHealth = 3;
        _currentHealth = _startingHealth;
        _audioSource = GetComponent<AudioSource>();
        
        if (BulletPrefab == null)
        {
            Debug.LogError("Error: BulletPrefab not assigned");
        }
    }

    void Update()
    {
        Shoot();
    }

	private void Shoot()
	{
		if (Input.GetButton("Fire1") && Time.time > _lastFire)
		{
		    AudioManager.Instance.PlayerShoot();


            _lastFire = Time.time + FireRate;
			
			GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation) as GameObject;
			bullet.GetComponent<PlayerBullet>().DamageAmount = (int)type;
		}
	}

    public void TakeDamage()
    {
        _currentHealth -= 1;

        if (_currentHealth >= 0)
        {
            _spriteRenderer.material.color = colors[_currentHealth];
        }

        if (_currentHealth < 0)
        {
            gameObject.SetActive(false);
            Invoke("EnableCannon", DisabledTime);

        }

        Debug.Log("Cannon: " + _currentHealth);
    }

    void EnableCannon()
    {
        gameObject.SetActive(true);
        _currentHealth = _startingHealth;
        _spriteRenderer.material.color = _startColor;
    }
}
