using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpType{
    Arrow,
    Slingshot
}

public class PlayerScript : MonoBehaviour {

    public bool isGrounded;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayer;

    [Header("Stats")]
    public JumpType jumpType;
    public float jumpStrength;

    Rigidbody2D rb;

    // Jump variables
    Vector2 _startPos;
    Vector2 _direction;
    bool isChronoJumping; // bool is true if chronojumping -> used for collision with destructibles
    bool jumpHoldBool; // check if touch input has been pressed in air
    bool chronoJumpBool; // check if player has reached chronoJump count limit
    bool isDirectionChosenBool;

    // Debugging variables
    int numberingVar;

    void Awake()
    {
        #if UNITY_ANDROID
        Debug.Log("phone");
        
        #endif
    }
	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
		
	}

    // Update is called once per frame
    void Update() {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (isGrounded)
        {

            // Reset everything
            Time.timeScale = 1;
            jumpHoldBool = false;
            chronoJumpBool = false;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();
            }

            //else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    Jump();
            //}
        }
        
        else
        {
            //if (Input.touchCount > 0)
            //{
            //    Touch touch = Input.GetTouch(0);

            //    switch (touch.phase)
            //    {

            //        // Recording intial touch position
            //        case TouchPhase.Began:
            //            _startPos = touch.position;
            //            isDirectionChosenBool = false;
            //            break;

            //        // Determine direction by comparing the current touch & initial touch
            //        case TouchPhase.Moved:
            //            _direction= touch.position - _startPos;
            //            break;

            //        // Report the direction has been chosen when finger is lifted
            //        case TouchPhase.Ended:
            //            isDirectionChosenBool = true;
            //            break;
            //    }

            //}

            // ChronoJump only when touch input received + if has not jumped or jump reset

            if (Input.GetMouseButtonDown(0) && !jumpHoldBool && !chronoJumpBool)
            {

                if (jumpType == JumpType.Slingshot)
                {
                    // Store position in temp variable on input
                    Touch touch = Input.GetTouch(0);
                    _startPos = touch.position;

                }

                else
                {

                }

                // Slow time and mark bool true
                Time.timeScale = 0.1f;
                jumpHoldBool = true;

                Debug.Log("Stop Time");

            }

            // When in midair, if touch input is lost, ChronoJump()
            // reset everything

            if (Input.GetMouseButtonUp(0) && jumpHoldBool)
            {

                // Store position of lost input
                Touch touch = Input.GetTouch(0);
                Vector2 _endPos = touch.position;

                // Calculate direction + force from start and end position
                _direction = _startPos - _endPos;
                _direction = _direction.normalized;
                ChronoJump();

                // Reset Time + bool
                Time.timeScale = 1f;
                jumpHoldBool = false;
                chronoJumpBool = true;
                Debug.Log("Start Time");

            }

        }

        if (isDirectionChosenBool)
        {
            Debug.Log(_startPos);
        }


    }

    // -------------------- ABILITIES / FUNCTIONS -----------------------

    void Jump()
    {
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        numberingVar++;
        Debug.Log(numberingVar + " + jump");
    }

    void ChronoJump()
    {
        isChronoJumping = true;

        // reset velocity
        rb.velocity = Vector2.zero;
        rb.AddForce(_direction * jumpStrength, ForceMode2D.Impulse);
    }

    // ------------------------ INTERACTIONS -----------------------------
    // 
    //
    // ------------------  (A) With Destructibles  -----------------------
    //


    // If hit, allow player to chronojump again

    public void CanJumpAgain()
    {
        chronoJumpBool = false;
    }

    // Destructible check if player is chronojumping

    public bool GetChronoJumpBool()
    {
        return isChronoJumping;
    } 
}
