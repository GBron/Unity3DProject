using CustomUtility.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [field:SerializeField] public CsvTable MonsterCSV { get; private set; }
    [field: SerializeField] public CsvDictionary MonsterDic { get; private set; }

    private void Awake() => Init();

    private void Start() => TestMethod();

    private void Init()
    {
        CsvReader.Read(MonsterCSV);
        CsvReader.Read(MonsterDic);
    }

    private void TestMethod()
    {
        Debug.Log(MonsterCSV.GetData(1,1));
    }
}

public enum MonsterData
{
    이름 = 1, 공격력, 방어력, 스피드, 설명
}
