/*
Version 1.4.2
28/03/2016 fue adaptado a ser un patron singleton
28/03/2016 se reemplazó el uso de capas por Tags ya que es mas facil de implementar
28/03/2016 se ha documentado el codigo
28/03/2016 se ha reducido el uso decorativo del editor en cambio de eso se mejoro un poco como se muestra las variables en el inspector

//Debug Shows the amount of renderers, lights, Audiosources and flares at the scene and which of these are being addressed by Dynamic Occlusion also the current quality setting and FPS.

  */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SPStudios.Tools; //Always include this in order to use Square Pie Studios' singletons

/// <summary>
/// dynamic occlusion has implemented a singleton persistent pattern because only one component is required to manage the entire scene in addition to the quality control
/// </summary>
[AddComponentMenu("Darkcom/Dynamic Occlusion System")]
//public class DynamicOcclusionSystem : MonoBehaviour 
public class DynamicOcclusionSystem : PersistentMonoSingleton<DynamicOcclusionSystem>
{
    
    [Tooltip("Select the camera where you want the occlusion occurs, does not support using this script in separate Cameras. If you do not select any camera, is automatically assigned the Main Camera")]
    public Camera   cam;

    [Tooltip("Tags to be It is prepared for control of Dynamic Occlusion.")]
    public List<string> tags = new List<string>();
        
    [Tooltip("Allows this script automatically manage the graphics quality in works of FPS. if this is turned off at runtime will be the initial quality you've made​ in the Start() method.")]
    public bool	autoQuality = true; 				
    [Range(5, 20)]
    [Tooltip("If the fps drops below this, the level of low quality also")]
    public int minimalFPS = 15;
    [Range(20, 60)]
    [Tooltip("Whether fps rises above this, the quality level also rises")]
    public int maximalFPS = 30;
    [Tooltip ("Handles the details of the terrain according to current FPS to take effect Auto Quality you must have previously enabled")]
    public bool	terrainQuality = true;
    [Tooltip("Disable renderers without preserving the shadows, perfect for scenes that have already baked lightmaps or does not require the use of activated shadows .Thus Is automatically activated when the quality falls below")]
    public bool	fastMethod = true;
    //[Header("Rendering Settings")]
    [Tooltip("Allow control Dynamic renderers, Apply Renderers Occlusion")]
    public bool renderersOcclusion = true;
    [Tooltip("Allow control on Point Light and Spotlight on the scene, Apply Light Occlusion")]
    public bool lightsOcclusion = true;
    [Tooltip("Allows control on Audiosources with Spatial Blend = 1, Apply AudioSource Occlusion")]
    public bool audioSourceOcclusion = true;
    [Tooltip("Hide Lens Flares not see the camera, Apply Flares Occlusion")]
    public bool flaresOcclusion = true;
    [Tooltip("Disable GameObjects already too far and activate when the camera is close to them, enables or disables GameObjects to the distance")]
    public bool disableGameObjects = false;
    [Tooltip("enable Occlusion when renderer bound is behind other renderer")]
    public bool hideBehind = true;


    private System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();
    public double ms { get; private set; }              // Display chrono

    public GameObject[] gObjects { get; private set; } // List of All GameObjects raw
    public List<GameObject> gObjectsFiltered { get; private set; } 
    public List<Renderer> gRenderersFiltered { get; private set; } 
    public List<AudioSource> aSourcesFiltered { get; private set; } 
    public List<Light> rLightsFiltered { get; private set; } 
    public List<LensFlare> rFlaresFiltered { get; private set; }

    public Terrain[] terrains { get; private set; } // List of All Terrains actives
    public List<TerrainCollider> terrainCollider { get; private set; } 

    private int[] qualityData = new int[4];// [0] = currentQuality [1] = originalQualitySettings [2] = lastFPS [3] = lastQuality;

	public int FramesPerSec { get; protected set; } // Current FPS


