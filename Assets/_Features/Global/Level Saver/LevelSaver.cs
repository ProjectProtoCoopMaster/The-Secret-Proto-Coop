using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Gameplay;
using UnityEditor;

namespace Tools.LevelDesign
{
    
    public class LevelSaver : MonoBehaviour
    {
        public GameObject[] parentSwitchers;
        public ListOfISwitchableElement elements;

        
        string path, JsonString;
        public void SaveJSON()
        {
            elements.list.Clear();
            for (int i = 0; i < parentSwitchers.Length; i++)
            {
                AddSwitcherChilds(parentSwitchers[i]);
            }

            path = "Assets/_Features/Global/Level Saver"+"/Level.json";
            JsonString = File.ReadAllText(path);
            JsonString = JsonUtility.ToJson(elements);


            Debug.Log(JsonString);
            File.WriteAllText(path, JsonString);

            
        }

        private void AddSwitcherChilds(GameObject switcher)
        {
            for (int j = 0; j < switcher.transform.childCount; j++)
            {
                if (switcher.transform.GetChild(j).GetComponent<ISwitchable>() != null)
                {
                    

                    if (switcher.transform.GetChild(j).gameObject.GetComponent<SwitcherBehavior>())
                    {
                        AddSwitcherChilds(switcher.transform.GetChild(j).gameObject);
                    }
                    else
                    {

                        ISwitchableElements newElement = new ISwitchableElements();
                        newElement.prefab = PrefabUtility.GetCorrespondingObjectFromSource(switcher.transform.GetChild(j).gameObject) as GameObject;
                        
                        newElement.position = switcher.transform.GetChild(j).position;
                        elements.list.Add(newElement);
                    }
                }
            }
        }
        [System.Serializable]
        public struct ISwitchableElements
        {
            public GameObject prefab;
            public Vector3 position;
        }
        [System.Serializable]
        public struct ListOfISwitchableElement
        {
            public List<ISwitchableElements> list;
        }
    }

}

