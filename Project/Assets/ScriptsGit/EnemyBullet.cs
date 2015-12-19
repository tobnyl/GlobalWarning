using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour 
{
	float speed = 0f;
	GameObject player;
	Vector3 volleyTarget;

	delegate void BulletBehaviour();
	BulletBehaviour bulletBehaviour;

	public void Initialize(GameObject target)
	{
		speed = 7.5f;
		transform.LookAt (target.transform.position);
		gameObject.SetActive(true);
		bulletBehaviour += Movement;
		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
		sprite.transform.rotation = Quaternion.identity;
	}

	public void VolleyInitialize(Vector3 target)
	{
		speed = 5f;
		gameObject.SetActive(true);
		transform.LookAt (target);
		bulletBehaviour += Movement;
		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
		sprite.transform.rotation = Quaternion.identity;
	}

	void Update()
	{
		bulletBehaviour();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (screenPos.x > Screen.width || screenPos.x < 0 || screenPos.y > Screen.height || screenPos.y < 0)
        {
            Destroy(gameObject);
        }

    }

	void Movement()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}


	void OnCollisionEnter2D(Collision2D co)
	{
		switch (co.gameObject.tag)
		{
			case "Player":
			{
			    Debug.Log ("Player got hit in 2D!");

                PlayerHealth playerHealth = co.gameObject.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(20);

                AudioManager.Instance.PlayerHit();

                Destroy (this.gameObject);
                break;
			}
            case "Shield":
            {
                transform.Rotate(Random.Range(160, 200), 0, 0);
                this.gameObject.tag = "PlayerBullet";
                gameObject.AddComponent<PlayerBullet>().DamageAmount = 50;
                gameObject.GetComponent<PlayerBullet>().speed = 10;
                gameObject.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                Destroy(GetComponent<EnemyBullet>());
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;

                AudioManager.Instance.ShieldBlock();

                break;
            }
            case "Cannon":
			{
                Debug.Log ("Cannon got hit in 2D!");
                Destroy (this.gameObject);

                AudioManager.Instance.PlayerHit();

                var cannon = co.gameObject.GetComponent<Cannon>();
                cannon.TakeDamage();

                break;
			}		
		}
	}
}
