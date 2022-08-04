using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Encryption : MonoBehaviour
{
    string path;
    const string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";


    static readonly string JSON_ENCRYPTED_KEY = "#kJ83DAlowjkf39(#($%0_+[]:#dDA'a";
                                               //01234567890123456789012345678901
    void Start()
    {
        path = "data/data/" + Application.identifier.ToString() + "/files/filesysteminformation.dat";//Path for android
        path = Application.persistentDataPath + "/filesysteminformation.dat";//Path for pc
        LoadData();
    }

    

    public void SaveData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        Rijndael crypto = new Rijndael();
        byte[] soup = crypto.Encrypt(json, JSON_ENCRYPTED_KEY);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        File.WriteAllBytes(path, soup);
    }
    public SaveData LoadData()
    {
		if(File.Exists(path))
		{ 
            Rijndael crypto = new Rijndael();
            byte[] soupBackIn = File.ReadAllBytes(path);
            string jsonFromFile = crypto.Decrypt(soupBackIn, JSON_ENCRYPTED_KEY);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonFromFile);
            return data;
		}
		else
		{
            Debug.Log("File not found");
            return new SaveData();
		}
    }
}

