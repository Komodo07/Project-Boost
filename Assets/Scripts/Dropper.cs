using UnityEngine;

public class Dropper : MonoBehaviour
{
    Rigidbody rb;
    Animator shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shake = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        shake.enabled = true;
    }

    public void EnableGravity()
    {
        rb.useGravity = true;
    }
    
}
