using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * 20 * Time.deltaTime);
        //if(this.transform.position.x > 50) {
        //    Destroy(this.gameObject);
        //}
        //else if (this.transform.position.y > 100)
        //{
        //   Destroy(this.gameObject);
        //}
         if (this.transform.position.z < 100)
        {
            Destroy(this.gameObject);
        }

    }
}
