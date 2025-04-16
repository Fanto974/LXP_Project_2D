using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // drag ton joueur ici dans l'inspecteur
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // facultatif : pour d�caler la vue

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