    /// <summary>
    ///  // Use this for initialization
    /// </summary>
    public void Start () {
        gObjectsFiltered = new List<GameObject>(); // List of All GameObjects Filtered
        gRenderersFiltered = new List<Renderer>(); // List of Renderers filtered from gRenderers
        aSourcesFiltered = new List<AudioSource>(); // List of AudioSources Filtered from aSources
        rLightsFiltered = new List<Light>(); // List of Lights filtered from rLights
        rFlaresFiltered = new List<LensFlare>(); // List of LensFlares filtered From rFlares
        terrainCollider = new List<TerrainCollider>();

        StopAllCoroutines();
        chrono.Start();         // initialize chrono on start()
        ClearRenderersList();   // clear Lists After disable this script
		UpdateRenderersList (); // update and filtered Lists on Start

        if (cam == null) cam = Camera.main; // if no Camera is assigned, Assign main camera
        /*else
        {
            distances[layerMask] = cam.farClipPlane / 2;    // assign to layerMask the camera far clip plane
            cam.layerCullDistances = distances;             // activate layerCullDistances
        }*/
        //---------------------------------------------

        Application.targetFrameRate = 60;        //used for save memory if VSync is not active

        qualityData[1] = QualitySettings.GetQualityLevel();    // Save original quality for use when disabling this script
        StartCoroutine(FPS());              // Start FPS Counter, autoQuality and terrainQuality
        StartCoroutine(IRLateUpdate());     // Start Dinamyc Occlusion

        chrono.Stop(); //Stop chrono to measure performance
        chrono.Reset();
    }



    /// <summary>
    /// clean all array at the beginning or end a scene
    /// </summary>
    public void ClearRenderersList(){
        // CLEAR ARRAYS
        gObjects    = new GameObject [0];
		terrains 	= new Terrain[0];
        // CLEAR LISTS
        gObjectsFiltered.Clear();
        gRenderersFiltered.Clear();
        rLightsFiltered.Clear();
        aSourcesFiltered.Clear();
        rFlaresFiltered.Clear();
	}


    /// <summary>
    /// Updates all arrays at the beginning or whenever prompted
    /// </summary>
    public void UpdateRenderersList(){
        // GAMEOBJECTS FILTER
            gObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            if (gObjects.Length > 0) {
                foreach (GameObject gObject in gObjects) {
                if (/*!gObject.isStatic && */tags.Contains(gObject.tag)) gObjectsFiltered.Add(gObject);
                }
            }
        
        // RENDERERS FILTER
            if (renderersOcclusion)            {
                if (gObjectsFiltered.Count > 0)                {
                    foreach (GameObject gObject in gObjectsFiltered)                    {
                    Renderer gRender = gObject.GetComponent<Renderer>();
                        if (gRender && !gRender.isPartOfStaticBatch) gRenderersFiltered.Add(gRender);
                    }
                }
            }

        // LENS FLARE FILTER
        if (flaresOcclusion)            {
            foreach (GameObject gObject in gObjectsFiltered) {
                LensFlare rFlare = gObject.GetComponent<LensFlare>();
                if(rFlare)rFlaresFiltered.Add(rFlare);
                }            
            }
        
        // AUDIO SOURCES FILTER
        if (audioSourceOcclusion)
        {

            foreach (GameObject gObject in gObjectsFiltered)
            {
                AudioSource aSource = gObject.GetComponent<AudioSource>();
#if (UNITY_4_6 || UNITY_4_5 || UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
                if(aSource)    aSourcesFiltered.Add(aSource);
#else
                if (aSource && aSource.spatialBlend == 1) aSourcesFiltered.Add(aSource);
#endif
            }
        }

        // LIGHTS FILTER
        if (lightsOcclusion) {

            foreach (GameObject gObject in gObjectsFiltered) {
                Light rLight = gObject.GetComponent<Light>();
                if (rLight && rLight.type != LightType.Directional) rLightsFiltered.Add(rLight);
                }
            }
       
        // ACTIVE TERRAINS
        terrains = Terrain.activeTerrains;

        // TERRAIN COLLIDERS
        foreach (Terrain terrain in terrains) {
           terrainCollider.Add(terrain.GetComponent<TerrainCollider>());
        }
	}


