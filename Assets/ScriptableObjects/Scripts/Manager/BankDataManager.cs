using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

[CreateAssetMenu(fileName = "BankManager", menuName = "Data/DataManager/BankData")]
public class BankDataManager : ScriptableObject
{
    private List<BankData> bankDatas;

    public void Init()
    {
        bankDatas = Load();
        if(bankDatas == null )
            bankDatas = new List<BankData>();
    }

    public bool AddBankData(string ID, string PS, string Name, int Gold)
    {
        if(bankDatas == null)
        {
            Debug.Log("BankData Null");
            Init();
        }
        foreach(var bank in bankDatas)
        {
            if (bank.ID == ID)
                return false;
        }

        bankDatas.Add(new BankData(ID, PS, Name, Gold));
        return true;
    }

    public void GetBankData(string ID, out BankData data)
    {
        if (bankDatas == null)
        {
            Debug.Log("BankData Null");
            Init();
        }
        data =  bankDatas.Find(x => x.ID == ID);
    }

    public void Save()
    {
        if (bankDatas == null || bankDatas.Count == 0)
        {
            return;
        }
        string path = Application.dataPath + "/Data/BankData.json";
        File.WriteAllText(path, JsonConvert.SerializeObject(bankDatas, Formatting.Indented));
    }

    private List<BankData> Load()
    {
        string path = Application.dataPath + "/Data/BankData.json";
        if (File.Exists(path) == false)
            return null;
        StreamReader file = File.OpenText(path);
        if (file != null)
        {
            JsonTextReader reader = new JsonTextReader(file);

            JArray json = (JArray)JToken.ReadFrom(reader);
            string str = JsonConvert.SerializeObject(json);
            file.Close();
            return JsonConvert.DeserializeObject<List<BankData>>(str);
        }
        Debug.LogError("Load Faill BankData");
        return null;
    }
}

[System.Serializable]
public class BankData
{
    public string ID;
    public string PS;
    public string Name;
    public int Gold;

    public BankData(string ID, string PS, string Name, int Gold)
    {
        this.ID = ID;
        this.PS = PS;
        this.Name = Name;
        this.Gold = Gold;
    }
}
