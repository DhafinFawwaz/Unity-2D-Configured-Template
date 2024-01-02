using UnityEngine;
using System.IO;
public class SaveHelper : MonoBehaviour
{
    [SerializeField] SaveData _newSaveDataToSet;

    public void SetSaveData()
    {
        Save.Data = _newSaveDataToSet;
        Save.SaveData();
        Debug.Log("Data value has been set to " + Save.Path);
    }
    
    public void ResetSaveData()
    {
        Save.Data = new SaveData();
        Save.SaveData();
        Debug.Log("Data value has been reset to default value");
    }
    public void DeleteSaveData()
    {
        if(File.Exists(Save.Path))
		{ 
            File.Delete(Save.Path);
            Debug.Log("SaveData has been deleted in " + Save.Path);
		}
		else
            Debug.Log("Encrypted Save Data not found");
        
        if(File.Exists(Save.UnencryptedPath))
		{ 
            File.Delete(Save.UnencryptedPath);
		}
		else
            Debug.Log("Unencrypted Save Data not found");
    }

    public void OpenSaveDataFolder()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
    
}