    /// <summary>
    /// Adjust the visibility of renderers.
    /// </summary>
    /// <param name="render"></param>
    /// <param name="visible"></param>
    void SetVisibleRenderer(Renderer render, bool visible){
        // IF SIMPLE
			if ((QualitySettings.shadowCascades == 0 || !SystemInfo.supportsShadows) || fastMethod) {
				render.enabled = visible;
			}
        // IF COMPLEX
            else if((QualitySettings.shadowCascades > 0 && SystemInfo.supportsShadows) && !fastMethod ){

#if (UNITY_4_6 || UNITY_4_5 || UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
            if (visible)
            {
                if (!render.castShadows)  render.castShadows = true;
                if (!render.receiveShadows)
                {
                    render.useLightProbes = true;
                    render.receiveShadows = true;
                }
            }
            else
            {
                if (render.castShadows)  render.castShadows = false;
                if (render.receiveShadows)
                {
                    render.useLightProbes = false;
                    render.receiveShadows = false;
                }
            }
#else
            if (visible ) {
					if(render.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.On)
						render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
					if(render.reflectionProbeUsage != UnityEngine.Rendering.ReflectionProbeUsage.BlendProbesAndSkybox)
						render.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.BlendProbesAndSkybox;
					if(!render.receiveShadows){
						render.useLightProbes = true;
						render.receiveShadows = true;
			        }
				}
            else {
					if(render.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly)
						render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
					if(render.reflectionProbeUsage != UnityEngine.Rendering.ReflectionProbeUsage.Off)
						render.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
					if(render.receiveShadows){
						render.useLightProbes = false;
						render.receiveShadows = false;
						}
				}
#endif

        }
    }

    /// <summary>
    /// Returns whether a render is visible or not
    /// </summary>
    /// <param name="render"></param>
    /// <returns></returns>
    bool GetVisibleRenderer(Renderer render){
        // if SIMPLE
		if (QualitySettings.shadowCascades == 0 || !SystemInfo.supportsShadows || fastMethod) {
			if(render.enabled)return true;
			else return false;
		}
        // IF NOT
        else  {
#if (UNITY_4_6 || UNITY_4_5 || UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
            if (render.receiveShadows && render.castShadows) return true;
#else
            if(render.receiveShadows && render.shadowCastingMode == UnityEngine.Rendering.ShadowCastingMode.On)return true;
#endif
            else return false;
		}
	}

