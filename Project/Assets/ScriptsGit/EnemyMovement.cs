using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    private float lifeTime = 20;
	public GameObject bulletPrefab;
	public GameObject volleyPrefab;
	GameObject dummy; //Player
	float speed = 0.05f;
	float coolDown = 1f;
	Vector3 tempV;
	float number = 0;
	public int StartingHealth = 100;
	private int _currentHealth;

    public Sprite alienSprite;
    public Sprite meleeSprite;

    delegate void EnemyBehaviour();
	EnemyBehaviour enemyBehaviour;

    delegate IEnumerator FiringPattern();
    FiringPattern firingPattern;

    private AudioSource _audioSource;

    public Color FlashColor;
    public int NumberFlashes = 2;
    public Vector3 direction;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        enemyBehaviour();
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        // Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        /* if (screenPos.x > Screen.width + 250f || screenPos.x < -250f || screenPos.y > Screen.height || screenPos.y < 0)
        {
            Destroy(gameObject);
        }*/
    }

    public void Initialize(int a)
    {
        _currentHealth = StartingHealth;
        int speedMod = 1; //Speed modifier for later use
        dummy = GameObject.FindGameObjectWithTag("Player");
        speed = speed * speedMod;
        direction = Vector3.left * a;


        int i = Random.Range(0, 2);
        if (i == 0)
        {
            enemyBehaviour += WavyMove;
            //enemyBehaviour += ShootHoming;
            enemyBehaviour += ShootRandom;
            int j = Random.Range(0, 3);
            switch (j)
            {
                case 0:
                    firingPattern += TracerFire;
                    break;

                case 1:
                    firingPattern += VolleyFire;
                    break;
                case 2:
                    firingPattern += SpiralFire;
                    break;
            }
        }
        else
        {
            enemyBehaviour += HomingMove;
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = meleeSprite;
            //enemyBehaviour += ShootHoming;
        }

    }


    void HomingMove()
	{
		transform.position = Vector2.MoveTowards (transform.position, dummy.transform.position, speed); //Seeker, homes in on player
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(-10,0),0.01f);
	}

    void WavyMove()
    {
        number++;
        tempV.y = (5f * Mathf.Sin(number * 0.1f * Mathf.PI / 15) + 1f);
        transform.Translate(direction * 0.05f);
        transform.position = new Vector3(transform.position.x, tempV.y);
    }



    void ShootHoming()
	{
		//Fires bullets, very dangerous
		coolDown -= Time.deltaTime;
		if (coolDown <= 0) 
		{
			coolDown = 1f;
			GameObject bullet = Instantiate (bulletPrefab,transform.position,Quaternion.identity) as GameObject;
			bullet.GetComponent<EnemyBullet>().Initialize(dummy);

            AudioManager.Instance.EnemyShoot();
        }
	}

	void ShootRandom()
	{
		coolDown -= Time.deltaTime;
		if (coolDown <= 0) 
		{
			coolDown = 5f;
            StartCoroutine(firingPattern());
		}
	}

    IEnumerator VolleyFire()
    {
        int i = 12;
        while (i > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<EnemyBullet>().VolleyInitialize(dummy.transform.position + new Vector3((Random.Range(-5, 6)), Random.Range(-5, 6)));
            AudioManager.Instance.EnemyShoot();
            i--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator NovaFire()
    {
        int i = 3;
        int j = 36;
        while (i > 0)
        {
            while (j > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<EnemyBullet>().VolleyInitialize(dummy.transform.position + new Vector3(i, 0));
                bullet.gameObject.transform.Rotate(10 * j, 0, 0);
                j--;
            }

            AudioManager.Instance.EnemyShoot();

            i--;
            j = 36;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator TracerFire()
    {
        int i = 6;
        int j = 36;
        while (i > 0)
        {
            while (j > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<EnemyBullet>().VolleyInitialize(dummy.transform.position + new Vector3(i, 0));
                bullet.gameObject.transform.Rotate(10 * j, 0, 0);
                j--;
            }

            AudioManager.Instance.EnemyShoot();

            i--;
            j = 36;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator SpiralFire()
    {
        int i = 500;
        int j = 1;
        while (i > 0)
        {
            while (j > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<EnemyBullet>().VolleyInitialize(dummy.transform.position + new Vector3(i, 0));
                bullet.gameObject.transform.Rotate(5 * i, 0, 0);
                j--;
            }
            i--;
            j = 1;
            yield return new WaitForSeconds(0.001f);
        }
    }

    void OnCollisionEnter2D(Collision2D co)
	{
		switch (co.gameObject.tag) {
		    case "Player":
		    {
			    //Debug.Log ("Player got hit in 2D!");
			    Destroy (this.gameObject);

		        var playerHealth = co.gameObject.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(10);

                AudioManager.Instance.PlayerHit();

                break;
		    }
            case "Shield":
            {
                Destroy(this.gameObject);
                co.gameObject.GetComponent<Shield>().Deactivate();
                break;
            }
            case "Cannon":
		    {
			    //Debug.Log ("Cannon got hit in 2D!");
			    Destroy (this.gameObject);

                AudioManager.Instance.PlayerHit();

                var cannon = co.gameObject.GetComponent<Cannon>();
                cannon.TakeDamage();

                break;
		    }
		    case "PlayerBullet":
		    {
			    //Debug.Log ("Player hit enemy");
			
			    PlayerBullet playerBullet = co.gameObject.GetComponent<PlayerBullet> ();
			    TakeDamage (playerBullet.DamageAmount);

		        StartCoroutine(Flash());

			    Destroy (co.gameObject);
			    break;
		    }
		}
	}

    private IEnumerator Flash()
    {
        //var numberOfFlashes = 2;
        var numberSecondsToWait = 0.05f;
        var enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        for (var i = 0; i < NumberFlashes; i++)
        {
            enemySpriteRenderer.material.color = FlashColor;
            yield return new WaitForSeconds(numberSecondsToWait);
            enemySpriteRenderer.material.color = Color.white;
            yield return new WaitForSeconds(numberSecondsToWait);
        }

    }

    public void TakeDamage(int amount)
	{
		_currentHealth -= amount;
		
		if (_currentHealth <= 0)
		{
		    var playerKills = Camera.main.GetComponent<PlayerKills>();
            playerKills.UpdateKills();

            AudioManager.Instance.EnemyDies();

            Destroy(gameObject);
		    
		}
	}

}
