/*
Version 1.4.2
28/03/2016 agregada algo de documentacion del codigo
28/03/2016 cambio de cosas menores en el codigo

 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SPStudios.Tools; //Always include this in order to use Square Pie Studios' singletons


/// <summary>
/// Class is used to determine an array of objects in which is embedded the different LODs indicates the distances between objects
/// </summary>
[System.Serializable]
public class LODData {
    public MeshFilter meshFilter;
    [Tooltip("LOD 0: Mesh greater detail (it is recommended not to assign nothing here)")]
    public Mesh lod0;
    [Tooltip("LOD 1: mesh median geometry")]
    public Mesh lod1;
    [Tooltip("LOD 2: mesh low geometry ")]
    public Mesh lod2;
    [Tooltip("if this is activated automatically calculate the distances based on the volume renderer Distances ignoring values")]
    public bool automaticDistance;
    [Tooltip("is the distance at which switches between LOD0 and LOD1")]
    public float lodDistance1;
    [Tooltip("is the changeover between LOD1 and LOD2")]
    public float lodDistance2;
    public MeshRenderer meshRenderer;
}

/// <summary>
/// Class manages level of detail of the whole scene, as well as the sacrifice of objects by the distance
/// </summary>
[AddComponentMenu("Darkcom/Dynamic LOD")]
public class DynamicLOD: MonoBehaviour {
    [Tooltip("Levels of Detail")]
    public List<LODData> lodDatas = new List<LODData>();
    [Tooltip("Camera where change takes effect on levels of detail")]
	public Camera cam;
    private List<GameObject> childrens = new List<GameObject>();

    /// <summary>
    /// Method to initialize variables, combining static GameObjects and assign LOD's
    /// </summary>
    private void Start (){
        childrens.Clear();

        if (transform.childCount > 0) {
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).gameObject.isStatic) childrens.Add(transform.GetChild(i).gameObject);
            }
        }
                
        // pro feature
        if (gameObject.isStatic && childrens.Count > 0) StaticBatchingUtility.Combine(childrens.ToArray(), gameObject);
        
        //Experimental
        if (lodDatas.Count > 0) {
            foreach (LODData lodData in lodDatas) {
                if (lodData.lod0 == null) lodData.lod0 = lodData.meshFilter.mesh;
            }
        }

        //-------------------------]
        if (Singletons.Get<DynamicOcclusionSystem>())cam = Singletons.Get<DynamicOcclusionSystem>().cam;
		if(cam == null)cam = Camera.main;
		StartCoroutine (GestarLOD());
	}


    /// <summary>
    /// Coroutine that serves pseudo Update that varies with the level of graphic quality, here almost constantly it checks the transitions between levels of detail
    /// </summary>
    /// <returns></returns>
    IEnumerator GestarLOD(){
		while (Application.isPlaying) {

            if (lodDatas.Count > 0) {
                foreach (LODData lodData in lodDatas) {
                    if (lodData.meshRenderer.enabled)
                    {
                        if (QualitySettings.GetQualityLevel() > 1 && lodData.lod0 && lodData.lod1 && lodData.lod2)
                        {
                            if (ClassExtensions.GetDistanceFast(cam.transform.position, lodData.meshRenderer.transform.position) < lodData.lodDistance1 && lodData.lod0)
                                lodData.meshFilter.mesh = lodData.lod0;
                            else if (ClassExtensions.GetDistanceFast(cam.transform.position, lodData.meshRenderer.transform.position) > lodData.lodDistance1 && ClassExtensions.GetDistanceFast(cam.transform.position, lodData.meshRenderer.transform.position) < lodData.lodDistance2 && lodData.lod1)
                                lodData.meshFilter.mesh = lodData.lod1;
                            else if (ClassExtensions.GetDistanceFast(cam.transform.position, lodData.meshRenderer.transform.position) > lodData.lodDistance2 && lodData.lod2)
                                lodData.meshFilter.mesh = lodData.lod2;
                        }

                        else if (QualitySettings.GetQualityLevel() == 1 && lodData.lod1 && lodData.lod2)
                        {
                            if (ClassExtensions.GetDistanceFast(cam.transform.position, lodData.meshRenderer.transform.position) < lodData.lodDistance1 && lodData.lod1)
                                lodData.meshFilter.mesh = lodData.lod1;
                            else lodData.meshFilter.mesh = lodData.lod2;

                        }
                        else if (QualitySettings.GetQualityLevel() == 1 && lodData.lod2) lodData.meshFilter.mesh = lodData.lod2;
                    }
                    else {
                        if (lodData.lod2) lodData.meshFilter.mesh = lodData.lod2;
                        else if (lodData.lod1) lodData.meshFilter.mesh = lodData.lod1;
                    }

                    float temporalVolume = 0;
                    if (lodData.meshRenderer.enabled && temporalVolume != ClassExtensions.GetBoundsVolume(lodData.meshRenderer.bounds))
                    {

                        if (lodData.automaticDistance)
                        {
                            lodData.lodDistance1 = 2 * ClassExtensions.GetBoundsVolume(lodData.meshRenderer.bounds);
                            lodData.lodDistance2 = 3 * ClassExtensions.GetBoundsVolume(lodData.meshRenderer.bounds);
                        }
                        temporalVolume = ClassExtensions.GetBoundsVolume(lodData.meshRenderer.bounds);
                    }
                }
            }
           
        yield return new WaitForSeconds(1 / (1+QualitySettings.GetQualityLevel()));
		}
	}
}
