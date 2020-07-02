using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject collector;
    [SerializeField] private float camOffset;

    private void LateUpdate()
    {
        transform.position = new Vector3(collector.transform.position.x + camOffset, transform.position.y, transform.position.z);
    }
}