using UnityEngine;
using CustomUtility.IO;

public class PlayerSaveDataExample : SaveData
{
    [field: SerializeField] public int Hp { get; set; }
    [field: SerializeField] public Vector3 Position { get; set; }
    [field: SerializeField] public int Exp { get; set; }

    public PlayerSaveDataExample()
    {
    }

    public PlayerSaveDataExample(int hp, Vector3 position, int exp)
    {
        Hp = hp;
        Position = position;
        Exp = exp;
    }
}

public class SaveTestSample : MonoBehaviour
{
    private static PlayerSaveDataExample _jsonSave;
    private static PlayerSaveDataExample _jsonLoad;
    public static PlayerSaveDataExample JsonLoad { get { return _jsonLoad; } set { _jsonLoad = value; } }
    private static PlayerSaveDataExample _binarySave;
    private static PlayerSaveDataExample _binaryLoad;

    private void Start()
    {
        //LoadJson();
        
        //SaveBinary(0, Vector3.zero, 0);
        LoadBinary();
    }

    public static void SaveJson(int hp, Vector3 pos, int exp)
    {
        _jsonSave = new(hp, pos, exp);
        
        DataSaveController.Save(_jsonSave, SaveType.JSON);
    }

    public static void LoadJson()
    {
        _jsonLoad = new(0, Vector3.zero, 0);
        
        DataSaveController.Load(ref _jsonLoad, SaveType.JSON);
        Debug.Log($"{_jsonLoad.Hp}, {_jsonLoad.Position.x}.{_jsonLoad.Position.y}.{_jsonLoad.Position.z}, {_jsonLoad.Exp}");
    }
    
    public static void SaveBinary(int hp, Vector3 pos, int exp)
    {
        _jsonSave = new(hp, pos, exp);

        DataSaveController.Save(_jsonSave, SaveType.BINARY);
    }

    public static void LoadBinary()
    {
        _jsonLoad = new(0, Vector3.zero, 0);
        
        DataSaveController.Load(ref _jsonLoad, SaveType.BINARY);
        Debug.Log($"{_jsonLoad.Hp}, {_jsonLoad.Position.x}.{_jsonLoad.Position.y}.{_jsonLoad.Position.z}, {_jsonLoad.Exp}");
    }
}
