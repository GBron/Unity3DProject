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
            Debug.Log($"�̸� : {_name} ���ݷ� : {_atk} ���� : {_def} ���ǵ� : {_spd} ���� : {_dsc}");
        }
    }


    void Init(string name)
    {
        _name = Data.MonsterDic.GetData(name, "�̸�");
        _atk = int.Parse(Data.MonsterDic.GetData(name, "���ݷ�"));
        _def = int.Parse(Data.MonsterDic.GetData(name, "����"));
        _spd = int.Parse(Data.MonsterDic.GetData(name, "���ǵ�"));
        _dsc = Data.MonsterDic.GetData(name, "����");

        // _name = Data.MonsterCSV.GetData(num, (int)MonsterData.�̸�);
        // _atk = int.Parse(Data.MonsterCSV.GetData(num, (int)MonsterData.���ݷ�));
        // _def = int.Parse(Data.MonsterCSV.GetData(num, (int)MonsterData.����));
        // _spd = int.Parse(Data.MonsterCSV.GetData(num, (int)MonsterData.���ǵ�));
        // _dsc = Data.MonsterCSV.GetData(num, (int)MonsterData.����);
    }
}
