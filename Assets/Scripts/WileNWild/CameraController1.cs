using UnityEngine;

public class CameraController1 : MonoBehaviour
{

    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + offset;
        transform.rotation = Quaternion.LookRotation(playerPos.position - transform.position);
    }
}
