using UnityEngine;
public static class Save
{
    /// <summary>
    /// SaveData object instance.
    /// </summary>
    public static SaveData Data = new SaveData();
    
    /// <summary>
    /// Called when SaveData finished loading.
    /// </summary>
    public static void OnDataLoaded()
    {
        Debug.Log("Save Data loaded");
    
    }

    /// <summary>
    /// Constructor to load save data when application start.
    /// </summary>
    static Save()
    {
        LoadData();
    }

    /// <summary>
    /// Save Save.Data to a path.
    /// </summary>
    public static void SaveData()
    {
        Encryption.SaveData(Data);
    }

    /// <summary>
    /// Load SaveData to Save.Data.
    /// </summary>
    public static SaveData LoadData()
    {
        Data = Encryption.LoadData();
        OnDataLoaded();
        return Data;
    }
}
