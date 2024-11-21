using UnityEngine;

public class MapViewer : MonoBehaviour
{
    public Camera mapCamera; // �}�b�v�\���p�̃J����
    public float zoomSpeed = 2f; // �Y�[�����x
    public float moveSpeed = 5f; // �J�����ړ����x

    private bool isMapMode = false; // �}�b�v�\�������ǂ���

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
        mapCamera.gameObject.SetActive(true); // �}�b�v�J������L����
        Time.timeScale = 0; // �Q�[���̎��Ԃ��~�i�C�Ӂj
    }

    public void ExitMapMode()
    {
        isMapMode = false;
        mapCamera.gameObject.SetActive(false); // �}�b�v�J�����𖳌���
        Time.timeScale = 1; // �Q�[���̎��Ԃ��ĊJ
    }

    private void HandleMapControls()
    {
        // �Y�[���C��/�Y�[���A�E�g
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize - scroll * zoomSpeed, 5f, 20f);

        // �J�����̈ړ�
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        mapCamera.transform.Translate(new Vector3(moveX, moveY, 0) * moveSpeed * Time.unscaledDeltaTime);
    }
}
