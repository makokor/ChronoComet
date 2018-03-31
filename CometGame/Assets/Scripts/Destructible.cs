using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    [SerializeField] bool canJumpAgain;
    Rigidbody2D rb;

    float min = -1;
    float max = 1;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(min, max), Random.Range(min, max)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            if (canJumpAgain)
            {
                PlayerScript playerScript = collider.gameObject.GetComponent<PlayerScript>();

                // Check if player is chronojumping before 
                if (playerScript.GetChronoJumpBool())
                {
                    playerScript.CanJumpAgain();
                }
            }

            Destroy(gameObject);
        }
    }
}
