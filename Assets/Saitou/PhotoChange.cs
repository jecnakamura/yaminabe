using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoChange : MonoBehaviour
{
    private Image img;
    public int count = 1;

    void Start()
    {
        img = GameObject.Find("Image").GetComponent<Image>();
    }

    public void OnClick()
    {
        if (count < 8)
            count++;
        else
            count = 8;

        img.sprite = Resources.Load<Sprite>("Image/ruru" + count.ToString());
    }
}