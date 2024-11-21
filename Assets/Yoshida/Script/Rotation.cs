using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotation : MonoBehaviour
{

    public float rotSpeed = 0;
    public float FirstSpeedMin;
    public float FirstSpeedMax;
    public float DecelerationMin;
    public float DecelerationMax;
    public float MinSpeed;

    public Vector3 RayGoal; 

    public enum State
    {
        Standby,
        Rotate,
    }
    State state = State.Standby;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        RayGoal = this.gameObject.transform.position;
        RayGoal.y = 2;
    }

    // Update is called once per frame
    public void Update()
    {
        switch(state)
        {
            case State.Standby:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.rotSpeed = Random.Range(FirstSpeedMin, FirstSpeedMax);
                    state = State.Rotate;
                }
                break;

            case State.Rotate:
                this.rotSpeed *= Random.Range(DecelerationMin, DecelerationMax);
                transform.Rotate(0, 0, this.rotSpeed);
                if (rotSpeed < MinSpeed)
                {
                    rotSpeed = 0;

                    var goal = Camera.main.WorldToScreenPoint(RayGoal);
                    Ray ray = Camera.main.ScreenPointToRay(goal);
                    int layerMask = 0xffff;
                    var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 100, layerMask);

                    if (hit.collider)
                    {
                        var piece = hit.collider.gameObject.GetComponent<RoulettePiece>();
                        ReceiveTimes(piece.No);
                        Debug.Log(piece.No);
                    }
                    state = State.Standby;
                }
                break;
        }
    }

    public int ReceiveTimes(int inp)
    {
        return inp;
    }

}
