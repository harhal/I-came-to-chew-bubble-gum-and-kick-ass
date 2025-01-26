using Core;
using UnityEngine;

public class CameraRubber : MonoBehaviour
{
    [SerializeField] 
    private float smoothSpeed = 0.125f;
    
    private void Update()
    {
        var targetPosition = The.Me.transform.position;
            
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
