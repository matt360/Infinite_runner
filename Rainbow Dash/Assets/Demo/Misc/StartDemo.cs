using UnityEngine;
using SPStudios.Tools; //Always include this in order to use Square Pie Studios' singletons

public class StartDemo : PersistentMonoSingleton {

	public GameObject[] Prefabs;
	public int PrefabNum;
	public float PosY;
	public float min = 0f;
	public float max = 0f;
    private DynamicOcclusionSystem DOS;
	
	void Awake() {
		GenerateLevel();
        DOS = Singletons.Get<DynamicOcclusionSystem>();
        DOS.FastConfiguration();
        if (!DOS.tags.Contains("DOS")) DOS.tags.Add("DOS");
        if (!DOS.tags.Contains("DOS2"))DOS.tags.Add("DOS2");
    }


    void GenerateLevel()
	{
		Vector3 prefabPos;
		for (var i = 0; i < PrefabNum; i++)
		{
			prefabPos = new Vector3(Random.Range(min, max), PosY, Random.Range(min, max));
            Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], prefabPos, Quaternion.identity);
		}
	}
    private void OnLevelWasLoaded() { Awake(); }

}
