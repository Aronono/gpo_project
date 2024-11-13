using UnityEngine;
using UnityEngine.UI;
using GenerationClasses;
using System.IO;

class DBManager : MonoBehaviour
{
    static string dbName = "/Dungeon.db";
    static string dbLink = "URI=file:" + Application.dataPath + dbName;
    [SerializeField] Text dungeonNameField;
    private NameGenerator nameGen;
    private ThreatGenerator threatGen;

    void Start()
    {
        if (CheckDB()) {
            InitGenerators();
            ChangeName(nameGen.GenerateName()); //при запуске один раз генерирует название
        }
    }

    void InitGenerators()
    {
        nameGen = new NameGenerator(dbLink);
        threatGen = new ThreatGenerator(dbLink);
    }

    bool CheckDB()
    {
        if (!File.Exists(Application.dataPath + dbName)) 
        {
            Debug.Log("Dungeon.db Not Found in " + Application.dataPath + ", Everything Will Break");
            return false;
        }
        else return true;
    }

    private void ChangeName(string newName)
    {
        dungeonNameField.text = newName;
    }
}