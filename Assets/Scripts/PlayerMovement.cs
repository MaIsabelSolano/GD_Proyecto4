using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Rigidbodies and colliders

    Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D cc2D_Ffion;
    [SerializeField] CapsuleCollider2D cc2D_Phoenix;

    private List<CapsuleCollider2D> capsuleColliders = new List<CapsuleCollider2D>();
    public int currentCharacter = 0;

    [SerializeField] GameObject Ffion;
    [SerializeField] GameObject Phoenix;

    #endregion

    #region parameters
    [Header("Parameters")]
    private bool isGrounded = false;
    public float jumpSpeed = 2000f;
    [SerializeField] float movementSpeed;

    public float life = 100f;
    public float mana = 3f;

    public float flyForce = 100f;

    #endregion

    #region animations
    private float playerInput = 0;
    public Animator AnimatorF;
    public Animator AnimatorP;

    private float animationMovement;

    #endregion

    private Vector3 lastPosition;

    private bool hasShield = false;

    private float timeBtwAttack;
    public float startTimeAttack;


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
        if (Time.timeScale > 0) 
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
            
            if (currentCharacter == 0) {
                AnimatorP.SetFloat("SpeedP", Input.GetAxisRaw("Horizontal"));
            }
            else if (currentCharacter == 1) {
                // Ffion
                AnimatorF.SetFloat("Speed", Input.GetAxisRaw("Horizontal"));
            }

            // Special moves
            if (Input.GetKey(KeyCode.Space)) {
                if (mana > 0) {
                    mana -= Time.deltaTime;
                    if (currentCharacter == 0) {
                        // Phoenix fly
                        rb.AddForce(Vector2.up * flyForce);              

                    } else if (currentCharacter == 1) {
                        // Ffion
                        hasShield = true;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                hasShield = false;
            }

            // attack
            if (timeBtwAttack <= 0) {
                if (Input.GetKey(KeyCode.F)) {}
            } else {
                timeBtwAttack -= 0;
            }


            Debug.Log("is grounded: " + isGrounded.ToString());
            Debug.Log("has shield " + hasShield.ToString());
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Floor") {
            isGrounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") {
            isGrounded = false;
            lastPosition = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "lowerBound") {
            life = life - 10;
            transform.position = lastPosition;
        }

        if (collider.gameObject.tag == "mana") {
            mana = 3;
            Destroy(collider.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!hasShield) {
            if (collision.gameObject.tag == "Ray") {
                life = life - 1f;
            }
        }
    }

  

    public void ChangeCharacter() {
        if (isGrounded) {
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
    }

    private void SwapSprite() {
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

}
