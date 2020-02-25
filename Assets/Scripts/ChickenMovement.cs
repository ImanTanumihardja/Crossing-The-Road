using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChickenMovement : MonoBehaviour
{
    private const float Rotation = -10f;
    private bool isMoving = false;
    private Vector3 currentDir = Vector3.zero;
    private Rigidbody rb;
    private bool jetpackOn = false;

    // Public vars
    public LevelManager levelManager;
    public Harness Harness;
    //public float CurrentTime;

    public JetPack JetPack
    {
        get { return this.Harness.JetPack; }
    }
    
    // This function is run when the game is first played
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    // This function is run every frame
    void Update()
    {
        if (this.transform.position.y <= 26)
        {
            this.rb.useGravity = false;
            rb.velocity += new Vector3(0, 1f, 0);
        }
        else
        {
            this.rb.useGravity = true;
        }

        if (rb.velocity != Vector3.zero)
        {
            if(rb.velocity.x > 0)
            {
                rb.velocity += new Vector3(-0.2f, 0 , 0);
            }
            if (rb.velocity.y > 0)
            {
                rb.velocity += new Vector3(0, -0.2f, 0);
            }
            if (rb.velocity.z > 0)
            {
                rb.velocity += new Vector3(0, 0, -0.2f);
            }
        }
        /** MOVEMENT **/

        // Jet pack
        if (Input.GetKeyDown(KeyCode.Space) && Harness.JetPack.Fuel >= 0)
        {
            Harness.JetPack.Trigger(true);
            jetpackOn = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Harness.JetPack.Trigger(false);
            jetpackOn = false;
        }

        if (Harness.JetPack.Velocity != Vector3.zero)
        {
            rb.velocity += Harness.JetPack.Velocity;
        }

        //Forwards
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)))
            {
                Move(new Vector3(0, 0, Harness.JetPack.Velocity.z + 1));
            }
        //Backwards
        if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)))
            {
            Move(new Vector3(0, 0, Harness.JetPack.Velocity.z - 1));
        }
            //Left
            if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
            {
            Move(new Vector3(Harness.JetPack.Velocity.z - 1, 0, 0));
        }
            //Right 
            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
            {
                Move(new Vector3(Harness.JetPack.Velocity.z + 1, 0, 0));
            }

            // Tilt the player GO based on currentDir
            Tilt();

           
        
        // Reset isMoving and currentDir variables after each Update cycle.
        isMoving = false;
        
        currentDir = Vector3.zero;
    }

    public void Move (Vector3 dir)
    {
        isMoving = true;
        currentDir += dir;

        if (jetpackOn)
        {
            rb.velocity += dir *  Harness.JetPack.Power;
            dir = dir * (Time.deltaTime * Harness.JetPack.Step);
            transform.Translate(dir, Space.World);
        }
        else
        {
            dir = dir * (Time.deltaTime * Harness.JetPack.Step);
            transform.Translate(dir, Space.World);
        }
        
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
    
    //void OnCollisionEnter(Collision other)
    //{
    //    // if (other is bridge)
    //    if (other.gameObject.CompareTag("Bridge"))
    //    {
    //        Harness.JetPack.Refuel(true);
    //    }
    //}

    //void OnCollisionExit(Collision other)
    //{
    //    Harness.JetPack.Refuel(false);
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AddPoint"))
        {

            GameObject level = other.transform.parent.parent.gameObject;
            GameObject vs = level.transform.Find("VehicleSpawner").gameObject;
            Destroy(vs, Random.Range(5, 11));

            levelManager.ScoreUp();
            levelManager.moveBarrier(other);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    public void setJetpackOn(bool isJetpackOn)
    {
        jetpackOn = isJetpackOn;
    }
}
