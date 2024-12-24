using Unity.VisualScripting;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;//Vị trí tương đối của camera
    private float speed = 7;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
