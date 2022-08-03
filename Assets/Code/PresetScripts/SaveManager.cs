using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public SaveData data;
    public void OnDataLoaded()
    {

    }
    void Start()
    {
        LoadData();
    }


    










    [SerializeField] Encryption encryption;
    public void SaveData()
    {
        encryption.SaveData(data);
    }
    public void LoadData()
    {
        data = encryption.LoadData();
        OnDataLoaded();
    }
}
