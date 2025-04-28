using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] GameObject playerObj;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] float moveAcceleration = 5000;
    [SerializeField] float maxMoveSpeed = 8;
    [SerializeField] float jumpForce = 100;
    [SerializeField] float gravityForce = 10;
    [SerializeField] playerGraphic1 graphics;
    bool grounded = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        //playerObj.transform.Translate(Vector3.up * yInput * Time.deltaTime);
        if (MathF.Abs(rb.linearVelocity.x) < maxMoveSpeed) {
            rb.AddRelativeForce(Vector3.right * xInput * Time.deltaTime * moveAcceleration);
            graphics.lookDirection(xInput);
            animator.SetFloat("Speed", Math.Abs(xInput * 2));
        }

        if (Input.GetButtonDown("Jump") && grounded) {
            rb.AddRelativeForce(Vector3.up * jumpForce);
        }

        if (!grounded) {
            rb.AddRelativeForce(Vector3.down * gravityForce * Time.deltaTime);
        }
    }

	private void OnCollisionStay(Collision collision) {
        grounded = true;
	}

	private void OnCollisionExit(Collision collision) {
		grounded = false;
	}
}