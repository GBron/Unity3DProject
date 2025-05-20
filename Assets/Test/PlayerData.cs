using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUtility.IO;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _exp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyPlayerData();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayerData();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _hp++;
            _exp++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _hp--;
            _exp--;
        }
    }

    private void ApplyPlayerData()
    {
        _hp = SaveTestSample.JsonLoad.Hp;
        _exp = SaveTestSample.JsonLoad.Exp;
        transform.position = SaveTestSample.JsonLoad.Position;
    }

    private void SavePlayerData()
    {
        SaveTestSample.SaveBinary(_hp, transform.position, _exp);
    }

    private void LoadPlayerData()
    {
        SaveTestSample.LoadBinary();
    }

}
