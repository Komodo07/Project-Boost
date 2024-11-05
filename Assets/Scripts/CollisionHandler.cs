using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":            
                Debug.Log("Collided into Friendly object");
                break;
            
            case "Finish":            
                Debug.Log("Collided into Finish object");
                break;
            
            case "Fuel":            
                Debug.Log("Collided into Fuel object");
                break;
            
            default:            
                Debug.Log("Player destroyed");
                break;
            
        }
    }
}
