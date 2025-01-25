using Core;
using UnityEngine;

public class CameraRubber : MonoBehaviour
{
    private void Update()
    {
        var targetPosition = The.Me.transform.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }
}
