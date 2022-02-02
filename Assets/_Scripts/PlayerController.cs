#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _rb;
    private GameManager gameManager;
    private AudioSource _jumpFBX;
    public bool accelerometer;

    private float horizontalInput;
    private float jumpInput;
    private float boundsX = 3f;

    //Salto
    [SerializeField, Range(5f, 100f)]
    private float jumpForce = 10f;
    public bool isOnTheGround;


    public InterstitialAd ad;
    public float timeForShowAdd = 1f;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        _jumpFBX = GetComponent<AudioSource>();
        accelerometer = LoadGame.accelerometer;
    }

    void FixedUpdate()
    {
#if USING_MOBILE

        if (accelerometer)
        {
            float moveForce = 120f;
            Vector3 dir = Vector3.zero;
            dir.x = Input.acceleration.x;

            jumpInput = Input.GetAxis("Mouse Y");

            if (Input.touchCount > 0)
            {
                jumpInput = Input.touches[0].deltaPosition.y;
            }

            if (dir.sqrMagnitude > 1)
            {
                dir.Normalize();
            }
            _rb.AddForce(Vector3.left * dir.x * moveForce * 4 * Time.fixedDeltaTime);
        }
        else
        {
            float moveForce = 20f;
            horizontalInput = Input.GetAxis("Mouse X");
            jumpInput = Input.GetAxis("Mouse Y");

            if (Input.touchCount > 0)
            {
                horizontalInput = Input.touches[0].deltaPosition.x;
                jumpInput = Input.touches[0].deltaPosition.y;
            }

            _rb.AddForce(Vector3.left.normalized * moveForce * horizontalInput * Time.fixedDeltaTime);
        }
#else
        float moveForce = 100f;
        horizontalInput = Input.GetAxis("Horizontal");
        
        jumpInput = Input.GetAxis("Jump");

        _rb.AddForce(Vector3.left.normalized * moveForce * horizontalInput * Time.fixedDeltaTime);
    
#endif

        //En pc modificar el jumpInput a q sea mayor a 0.1;
        //En android a 30
        if (jumpInput > 30f && isOnTheGround)
        {
            _rb.AddForce(transform.localPosition.y * Vector3.up.normalized * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
            isOnTheGround = false;
            _jumpFBX.Play();
        }


    }

    private void Update()
    {
        FrontiersBounds();
    }

    private void FrontiersBounds()
    {
        if (gameObject.transform.position.x >= boundsX)
        {
            this.transform.position = new Vector3(boundsX, transform.position.y, transform.position.z);
        }
        else if (gameObject.transform.position.x <= -boundsX)
        {
            this.transform.position = new Vector3(-boundsX, transform.position.y, transform.position.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
        }
        if (collision.gameObject.CompareTag("Double") || collision.gameObject.CompareTag("Static") || collision.gameObject.CompareTag("Move") || collision.gameObject.CompareTag("Hole"))
        {
            gameManager.gameOver = true;
            this.gameObject.SetActive(false); 
            ad.showAds = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Double") || other.gameObject.CompareTag("Static") || other.gameObject.CompareTag("Move") || other.gameObject.CompareTag("Hole"))
        {
            gameManager.UpdateScore(1);
        }
        else
        {
            gameManager.UpdateScore(10);
        }
    }
}
