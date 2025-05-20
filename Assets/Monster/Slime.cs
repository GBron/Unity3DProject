using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public DataManager Data;
    public string MonsterName;
    public int Id;

    [SerializeField] private string _name;
    [SerializeField] private int _atk;
    [SerializeField] private int _def;
    [SerializeField] private int _spd;
    [SerializeField] private string _dsc;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Init(MonsterName);
            Debug.Log($"이름 : {_name} 공격력 : {_atk} 방어력 : {_def} 스피드 : {_spd} 설명 : {_dsc}");
        }
    }


    void Init(string name)
    {
        _name = Data.MonsterDic.GetData(name, "이름");
        _atk = int.Parse(Data.MonsterDic.GetData(name, "공격력"));
        _def = int.Parse(Data.MonsterDic.GetData(name, "방어력"));
        _spd = int.Parse(Data.MonsterDic.GetData(name, "스피드"));
        _dsc = Data.MonsterDic.GetData(name, "설명");

        // _name = Data.MonsterCSV.GetData(num, (int)MonsterData.이름);
        // _atk = int.Parse(Data.MonsterCSV.GetData(num, (int)MonsterData.공격력));
        // _def = int.Parse(Data.MonsterCSV.GetData(num, (int)MonsterData.방어력));
        // _spd = int.Parse(Data.MonsterCSV.GetData(num, (int)MonsterData.스피드));
        // _dsc = Data.MonsterCSV.GetData(num, (int)MonsterData.설명);
    }
}
