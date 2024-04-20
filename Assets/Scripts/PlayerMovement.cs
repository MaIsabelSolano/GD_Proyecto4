using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Rigidbodies and colliders

    Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D cc2D_Ffion;
    [SerializeField] CapsuleCollider2D cc2D_Phoenix;

    private List<CapsuleCollider2D> capsuleColliders = new List<CapsuleCollider2D>();
    private int currentCharacter = 0;

    [SerializeField] GameObject Ffion;
    [SerializeField] GameObject Phoenix;

    #endregion

    #region parameters

    private bool isGrounded = false;
    public float jumpSpeed = 2000f;
    [SerializeField] float movementSpeed;

    #endregion

    #region animations
    private float playerInput = 0;
    public Animator AnimatorF;
    public Animator AnimatorP;

    // walking

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Ffion.SetActive(false);
        Phoenix.SetActive(true);

        capsuleColliders.Add(cc2D_Phoenix);
        capsuleColliders.Add(cc2D_Ffion);

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // character swapping
        ChangeCharacter();


        // movement
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
            if(!isGrounded){
                // rigidBoddies[currentCharacter].gravityScale = 2;
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            if(isGrounded){
                rb.AddForce(Vector2.up * jumpSpeed);
                isGrounded = false;
            }
        }

        // Animations
        playerInput = Input.GetAxisRaw("Horizontal");
        SwapSprite();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Floor") {
            isGrounded = true;
        }
    }


    public void ChangeCharacter() {
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            if (!Ffion.activeSelf) {
                Phoenix.SetActive(false);
                Ffion.SetActive(true);

                currentCharacter = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            if (!Phoenix.activeSelf) {
                Ffion.SetActive(false);
                Phoenix.SetActive(true);

                currentCharacter = 0;
            }
        }
    }

    void SwapSprite()
    {
        // Right
        if (playerInput > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Left
        else if (playerInput < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    // private void FixedUpdate() {
    //     rigidBoddies[currentCharacter].MovePosition(rigidBoddies[currentCharacter].position + movement * speed_ * Time.deltaTime);
    // }
}