    /// <summary>
    /// Coroutine that simulates the update method but the update depends on the graphics quality
    /// </summary>
    /// <returns></returns>
    private IEnumerator IRLateUpdate(){
        // DYNAMIC OCCLUSION
        while (Application.isPlaying) {
            chrono.Start();

            #region [GameObject Deactivation]
            List<GameObject> gObjectsToRemove = new List<GameObject>();// List of GameObjects that has been elimitated

            if (gObjectsFiltered.Count > 0 && enabled) {
                foreach (GameObject gObj in gObjectsFiltered) {
                    if (gObj)
                    {
                        if (disableGameObjects)
                        {// DEACTIVE GAME OBJECTS FROM DISTANCES
                            if (ClassExtensions.GetDistanceFast(gObj.transform.position, cam.transform.position) > cam.farClipPlane / 2) gObj.SetActive(false);//SetVisibleRenderer(gRenderer, false);//esto traga 2.4 a 2.5 ms / ahora es 2.3 al cambiar el 3.1446055 por 3
                            else if(ClassExtensions.GetDistanceFast(gObj.transform.position, cam.transform.position) < cam.farClipPlane/2) gObj.gameObject.SetActive(true);
                        }
                    }
                    else gObjectsToRemove.Add(gObj);
                }
            }

            if (gObjectsToRemove.Count > 0) {
                foreach (GameObject gObj in gObjectsToRemove) {
                    gObjectsFiltered.Remove(gObj);
                }
            }
            #endregion

            #region [MeshRenderer] 
            List<Renderer> gRenderersToRemove = new List<Renderer>(); // List of Renderer that has been eliminated

            if (gRenderersFiltered.Count > 0 && enabled)            {
                foreach (Renderer gRenderer in gRenderersFiltered)       {
                    
                    if (gRenderer)                    {
                        if (ClassExtensions.GetDistanceFast(gRenderer.transform.position, cam.transform.position) > ClassExtensions.GetBoundsVolume(gRenderer.bounds))
                        {// VISIBLE FROM CAMERA VISIBLE WITHOUT FRUSTUM PLANES
                            if (!ClassExtensions.BoundsIsVisibleFromViewport(gRenderer.bounds, cam) && GetVisibleRenderer(gRenderer)) SetVisibleRenderer(gRenderer, false);
                            else if (ClassExtensions.BoundsIsVisibleFromViewport(gRenderer.bounds, cam) && !GetVisibleRenderer(gRenderer)) SetVisibleRenderer(gRenderer, true);
                           
                        }
                        else
                        {// VISIBLE FROM CAMERA WITH FRUSTUM PLANES 
                            if (!ClassExtensions.BoundsIsVisibleFromPlanes(gRenderer.bounds, cam) && GetVisibleRenderer(gRenderer)) SetVisibleRenderer(gRenderer, false);
                            else if (ClassExtensions.BoundsIsVisibleFromPlanes(gRenderer.bounds, cam) && !GetVisibleRenderer(gRenderer))SetVisibleRenderer(gRenderer, true);
                        }

                        if((ClassExtensions.BoundsIsVisibleFromViewport(gRenderer.bounds, cam) /*|| ClassExtensions.BoundsIsVisibleFromPlanes(gRenderer.bounds, cam)*/) && GetVisibleRenderer(gRenderer) && (hideBehind || QualitySettings.shadowCascades == 0) && gRenderer.bounds.IntersectRay(new Ray(cam.transform.position, gRenderer.transform.position))) SetVisibleRenderer(gRenderer, false);
                    }

                    // IF RENDERER IS ELIMINATED ADD TO LIST TO REMOVE IN NEXT STEP
                    else gRenderersToRemove.Add(gRenderer);
                }
            }
            // REMOVE RENDERERS FROM FILTERED LIST
            if (gRenderersToRemove.Count > 0) {
                foreach (Renderer gRenderer in gRenderersToRemove) {
                    gRenderersFiltered.Remove(gRenderer);
                }
                gRenderersToRemove.Clear();
            }

#endregion


            #region [Audio Sources]
            List<AudioSource> aSourcesToRemove = new List<AudioSource>(); // List of AudioSources Removed

            if (aSourcesFiltered.Count > 0)
            {
                foreach (AudioSource aSource in aSourcesFiltered)
                {
                    if (aSource)
                    {// AUDIOSOURCES DISTANCE OCCLUSION
                        if (ClassExtensions.GetDistanceFast(aSource.transform.position, cam.transform.position) > aSource.maxDistance * 2)       aSource.enabled = false;
                        else aSource.enabled = true;
                        if(aSource.enabled)aSource.priority = (byte)Mathf.Clamp(ClassExtensions.GetDistanceFast(aSource.transform.position, cam.transform.position),0,255);

                    /*
                    curSource->m_Channel != NULL
                    I found that the error on my end was due to having too many audio sources playing at one time. 
                    Due to the nature of my project I have hundreds of individual audio clips that can potentially be simultaneously playing (or trying to play.) 
                    Setting priority levels on the various audio sources via AudioSource.priority took care of the issue.    
                    */
                    }
                    // ADD TO LIST AUDIOSOURCES ELIMINATED IN NEXT STEP
                    else aSourcesToRemove.Add(aSource);
                }
            }
            // REMOVE RENDERERS FROM FILTERED LIST
            if (aSourcesToRemove.Count > 0) {
                foreach (AudioSource aSource in aSourcesToRemove) {
                    aSourcesFiltered.Remove(aSource);
                }
                aSourcesToRemove.Clear();
            }
#endregion
            //TODO: tratar de fusionar simple con complejo en la vision de las luces
            #region [Lights]
            List<Light> rLightsToRemove = new List<Light>(); // List of Lights Removed

            if (rLightsFiltered.Count > 0)
            {
                foreach (Light rLight in rLightsFiltered)
                {
                    if (rLight)
                    {   // LIGHT VISIBLE FROM CAMERA
                        if (ClassExtensions.Vector3IsVisibleFrom(rLight.transform.position, cam) && rLight.renderMode != LightRenderMode.Auto) rLight.renderMode = LightRenderMode.Auto;
                        else if (!ClassExtensions.Vector3IsVisibleFrom(rLight.transform.position, cam) && rLight.renderMode != LightRenderMode.ForceVertex) rLight.renderMode = LightRenderMode.ForceVertex;
                  
                    }
                    // ADD ELIMINATED lIGHTS TO REMOVE LIST
                    else rLightsToRemove.Add(rLight);
                }
            }
            // REMOVE ELIMINATED LIGHTS FROM FILTERED LIST
            if (rLightsToRemove.Count > 0) {
                foreach (Light rLight in rLightsToRemove) {
                    rLightsFiltered.Remove(rLight);
                }
                rLightsToRemove.Clear();
            }
            #endregion

            #region [Flares]
            List<LensFlare> rFlaresToRemove = new List<LensFlare>(); // List of LensFlares Removed

            if (rFlaresFiltered.Count > 0) {//Los Flares dan conflictos con otras camaras
                foreach (LensFlare rFlare in rFlaresFiltered) {
                    if (rFlare)
                    {// LENS FLARE VISIBLE FROM CAMERA
                        if (ClassExtensions.Vector3IsVisibleFrom(rFlare.transform.position, cam) && !rFlare.enabled) rFlare.enabled = true;
                        else if (!ClassExtensions.Vector3IsVisibleFrom(rFlare.transform.position, cam) && rFlare.enabled) rFlare.enabled = false;
                    }
                    // ADD REMOVE FLARES TO LIST
                    else rFlaresToRemove.Add(rFlare);
                }
            }
            // REMOVE LENSFLARE FROM FILTERED LIST
            if (rFlaresToRemove.Count > 0) {
                foreach (LensFlare rFlare in rFlaresToRemove) {
                    rFlaresFiltered.Remove(rFlare);
                }
                rFlaresToRemove.Clear();
            }
            #endregion

            #region TerrainColliders
            List<TerrainCollider> terrainCollidersToRemove = new List<TerrainCollider>();
            if (terrainCollider.Count > 0)
            {
                foreach (TerrainCollider terCol in terrainCollider)
                {
                    if (terCol != null)
                    {
                        if (terCol.bounds.Contains(cam.transform.position))
                        {
                            terCol.gameObject.GetComponent<Terrain>().heightmapMaximumLOD = 0;
                            terCol.gameObject.GetComponent<Terrain>().enabled = true;
                        }
                        else
                        {
                            if (ClassExtensions.BoundsIsVisibleFromPlanes(terCol.bounds, cam)) terCol.gameObject.GetComponent<Terrain>().enabled = true;
                            else terCol.gameObject.GetComponent<Terrain>().enabled = false;

                            terCol.gameObject.GetComponent<Terrain>().heightmapMaximumLOD = 1;
                        }
                    }
                    else terrainCollidersToRemove.Add(terCol);
                }
            }

            if (terrainCollidersToRemove.Count > 0) {
                foreach (TerrainCollider terCol in terrainCollidersToRemove) {
                    terrainCollider.Remove(terCol);
                }
                terrainCollidersToRemove.Clear();
            }
            #endregion

            chrono.Stop();
            ms = chrono.Elapsed.TotalMilliseconds;
            chrono.Reset();
            yield return new WaitForSeconds(1/(qualityData[0]+1));
        }
    }

