using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChickenMovement : MonoBehaviour
{
    private const float Rotation = -10f;
    private bool isMoving = false;
    private Vector3 currentDir = Vector3.zero;
    private Rigidbody rb;

    // Public vars
    public LevelManager LevelManager;
    public Harness Harness;
    //public float CurrentTime;

    public JetPack JetPack
    {
        get { return this.Harness.JetPack; }
    }
    
    // This function is run when the game is first played
    void Start()
    {
        this.LevelManager = GameObject.FindObjectOfType<LevelManager>();
        this.rb = this.GetComponent<Rigidbody>();
    }

    // This function is run every frame
    void Update()
    {
        /** MOVEMENT **/
        //Forwards
        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0, 0, 1));
        }
        //Backwards
        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0, 0, -1));
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-1, 0, 0));
        }
        //Right 
        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(1, 0, 0));
        }

        // Title the player GO based on currentDir
        Tilt();

        // Jet pack
        if (Input.GetKeyDown(KeyCode.Space) && Harness.JetPack.Fuel >= 0)
        {
            Harness.JetPack.Trigger(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Harness.JetPack.Trigger(false);
        }

        if (Harness.JetPack.Velocity != Vector3.zero)
        {
            rb.velocity = Harness.JetPack.Velocity;
        }

        // Reset isMoving and currentDir variables after each Update cycle.
        isMoving = false;
        currentDir = Vector3.zero;
    }

    public void Move (Vector3 dir)
    {
        isMoving = true;
        currentDir += dir;

        dir = dir * (Time.deltaTime * Harness.JetPack.SideStep);
        transform.Translate(dir, Space.World);
    }

    public void Stop()
    {
        isMoving = false;
    }

    /// <summary>
    /// Title the player GO based on the current direction it is moving.
    /// </summary>
    public void Tilt()
    {
        if (isMoving)
        {
            float x = Rotation * currentDir.normalized.x;
            float z = Rotation * currentDir.normalized.z;

            GetComponent<Transform>().eulerAngles = new Vector3(x, -90, z);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        // if (other is bridge)
        if (other.gameObject.CompareTag("Bridge"))
        {
            Harness.JetPack.Refuel(true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        Harness.JetPack.Refuel(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AddPoint"))
        {
            LevelManager.ScoreUp();
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
