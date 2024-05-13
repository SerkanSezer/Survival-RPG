using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class ObjectID : EditorWindow
{
    private List<int> idList = new List<int>();
    //int objectId = 1;
    [MenuItem("Tools/CreateObjectID")]

    public static void ShowWindow() {
        GetWindow(typeof(ObjectID));    
    }

    private void OnGUI() {
        GUILayout.Label("Create Object IDs", EditorStyles.boldLabel);
        //objectId = EditorGUILayout.IntField("Object ID",objectId);

        if (GUILayout.Button("Set Object Id")) {
            SetObjectID();
        }
    }

    public void SetObjectID() {
        List<CollectibleResource> crList = new List<CollectibleResource>();
        crList.AddRange(FindObjectsOfType<CollectibleResource>());
        foreach (var cr in crList) {
            int tempId = Random.Range(10000, 99999);
            while (!idList.Contains(tempId)) {
                idList.Add(tempId);
                //cr.SetID(tempId);
            }
        }
    }


}
