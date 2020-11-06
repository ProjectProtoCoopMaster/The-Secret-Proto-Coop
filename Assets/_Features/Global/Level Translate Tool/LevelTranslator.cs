using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

namespace Tools.LevelDesign
{
    public class LevelTranslator : MonoBehaviour
    {
        [SerializeField] GameObject cube;
        [SerializeField] Camera cam;
        [SerializeField] Image image;
        [SerializeField] Object jsonFile;
        public TextAsset json;
        [SerializeField] LevelSaver.ListOfISwitchableElement elements;

        public void TranslateLevelPosition()
        {
            //string path = AssetDatabase.GetAssetPath(jsonFile);
            //AssetDatabase.MoveAsset(path, path.Replace(".json", ".txt"));

            //Object newJson = new Object();
            //AssetDatabase.CreateAsset(new TextAsset(), "Assets/StreamingAssets/Levels/Text/Text.txt");
            //File.
            //json = new TextAsset();
            //json = jsonFile as TextAsset;
            elements = JsonUtility.FromJson<LevelSaver.ListOfISwitchableElement>(json.ToString());


            //Vector3 screenPoint = cam.WorldToViewportPoint(cube.transform.position);
            //screenPoint = new Vector2(screenPoint.x * cam.pixelWidth, screenPoint.y * cam.pixelHeight);
            //image.rectTransform.anchoredPosition = screenPoint;
        }
    }
}

