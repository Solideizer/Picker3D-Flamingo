using UnityEngine;

public class CameraController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject collector;
    [SerializeField] private float camOffset;
#pragma warning restore 0649
    
    private void LateUpdate()
    {
        transform.position = new Vector3(
            collector.transform.position.x + camOffset, transform.position.y, transform.position.z);
    }
}