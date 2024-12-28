using UnityEngine;
using System.Collections;

public class RoomRotator : MonoBehaviour
{
    public float rotationAngle = 90f;
    private bool isRotating = false; 

    public void RotateRoom()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateRoomCoroutine());
        }
    }

    private IEnumerator RotateRoomCoroutine()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));

        float duration = 3f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
