using UnityEngine;

public class EnemyModelFixer : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
