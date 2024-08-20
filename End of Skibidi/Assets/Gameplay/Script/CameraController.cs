using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // Referensi ke Transform player dan batas stage
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint; // Titik awal stage (batas kiri)
    [SerializeField] private Transform endPoint;   // Titik akhir stage (batas kanan)

    [SerializeField] private float previewDuration = 3f; // Durasi preview dalam detik
    [SerializeField] private float pauseDuration = 1f;   // Durasi berhenti di endPoint dalam detik
    [SerializeField] private float moveToPlayerDuration = 2f; // Durasi bergerak ke pemain dalam detik

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

        // Lerp posisi kamera dari start ke end selama previewDuration
        while (elapsedTime < previewDuration)
        {
            transform.position = Vector3.Lerp(startCameraPosition, endCameraPosition, elapsedTime / previewDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pastikan kamera di posisi akhir setelah selesai lerp
        transform.position = endCameraPosition;

        // Berhenti sejenak di endPoint
        yield return new WaitForSeconds(pauseDuration);

        // Mulai Lerp dari endPoint ke pemain
        elapsedTime = 0f;
        Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

        while (elapsedTime < moveToPlayerDuration)
        {
            transform.position = Vector3.Lerp(endCameraPosition, playerPosition, elapsedTime / moveToPlayerDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pastikan kamera tepat di posisi pemain setelah selesai lerp
        transform.position = playerPosition;

        // Preview selesai, kamera kembali mengikuti player
        isPreviewing = false;
    }
}