    //-------------------------------------------------------------------------------------------
    // AUTOQUALITY

    /// <summary>
    /// Coroutine measuring frames per second internally to increase or decrease the quality
    /// </summary>
    /// <returns></returns>
    private IEnumerator FPS() {
		for(;;){
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(1);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;
			
			// Display it
			FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            if ((FramesPerSec < minimalFPS || FramesPerSec > maximalFPS) && autoQuality) CalculateQuality();
            else {
                if(QualitySettings.GetQualityLevel() == qualityData[1] && !autoQuality)QualitySettings.SetQualityLevel(qualityData[1]);
            }
			qualityData[0] = QualitySettings.GetQualityLevel ();
			
		}
	}


    /// <summary>
    /// Enabling Automatic quality
    /// </summary>
    /// <param name="activation"></param>
    public void ActivateAutomaticQuality(bool activation){
        // AUTOMATIC QUALITY FOR ACTIVATE FROM OTHER SCRIPT
		autoQuality = activation;
		if(activation == false) QualitySettings.SetQualityLevel(qualityData[1]);
	}


    /// <summary>
    /// Calculate the quality based on current FPS
    /// </summary>
    private void CalculateQuality(){

        // RETURN IF LAST FPS IS EQUAL CURRENT FPS
        if (qualityData[2] == Mathf.FloorToInt (FramesPerSec) || !autoQuality)
			return;
		else
			qualityData[2] = Mathf.FloorToInt (FramesPerSec);
        // INCREASE OR DECREASE QUALITY LEVEL
		if(qualityData[0] > 0 && FramesPerSec < minimalFPS && autoQuality)
			QualitySettings.DecreaseLevel(false);
		else if(qualityData[0] < QualitySettings.names.Length && FramesPerSec > maximalFPS && autoQuality)
			QualitySettings.IncreaseLevel(false);
        // INCREASE OR DECREASE CAMERA FAR CLIP PLANE AND SHADER LOD
		cam.farClipPlane = 3000/QualitySettings.names.Length - qualityData[0];
        //distances[layerMask.value] = cam.farClipPlane / 2;
        Shader.globalMaximumLOD = 100 + (qualityData[0] * 100);
        // INCREASE OR DECREASE TERRAIN DETAILS
        foreach (Terrain terrain in terrains) {
			if(terrainQuality)SetTerrainQuality(terrain,qualityData[0]);
			else SetTerrainQuality(terrain,qualityData[1]);
		}
	}


