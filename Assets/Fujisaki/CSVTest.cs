using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVTest : MonoBehaviour
{
    public TestData[] testData;
    // Start is called before the first frame update
    void Start()
    {
        //�@�e�L�X�g�t�@�C���̓ǂݍ��݂��s���Ă����N���X
        TextAsset textasset = new TextAsset();
        //�@��قǗp�ӂ���csv�t�@�C����ǂݍ��܂���B
        //�@�t�@�C���́uResources�v�t�H���_�����A�����ɓ���Ă������ƁB�܂�"CSVTestData"�̕����̓t�@�C�����ɍ��킹�ĕύX����B
        textasset = Resources.Load("syokuzai", typeof(TextAsset)) as TextAsset;
        //�@CSVSerializer��p����csv�t�@�C����z��ɗ������ށB
        testData = CSVSerializer.Deserialize<TestData>(textasset.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
