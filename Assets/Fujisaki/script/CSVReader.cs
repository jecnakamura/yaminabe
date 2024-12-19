using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting.FullSerializer;

public class CSVReader : MonoBehaviour
{
    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMaker rMaker;
    private int id;
    TextAsset csvFile; // CSV�t�@�C��
    List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;

    void Start()
    {
        csvFile = Resources.Load("syokuzai") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(csvFile.text);

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }

        // csvDatas[�s][��]���w�肵�Ēl�����R�Ɏ��o����
        // Debug.Log(csvDatas[3][3]);

       
    }
    private void ShowResult(float x)
    {
        for (int i = 1; i <= rMaker.choices.Count; i++)
        {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette))))
            {
                id = rMaker.ID[i - 1];
            }
        }
        // �f�[�^�̕\��
        Debug.Log("ID�F" + csvDatas[id][0] + ", ���O�F" + csvDatas[id][1] + ", �W�������F" + csvDatas[id][2] + ", �����l�F" + csvDatas[id][3]);

    }

    //private void Update()
    //{
    //    ShowResult();
    //}
}
