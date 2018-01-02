using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour {

    bool facingRight = true;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float jumpForce = 400f;

    [Range(0, 1)]
    [SerializeField] float crouchSpeed = .36f;

    [SerializeField] bool airControl = false;
    [SerializeField] LayerMask whatIsGround;

    Transform groundCheck;
    float groundedRadius = .2f;
    bool grounded = false;
    Transform ceilingCheck;
    float ceilingRadius = .01f;
    Animator anim;
    Rigidbody2D rigidbody2D;

    bool doubleJump = false;

	// Use this for initialization
	void Awake () {
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        if (grounded)
            doubleJump = false;
	}

    public void Move(float move, bool crouch, bool jump) {
        if(!crouch && anim.GetBool("crouch")) {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
                crouch = true;
        }

        //anim.SetBool("Crouch", crouch);

        if (grounded || airControl) {
            move = (crouch ? move * crouchSpeed : move);
            anim.SetFloat("Speed", Mathf.Abs(move));
            rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();
        }

        if ((grounded || !doubleJump) && jump) {
            anim.SetBool("Ground", false);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            if (!grounded)
                doubleJump = true;
        }
    }

    void Flip () {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}