using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveBall : MonoBehaviour
{

    public Text score;
    private int points = 0;
    public float speed;
    private bool onGround = true;
    public Rigidbody rb;
     
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Space) && onGround == true)
        {
            Vector3 jump = new Vector3(moveHorizontal, 500.0f, moveVertical);
            rb.AddForce(jump);
            onGround = false;
        }

        if (rb.position.y < -10)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collect")
        {
            points++;
            score.text = "Score: " + points;
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "End")
        {
            FindObjectOfType<GameManager>().EndGame();
            other.gameObject.SetActive(false);
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        onGround = true;
    }
}
