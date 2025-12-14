using UnityEngine;
using System.Collections;
using MyNamespace;
using System.IO;
//using Newtonsoft.Json;

public class DeserMono : MonoBehaviour
{
    public static DeserMono deserMono;
    private void Awake()
    {
        deserMono = this;
    }
    public void Load()
    {
        string jsonString = File.ReadAllText("\"D:\\repos\\EditorScriptingExercise\\Assets\\Scripts\\SerializationExercise\\JsonFile.json\"");
        
        deserMono = JsonUtility.FromJson<DeserMono>(jsonString);
        //deserMono = JsonConvert.DeserializeObject<DeserMono>(jsonString);

        File.WriteAllText("\"D:\\repos\\EditorScriptingExercise\\Assets\\Scripts\\SerializationExercise\\JsonFile_out.json\"", jsonString);
    }
}
