using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    [Range(0.1f, 10f)]
    public float speed = 5f;

    private Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(Vector3.left * speed);
        }
    }
}