    /// <summary>
    /// Adjust terrain quality based on the graphic quality
    /// </summary>
    /// <param name="eTerrain"></param>
    /// <param name="quality"></param>
    private void SetTerrainQuality(Terrain eTerrain,int quality){

		if (qualityData[3] == quality)
			return;
		else
			qualityData[3] = quality;

		eTerrain.heightmapPixelError 	= 5*QualitySettings.names.Length - quality;
		eTerrain.basemapDistance 		= cam.farClipPlane/2;
        eTerrain.detailObjectDensity 	= (1/terrains.Length);// Esta accion es muy cara esto es mejor en Start

        eTerrain.detailObjectDistance 	= Mathf.Clamp(qualityData[2] + 30,30,90);// esta comienza a ser cara despues de 80
    //http://wiki.unity3d.com/index.php?title=Terrain_tutorial
        eTerrain.treeDistance 			= eTerrain.detailObjectDistance * 2; //cam.farClipPlane;//barata
        eTerrain.treeBillboardDistance = eTerrain.detailObjectDistance;//cam.farClipPlane / 2;//barata
		eTerrain.treeMaximumFullLODCount= 50 / QualitySettings.names.Length - quality;//Barata
		
		/*if(qualityData[0]== 0)eTerrain.heightmapMaximumLOD	= 1;
		else eTerrain.heightmapMaximumLOD	= 0;*/
	}
    //--------------------------------------------------------------------------------------

    //private void OnDestroy(){}

    /// <summary>
    /// Run actions when disable this component
    /// </summary>
    private void OnDisable ()	{

		if (Application.isEditor)
			QualitySettings.SetQualityLevel(qualityData[1]);

		foreach (Renderer gRenderer in gRenderersFiltered) {
            if (gRenderer) {
                gRenderer.gameObject.SetActive(true);
                SetVisibleRenderer(gRenderer, true);
            }
		}

        foreach (Light rLight in rLightsFiltered) {
            if (rLight) {
                rLight.gameObject.SetActive(true);
                rLight.enabled = true;
            }
        }

        foreach (AudioSource aSource in aSourcesFiltered) {
            if (aSource) aSource.enabled = true;
        }

        foreach (LensFlare rFlare in rFlaresFiltered) {
            if (rFlare) rFlare.enabled = true;
        }

        StopAllCoroutines();
	}
    [ContextMenu("Fast Configuration")]
    public void FastConfiguration() {
        if (FindObjectOfType(typeof(Renderer))) renderersOcclusion = true;
        else renderersOcclusion = false;
        if (FindObjectOfType(typeof(Light))) lightsOcclusion = true;
        else lightsOcclusion = false;
        if (FindObjectOfType(typeof(AudioSource))) audioSourceOcclusion = true;
        else audioSourceOcclusion = false;
        if (FindObjectOfType(typeof(LensFlare))) flaresOcclusion = true;
        else flaresOcclusion = false;
        if (gObjects.Length > 250) disableGameObjects = true;
        else disableGameObjects = false;
    }

    /// <summary>
    /// Run actions when enable this component
    /// </summary>
    private void OnEnable() {        Start();    }
    /// <summary>
    /// Run actions when a level has been loaded this component
    /// </summary>
    private void OnLevelWasLoaded ()	{		Start();    }
    
}
