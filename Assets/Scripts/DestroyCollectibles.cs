using UnityEngine;

public class DestroyCollectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 1f);
    }
}