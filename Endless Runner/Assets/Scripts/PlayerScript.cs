using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10f;
    private bool enableJump = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && enableJump == true)
        {
            rb.AddForce(Vector2.up * 700f);
            enableJump = false;
        }

        Vector3 clampedPosition = transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -8f, 8f);
        // re-assigning the transform's position will clamp it
        transform.position = clampedPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Tags.GROUND_TAG)
        {
            enableJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tags.POINT_TAG)
        {
            GameManager.Instance.IncreaseScore(1);
            Destroy(collision.gameObject);
        }
    }
}
