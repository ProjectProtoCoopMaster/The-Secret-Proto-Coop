using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace Tools.LevelDesign
{
    public class CameraPicture : MonoBehaviour
    {
        
        private Camera cam;
        public string pictureName;

        public void TakePicture()
        {
            cam = GetComponent<Camera>();

            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = cam.targetTexture;

            cam.Render();

            Texture2D tex2D = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
            tex2D.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
            tex2D.Apply();

            RenderTexture.active = currentRT;

            var Bytes = tex2D.EncodeToPNG();
            DestroyImmediate(tex2D);
            File.WriteAllBytes(Application.dataPath + "/_Features/Global/Level Translate Tool/Pictures/"+pictureName + ".png", Bytes);
            AssetDatabase.Refresh();
        }

        

    }
}

