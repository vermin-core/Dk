using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{  
    //komponenttimuuttujat   
    private Rigidbody2D body;
    private Animator animator;
    // input-muuttujat
    private float horizontalMovement;
    private float verticalMovement;
    //liikkumismuuttujat
    private float moveSpeed = 10f;
    private Vector2 movement = new Vector2();
    //hyppäämismuuttujat
    private float jumpForce = 10f;
    private bool grounded;
    // kiipeämismuuttuja
    private bool canClimb;
    // Start is called before the first frame update
    void Start()
    {
        // haetaan komponetit 
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      horizontalMovement = Input.GetAxisRaw("Horizontal");
      verticalMovement = Input.GetAxisRaw("Vertical");
      // sivuttaisliike
      movement.x = horizontalMovement * moveSpeed;
      if(horizontalMovement > 0)
      {
        transform.localScale = new Vector3(-1, 1, 1);
      }
       if(horizontalMovement < 0)
      {
        transform.localScale = new Vector3(1, 1, 1);
      }
      // hyppääminen
      if (Input.GetButtonDown("Jump") && grounded)
      {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      }
      // kiipeäminen
      if (canClimb && verticalMovement != 0)
      {
        movement.y = verticalMovement * moveSpeed;
        body.isKinematic = true;
      }
      else
      {
        movement.y = 0;
        body.isKinematic = false;
      }
      // animaatiot
      animator.SetFloat("speed", Mathf.Abs(movement.x));  
    }

    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("Princess"))
      {
        Win();
      }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("Ground"))
      {
        grounded = true;
      }
      if (collision.gameObject.CompareTag("Ladder"))
      {
        canClimb = true;
      }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("Ground"))
      {
        grounded = false;
      }
      if (collision.gameObject.CompareTag("Ladder"))
      {
        canClimb = false;
      }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("FireBall"))
      {
        Lose();
      }
    }
    void Lose()
    {
      Debug.Log("You Lose!");
      ResetScene();
    }
    void Win()
    {
      Debug.Log("You Win!");
      ResetScene();
    }
    void ResetScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
