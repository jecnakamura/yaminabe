using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class siyou : MonoBehaviour
{
    private Image ruleimage;
    public Sprite[] rulesprite;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        ruleimage = GetComponent<Image>();
        ruleimage.sprite = rulesprite[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void R()//1�摜�N���b�N�Ői��
    {
        switch (count)
        {
            case 0:
                if (ruleimage.sprite = rulesprite[0])
                    ruleimage.sprite = rulesprite[1];//2���ڂ̃��[���Љ��ʂ�
                count++;
                break;
            case 1:
                if (ruleimage.sprite = rulesprite[1])
                    ruleimage.sprite = rulesprite[2];//3���ڂ̃��[���Љ��ʂ�
                count++;
                break;
            case 2:
                if (ruleimage.sprite = rulesprite[2])
                    ruleimage.sprite = rulesprite[3];//4���ڂ̃��[���Љ��ʂ�
                count++;
                break;
            case 3:
                if (ruleimage.sprite = rulesprite[3])
                    ruleimage.sprite = rulesprite[4];//5���ڂ̃��[���Љ��ʂ�
                count++;
                break;
        }
    }

    public void L()//1�摜�N���b�N�Ŗ߂�
    {
        switch (count)
        {
            case 0:
                if (ruleimage.sprite = rulesprite[0])
                    ruleimage.sprite = rulesprite[1];//2���ڂ̃��[���Љ��ʂ�
                count--;
                break;
            case 1:
                if (ruleimage.sprite = rulesprite[1])
                    ruleimage.sprite = rulesprite[2];//3���ڂ̃��[���Љ��ʂ�
                count--;
                break;
            case 2:
                if (ruleimage.sprite = rulesprite[2])
                    ruleimage.sprite = rulesprite[3];//4���ڂ̃��[���Љ��ʂ�
                count--;
                break;
            case 3:
                if (ruleimage.sprite = rulesprite[3])
                    ruleimage.sprite = rulesprite[4];//5���ڂ̃��[���Љ��ʂ�
                count--;
                break;
        }
    }
}
