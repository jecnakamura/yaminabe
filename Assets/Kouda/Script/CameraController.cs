using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 現在追従するプレイヤー
    public float followSpeed = 5f;
    public float zoomSpeed = 2f;

    private bool isMapView = false; // マップ全体表示モード
    private Vector3 mapCenter; // マップ中心
    private float mapZoom = 10f; // マップ全体表示時のズーム値
    private float defaultZoom = 5f; // 通常モード時のズーム値

    void Update()
    {
        if (isMapView)
        {
            HandleMapView(); // マップ全体表示モード
        }
        
    }

    public void FollowPlayer(Player player)
    {
        target = player.transform;
        isMapView = false;
        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, defaultZoom, zoomSpeed * Time.deltaTime);
    }

   

    public void ToggleMapView(Vector3 mapCenter)
    {
        this.mapCenter = mapCenter;
        isMapView = !isMapView;
    }

    private void HandleMapView()
    {
        Vector3 mapViewPosition = new Vector3(mapCenter.x, mapCenter.y, -10f);
        transform.position = Vector3.Lerp(transform.position, mapViewPosition, followSpeed * Time.deltaTime);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, mapZoom, zoomSpeed * Time.deltaTime);

        // 矢印キーでスクロール
        float moveSpeed = 5f;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
