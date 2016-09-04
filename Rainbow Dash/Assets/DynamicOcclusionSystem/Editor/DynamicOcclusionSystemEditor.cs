using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DynamicOcclusionSystem))]
public class DynamicOcclusionSystemEditor : Editor {
    
	private Texture2D logo;
    public int minFPS = 15;

    void OnEnable(){       
        logo = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/DynamicOcclusionSystem/Editor/Images/DynamicOcclusion.png", typeof(Texture2D));
    }

    public override void OnInspectorGUI(){
        GUILayout.Label(logo);
        DynamicOcclusionSystem DOS = target as DynamicOcclusionSystem;
        EditorGUILayout.HelpBox("[Current Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()] + "] [to: " + DOS.FramesPerSec + " FPS]" + "[Latency: " + DOS.ms + " ms]\n" + 
                "[Renderers: " + DOS.gRenderersFiltered.Count + " of " + DOS.gObjectsFiltered.Count +"]\n" +
                "[GObjects: " + DOS.gObjectsFiltered.Count + " of " + DOS.gObjects.Length + "]\n" +
                "[AudioSources: " + DOS.aSourcesFiltered.Count + " of " + DOS.gObjectsFiltered.Count + "]\n" +
                "[Lights: " + DOS.rLightsFiltered.Count + " of " + DOS.gObjectsFiltered.Count + "]\n" +
                "[LensFlares: " + DOS.rFlaresFiltered.Count + " of " + DOS.gObjectsFiltered.Count + "]\n" +
                "[Terrains: " + DOS.terrains.Length + "]", MessageType.None, true);

        DrawDefaultInspector();
             
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(DOS);
           // DOS.Start();
        }
        
    }

}
