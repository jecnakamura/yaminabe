using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{

    public float rotSpeed = 0;
    public float FirstSpeedMin;
    public float FirstSpeedMax;
    public float DecelerationMin;
    public float DecelerationMax;
    public float MinSpeed;
    public float DecelerationFactor = 0.99f;

    public Vector3 RayGoal;
    public Button startButton;
    public Button stopButton;

    private bool isDecelerating = false;
    public enum State
    {
        Standby,
        Rotate,
        Decelerating,
    }
    State state = State.Standby;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        RayGoal = this.gameObject.transform.position;
        RayGoal.y = 2;

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        if (stopButton != null)
        {
            stopButton.onClick.AddListener(OnStopButtonClicked);
        }
    }
    private void OnStartButtonClicked()
    {
        if (state == State.Standby)
        {
            this.rotSpeed = Random.Range(FirstSpeedMin, FirstSpeedMax);
            state = State.Rotate;
        }
    }
    private void OnStopButtonClicked()
    {
        isDecelerating = true;
        state = State.Decelerating;
    }
    // Update is called once per frame
    public void Update()
    {
        switch (state)
        {
            case State.Standby:
                break;

            case State.Rotate:
                transform.Rotate(0, 0, this.rotSpeed);
                break;
            case State.Decelerating:
                if (rotSpeed > MinSpeed)
                {
                    // ������K�p���ĉ�]������
                    rotSpeed *= DecelerationFactor;
                    transform.Rotate(0, 0, this.rotSpeed);
                }
                else
                {
                    // �ŏ����x�ɒB�������~
                    rotSpeed = 0;

                    // �q�b�g��������Č��ʂ�\��
                    var goal = Camera.main.WorldToScreenPoint(RayGoal);
                    Ray ray = Camera.main.ScreenPointToRay(goal);
                    int layerMask = 0xffff;
                    var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 100, layerMask);

                    if (hit.collider)
                    {
                        var piece = hit.collider.gameObject.GetComponent<RoulettePiece>();
                        RouletteResultHandler.SetResult(piece.No);
                        RouletteResultHandler.SetEnd(true);
                        Debug.Log(piece.No);
                    }

                    state = State.Standby;
                }
                break;
        }
    }

}
