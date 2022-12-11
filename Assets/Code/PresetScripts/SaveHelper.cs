using UnityEngine;
using System.IO;
public class SaveHelper : MonoBehaviour
{
    [SerializeField] SaveData _newSaveDataToSet;
    SaveData _currentSaveData;


    public void SetSaveData()
    {
        _currentSaveData = _newSaveDataToSet;
        Encryption.SaveData(_currentSaveData);
        Debug.Log("Data value has been set to " + Encryption.Path);
    }
    
    public void ResetSaveData()
    {
        _currentSaveData = new SaveData();
        Encryption.SaveData(_currentSaveData);
        Debug.Log("Data value has been reset to default value");
    }
    public void DeleteSaveData()
    {
        if(File.Exists(Encryption.Path))
		{ 
            File.Delete(Encryption.Path);
            _currentSaveData = null;
            Debug.Log("SaveData has been deleted in " + Encryption.Path);
		}
		else
            Debug.Log("Encrypted Save Data not found");
        
        if(File.Exists(Encryption.UnencryptedPath))
		{ 
            File.Delete(Encryption.UnencryptedPath);
		}
		else
            Debug.Log("Unencrypted Save Data not found");
    }

    public void OpenSaveDataFolder()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
    
}
