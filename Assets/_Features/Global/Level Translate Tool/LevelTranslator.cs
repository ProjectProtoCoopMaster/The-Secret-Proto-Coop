using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using UnityEditorInternal;

namespace Tools.LevelDesign
{
    public class LevelTranslator : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        [SerializeField] RectTransform switchablesParent;
        [SerializeField] Camera cam;
        [SerializeField] Image prefab;
        public TextAsset json;
        [SerializeField] LevelSaver.ListOfISwitchableElement elements;

        public void TranslateLevelPosition()
        {
            elements = JsonUtility.FromJson<LevelSaver.ListOfISwitchableElement>(json.ToString());
            RectTransform parent = Instantiate(switchablesParent, canvas.transform) as RectTransform;
            for (int i = 0; i < elements.list.Count; i++)
            {
                Vector3 position = elements.list[i].position;
                
                Image newImage = Instantiate(prefab, parent.transform) as Image;
                newImage.rectTransform.anchoredPosition = position /*- new Vector3(newImage.rectTransform.sizeDelta.x * .5f, newImage.rectTransform.sizeDelta.y * .5f)*/;
                
            }




        }
    }
}

