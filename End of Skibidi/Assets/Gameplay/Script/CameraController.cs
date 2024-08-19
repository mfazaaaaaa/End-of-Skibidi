using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // Referensi ke Transform player dan batas stage
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint; // Titik awal stage (batas kiri)
    [SerializeField] private Transform endPoint;   // Titik akhir stage (batas kanan)

    [SerializeField] private float previewDuration = 5f; // Durasi preview dalam detik

    private bool isPreviewing = true;

    void Start()
    {
        // Mulai coroutine untuk preview stage
        StartCoroutine(PreviewStage());
    }

    void Update()
    {
        if (!isPreviewing)
        {
            // Mengikuti posisi pemain, namun dibatasi oleh startPoint dan endPoint
            float targetX = player.position.x;
            float clampedX = Mathf.Clamp(targetX, startPoint.position.x, endPoint.position.x);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    private IEnumerator PreviewStage()
    {
        float elapsedTime = 0f;

        // Posisi awal dan akhir kamera selama preview
        Vector3 startCameraPosition = new Vector3(startPoint.position.x, transform.position.y, transform.position.z);
        Vector3 endCameraPosition = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);

        while (elapsedTime < previewDuration)
        {
            // Lerp posisi kamera dari start ke end selama previewDuration
            transform.position = Vector3.Lerp(startCameraPosition, endCameraPosition, elapsedTime / previewDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pastikan kamera di posisi akhir setelah selesai lerp
        transform.position = endCameraPosition;

        // Preview selesai, kamera kembali mengikuti player
        isPreviewing = false;
    }
}
