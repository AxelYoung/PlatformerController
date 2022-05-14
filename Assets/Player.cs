using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed of movement -irrelevent-
    public float speed;
    // Force to jump -irrelevent-
    public int jumpForce;
    // Layer of ground *IMPORTANT*
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Player movement -irrelevent-
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        // Only add force when key press AND onGround() == true *IMPORTANT*
        if (Input.GetKeyDown(KeyCode.Space) && onGround())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        // Assign variables in our Animation Controller to use there *IMPORTANT*
        anim.SetBool("grounded", onGround());
        anim.SetFloat("vel", rb.velocity.y);
    }

    bool onGround()
    {
        // Overlap circle returns any colliders in a circular area around a point, this point being at the very bottom of our player
        // Bottom of player is found by the center minus half the player height *Normally would hard code this, just an example*
        // Radius is half the size of player in width / x *Normally would hard code this, just an example*
        // And only check for things on groundLayer *IMPORTANT*
        return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2f)), transform.localScale.x / 2f, groundLayer);
    }

    // OBJECTS

    // All ground sprites have BoxCollider2D, AND are assigned to the Ground Layer *IMPORTANT*
    // Player has BoxCollider2D, Rigidbody2D (slightly edited to taste, no effect on this), this script, and a frictionless physics material to avoid wall drag

    // ANIMATION

    // There are three animations in this example, I'd say 3-4 is fairly traditional, could be more or less though
    // Jump is what happens when the player presses the button and follows through until peak of height
    // Falling happens after peak of height whilst moving down
    // Land happens after falling when touching the ground

    // Jump in this case DOES NOT LOOP, as it includes the anticipation attributed to pressing the button, could change this and seperate into two animations, one that doesn't loop (anticipation) and one that does (going up until peak height)
    // Falling DOES LOOP, animation repeats itself until transition into land
    // Landing DOES NOT MOVE, and currently it will not cut itself off, only transitioning into idle or another jump after it's finished, could be changed easily

    // In technical terms
    // Jump happens whenever was grounded and then is not grounded
    // Falling happens whenever is not grounded and going down (velocity.y < 0)
    // And landing happens whenever is falling and is grounded

    // EDITOR

    // Only one thing here, it IS IMPORTANT, go to Build Settings > Player Settings > Physics2D, UNCHECK "Queries start in collider"
    // IF you don't do this the Overlap circle can return the collider of the player, as it is by default not ignored
}
