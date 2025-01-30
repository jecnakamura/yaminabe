using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10.0f)]
    private float move = 2.0f;

    private bool cameraMoveActive = false;

    private Transform camTransform;
    //private Vector2 presentCamPos;

    private bool _uiMessageActiv;

    private GUIStyle style;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = this.gameObject.transform;
        style = new GUIStyle();
        style.fontSize = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //CamControlIsActive();

        if(cameraMoveActive)
        {
            CameraPositionKeyControl();
        }
    }

    public void OnClick()
    {
        cameraMoveActive = !cameraMoveActive;
        if (_uiMessageActiv == false)
        {
            StartCoroutine(DisplayUiMessage());
        }
    }

    private void CameraPositionKeyControl()
    {
        Vector3 campos = camTransform.position;

        if (Input.GetKey(KeyCode.RightArrow)) { campos += camTransform.right * Time.deltaTime * move; }
        if (Input.GetKey(KeyCode.LeftArrow)) { campos -= camTransform.right * Time.deltaTime * move; }
        if (Input.GetKey(KeyCode.UpArrow)) { campos += camTransform.forward * Time.deltaTime * move; }
        if (Input.GetKey(KeyCode.DownArrow)) { campos -= camTransform.forward * Time.deltaTime * move; }

        camTransform.position = campos;
    }

    private IEnumerator DisplayUiMessage()
    {
        _uiMessageActiv = true;
        float time = 0;
        while (time < 2)
        {
            time = time + Time.deltaTime;
            yield return null;
        }
        _uiMessageActiv = false;
    }

    void OnGUI()
    {
        if (cameraMoveActive == true)
        {
            GUI.Label(new Rect(Screen.width / 4-50, Screen.height - 1000, 100, 20), "十字キーで視点の移動",style);
        }

        if (cameraMoveActive == false)
        {
            GUI.Label(new Rect(Screen.width / 4-50, Screen.height - 1000, 100, 20), "aaaaaaaaa",style);
        }
    }
}
