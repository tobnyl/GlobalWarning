using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    public float speed = 5f;
    public LayerMask mask;
    //public float maxDistance = 100;

    Rigidbody2D _RigidBody;
    Vector3 fixedSpeed;
    //Vector3 distanceTravelled = Vector3.zero;
    //float maxDistanceSqr;
	private int damageAmount = 0;
	
	public int DamageAmount 
	{
		get { return damageAmount; }
		set { damageAmount = value; }
	}


    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        //maxDistanceSqr = maxDistance * maxDistance;
		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
		sprite.transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        fixedSpeed = Vector3.forward * speed * Time.deltaTime;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(_RigidBody.position);

        if (screenPos.x > Screen.width || screenPos.x < 0 || screenPos.y > Screen.height || screenPos.y < 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(fixedSpeed);
    }

    void OnCollisionEnter2D(Collision2D co)
    {
        // TODO: check collision with enemies
        //if (c.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //{
        //    Destroy(gameObject);
        //}
    }
}
