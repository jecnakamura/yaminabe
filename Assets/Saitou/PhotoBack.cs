using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoBack : MonoBehaviour
{

    private Image img;

    void Start()
    {
        img = GameObject.Find("Image").GetComponent<Image>();
    }

    public void OnClick()
    {
        PhotoChange change;
        GameObject obj = GameObject.Find("R");
        change = obj.GetComponent<PhotoChange>();

        if (change.count > 1)
            change.count--;
        else
            change.count = 1;

        img.sprite = Resources.Load<Sprite>("Image/ruru" + change.count.ToString());
    }
}
