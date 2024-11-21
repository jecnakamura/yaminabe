using UnityEngine;

public class MapViewer : MonoBehaviour
{
    public Camera mapCamera; // マップ表示用のカメラ
    public float zoomSpeed = 2f; // ズーム速度
    public float moveSpeed = 5f; // カメラ移動速度

    private bool isMapMode = false; // マップ表示中かどうか

    void Update()
    {
        if (isMapMode)
        {
            HandleMapControls();
        }
    }

    public void EnterMapMode()
    {
        isMapMode = true;
        mapCamera.gameObject.SetActive(true); // マップカメラを有効化
        Time.timeScale = 0; // ゲームの時間を停止（任意）
    }

    public void ExitMapMode()
    {
        isMapMode = false;
        mapCamera.gameObject.SetActive(false); // マップカメラを無効化
        Time.timeScale = 1; // ゲームの時間を再開
    }

    private void HandleMapControls()
    {
        // ズームイン/ズームアウト
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize - scroll * zoomSpeed, 5f, 20f);

        // カメラの移動
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        mapCamera.transform.Translate(new Vector3(moveX, moveY, 0) * moveSpeed * Time.unscaledDeltaTime);
    }
}
