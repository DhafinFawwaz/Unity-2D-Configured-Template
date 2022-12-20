using UnityEngine;
using System.IO;
public static class Encryption
{
    /// <summary>
    /// File name for the save file. Modify it however you want.
    /// </summary>
    static string _fileName = "game";

    /// <summary>
    /// File format for the save file. Can actually be anything.
    /// </summary>
    static string _fileFormat = ".dat";

    /// <summary>
    /// Key for encryption. Change it to any string with 32 characters.
    /// </summary>
    const string JSONEncryptedKey = "#kJ83DAlowjkf39(#($%0_+[]:#dDA'a";
                                   //01234567890123456789012345678901

#if UNITY_STANDALONE_WIN
    /// <summary>
    /// Save file path for Windows.
    /// </summary>
    public static readonly string Path = Application.persistentDataPath + "/" +_fileName + _fileFormat;
#elif UNITY_ANDROID
    /// <summary>
    /// Save file path for Android.
    /// </summary>
    public static readonly string Path = "data/data/" + Application.identifier.ToString() + "/files/"+_fileName + _fileFormat;
#endif

    /// <summary>
    /// Save data to a path.
    /// </summary>
    /// <param name="data">SaveData object to save into json.</param>
    public static void SaveData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        Rijndael crypto = new Rijndael();
        byte[] soup = crypto.Encrypt(json, JSONEncryptedKey);

        if (File.Exists(Path))
        {
            File.Delete(Path);
        }

        File.WriteAllBytes(Path, soup);
        Debug.Log("File saved successfully");

        if(_saveUnencryptedData) SaveUnencryptedData(data);
    }

    /// <summary>
    /// Load data to a SaveData object.
    /// </summary>
    /// <returns>Returns SaveData object.</returns>
    public static SaveData LoadData()
    {
		if(File.Exists(Path))
		{ 
            Rijndael crypto = new Rijndael();
            byte[] soupBackIn = File.ReadAllBytes(Path);
            string jsonFromFile = crypto.Decrypt(soupBackIn, JSONEncryptedKey);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonFromFile);
            return data;
		}
		else
		{
            Debug.Log("Save File not found");
            SaveData saveData = new SaveData();
            return saveData;
		}
    }

#region Unencrypted

#if UNITY_STANDALONE_WIN
    /// <summary>
    /// Path for unencrypted save data.
    /// </summary>
    public static readonly string UnencryptedPath = Application.persistentDataPath + "/Unencrypted_" +_fileName + ".txt";
#elif UNITY_ANDROID
    /// <summary>
    /// Path for unencrypted save data.
    /// </summary>
    public static readonly string UnencryptedPath = "data/data/" + Application.identifier.ToString() + "/files/Unencrypted_"+_fileName + ".txt";
#endif    
    
    /// <summary>
    /// Change to true to save unencrypted version of the save data for debugging purpose. Don't forget to change this to false before building.
    /// </summary>
    static bool _saveUnencryptedData = true;

    /// <summary>
    /// Save unencrypted data to UnencryptedPath.
    /// </summary>
    /// <param name="data">SaveData object to save into json.</param>
    public static void SaveUnencryptedData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        if (File.Exists(UnencryptedPath))
        {
            File.Delete(UnencryptedPath);
        }
        File.WriteAllText(UnencryptedPath, json);
    }
#endregion Unencrypted
}

