using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private bool clicking;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float clickSpeed;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clicking = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            clicking = false;
        }
        if(clicking)
        {
            rb.velocity = Vector3.up*-clickSpeed;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("pat");
        if(clicking)
        {
            Debug.Log("klik");
            if(other.collider.gameObject.CompareTag("Bad"))
            {
                print(other.gameObject.tag);
                SceneManager.LoadScene(0);
            }
            else if(other.collider.gameObject.CompareTag("Good"))
            {
                print(other.gameObject.tag);
                Destroy(other.collider.transform.parent.gameObject);
            }
        }
        else
        {
            rb.velocity = Vector3.up*jumpSpeed;
        }
        
    }
}
