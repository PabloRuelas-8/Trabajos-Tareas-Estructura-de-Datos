using UnityEngine;

public class CameraManager : MonoBehaviour
{ 
    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 0, -2); 
    public float smoothSpeed = 0.250f;     

    void LateUpdate()
    {
        if (objetivo == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
                objetivo = player.transform;
            else
                return;
        }   

        Vector3 desiredPosition = objetivo.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }


}
