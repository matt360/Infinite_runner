struct ClassRegistrationContext;
void InvokeRegisterStaticallyLinkedModuleClasses(ClassRegistrationContext& context)
{
	// Do nothing (we're in stripping mode)
}

void RegisterStaticallyLinkedModulesGranular()
{
	void RegisterModule_AI();
	RegisterModule_AI();

	void RegisterModule_Animation();
	RegisterModule_Animation();

	void RegisterModule_Audio();
	RegisterModule_Audio();

	void RegisterModule_ParticleSystem();
	RegisterModule_ParticleSystem();

	void RegisterModule_ParticlesLegacy();
	RegisterModule_ParticlesLegacy();

	void RegisterModule_Physics();
	RegisterModule_Physics();

	void RegisterModule_Physics2D();
	RegisterModule_Physics2D();

	void RegisterModule_Terrain();
	RegisterModule_Terrain();

	void RegisterModule_TerrainPhysics();
	RegisterModule_TerrainPhysics();

	void RegisterModule_TextRendering();
	RegisterModule_TextRendering();

	void RegisterModule_UI();
	RegisterModule_UI();

	void RegisterModule_IMGUI();
	RegisterModule_IMGUI();

}

void RegisterAllClasses()
{
	//Total: 97 classes
	//0. AssetBundle
	void RegisterClass_AssetBundle();
	RegisterClass_AssetBundle();

	//1. NamedObject
	void RegisterClass_NamedObject();
	RegisterClass_NamedObject();

	//2. EditorExtension
	void RegisterClass_EditorExtension();
	RegisterClass_EditorExtension();

	//3. RenderSettings
	void RegisterClass_RenderSettings();
	RegisterClass_RenderSettings();

	//4. LevelGameManager
	void RegisterClass_LevelGameManager();
	RegisterClass_LevelGameManager();

	//5. GameManager
	void RegisterClass_GameManager();
	RegisterClass_GameManager();

	//6. QualitySettings
	void RegisterClass_QualitySettings();
	RegisterClass_QualitySettings();

	//7. GlobalGameManager
	void RegisterClass_GlobalGameManager();
	RegisterClass_GlobalGameManager();

	//8. MeshFilter
	void RegisterClass_MeshFilter();
	RegisterClass_MeshFilter();

	//9. Component
	void RegisterClass_Component();
	RegisterClass_Component();

	//10. Mesh
	void RegisterClass_Mesh();
	RegisterClass_Mesh();

	//11. Renderer
	void RegisterClass_Renderer();
	RegisterClass_Renderer();

	//12. Skybox
	void RegisterClass_Skybox();
	RegisterClass_Skybox();

	//13. Behaviour
	void RegisterClass_Behaviour();
	RegisterClass_Behaviour();

	//14. LineRenderer
	void RegisterClass_LineRenderer();
	RegisterClass_LineRenderer();

	//15. MeshRenderer
	void RegisterClass_MeshRenderer();
	RegisterClass_MeshRenderer();

	//16. GUILayer
	void RegisterClass_GUILayer();
	RegisterClass_GUILayer();

	//17. Texture
	void RegisterClass_Texture();
	RegisterClass_Texture();

	//18. Texture2D
	void RegisterClass_Texture2D();
	RegisterClass_Texture2D();

	//19. Texture3D
	void RegisterClass_Texture3D();
	RegisterClass_Texture3D();

	//20. RenderTexture
	void RegisterClass_RenderTexture();
	RegisterClass_RenderTexture();

	//21. RectTransform
	void RegisterClass_RectTransform();
	RegisterClass_RectTransform();

	//22. Transform
	void RegisterClass_Transform();
	RegisterClass_Transform();

	//23. Shader
	void RegisterClass_Shader();
	RegisterClass_Shader();

	//24. TextAsset
	void RegisterClass_TextAsset();
	RegisterClass_TextAsset();

	//25. Material
	void RegisterClass_Material();
	RegisterClass_Material();

	//26. Sprite
	void RegisterClass_Sprite();
	RegisterClass_Sprite();

	//27. SpriteRenderer
	void RegisterClass_SpriteRenderer();
	RegisterClass_SpriteRenderer();

	//28. Camera
	void RegisterClass_Camera();
	RegisterClass_Camera();

	//29. MonoBehaviour
	void RegisterClass_MonoBehaviour();
	RegisterClass_MonoBehaviour();

	//30. Light
	void RegisterClass_Light();
	RegisterClass_Light();

	//31. GameObject
	void RegisterClass_GameObject();
	RegisterClass_GameObject();

	//32. WindZone
	void RegisterClass_WindZone();
	RegisterClass_WindZone();

	//33. ParticleSystem
	void RegisterClass_ParticleSystem();
	RegisterClass_ParticleSystem();

	//34. Rigidbody
	void RegisterClass_Rigidbody();
	RegisterClass_Rigidbody();

	//35. Joint
	void RegisterClass_Joint();
	RegisterClass_Joint();

	//36. SpringJoint
	void RegisterClass_SpringJoint();
	RegisterClass_SpringJoint();

	//37. Collider
	void RegisterClass_Collider();
	RegisterClass_Collider();

	//38. BoxCollider
	void RegisterClass_BoxCollider();
	RegisterClass_BoxCollider();

	//39. SphereCollider
	void RegisterClass_SphereCollider();
	RegisterClass_SphereCollider();

	//40. CapsuleCollider
	void RegisterClass_CapsuleCollider();
	RegisterClass_CapsuleCollider();

	//41. WheelCollider
	void RegisterClass_WheelCollider();
	RegisterClass_WheelCollider();

	//42. CharacterController
	void RegisterClass_CharacterController();
	RegisterClass_CharacterController();

	//43. Rigidbody2D
	void RegisterClass_Rigidbody2D();
	RegisterClass_Rigidbody2D();

	//44. Collider2D
	void RegisterClass_Collider2D();
	RegisterClass_Collider2D();

	//45. CircleCollider2D
	void RegisterClass_CircleCollider2D();
	RegisterClass_CircleCollider2D();

	//46. BoxCollider2D
	void RegisterClass_BoxCollider2D();
	RegisterClass_BoxCollider2D();

	//47. NavMeshAgent
	void RegisterClass_NavMeshAgent();
	RegisterClass_NavMeshAgent();

	//48. AudioClip
	void RegisterClass_AudioClip();
	RegisterClass_AudioClip();

	//49. SampleClip
	void RegisterClass_SampleClip();
	RegisterClass_SampleClip();

	//50. AudioSource
	void RegisterClass_AudioSource();
	RegisterClass_AudioSource();

	//51. AudioBehaviour
	void RegisterClass_AudioBehaviour();
	RegisterClass_AudioBehaviour();

	//52. Animation
	void RegisterClass_Animation();
	RegisterClass_Animation();

	//53. Animator
	void RegisterClass_Animator();
	RegisterClass_Animator();

	//54. DirectorPlayer
	void RegisterClass_DirectorPlayer();
	RegisterClass_DirectorPlayer();

	//55. Terrain
	void RegisterClass_Terrain();
	RegisterClass_Terrain();

	//56. GUIText
	void RegisterClass_GUIText();
	RegisterClass_GUIText();

	//57. GUIElement
	void RegisterClass_GUIElement();
	RegisterClass_GUIElement();

	//58. Font
	void RegisterClass_Font();
	RegisterClass_Font();

	//59. Canvas
	void RegisterClass_Canvas();
	RegisterClass_Canvas();

	//60. CanvasGroup
	void RegisterClass_CanvasGroup();
	RegisterClass_CanvasGroup();

	//61. CanvasRenderer
	void RegisterClass_CanvasRenderer();
	RegisterClass_CanvasRenderer();

	//62. LensFlare
	void RegisterClass_LensFlare();
	RegisterClass_LensFlare();

	//63. TerrainCollider
	void RegisterClass_TerrainCollider();
	RegisterClass_TerrainCollider();

	//64. TrailRenderer
	void RegisterClass_TrailRenderer();
	RegisterClass_TrailRenderer();

	//65. ParticleRenderer
	void RegisterClass_ParticleRenderer();
	RegisterClass_ParticleRenderer();

	//66. ParticleSystemRenderer
	void RegisterClass_ParticleSystemRenderer();
	RegisterClass_ParticleSystemRenderer();

	//67. FlareLayer
	void RegisterClass_FlareLayer();
	RegisterClass_FlareLayer();

	//68. RuntimeAnimatorController
	void RegisterClass_RuntimeAnimatorController();
	RegisterClass_RuntimeAnimatorController();

	//69. PreloadData
	void RegisterClass_PreloadData();
	RegisterClass_PreloadData();

	//70. Cubemap
	void RegisterClass_Cubemap();
	RegisterClass_Cubemap();

	//71. TimeManager
	void RegisterClass_TimeManager();
	RegisterClass_TimeManager();

	//72. AudioManager
	void RegisterClass_AudioManager();
	RegisterClass_AudioManager();

	//73. InputManager
	void RegisterClass_InputManager();
	RegisterClass_InputManager();

	//74. Physics2DSettings
	void RegisterClass_Physics2DSettings();
	RegisterClass_Physics2DSettings();

	//75. GraphicsSettings
	void RegisterClass_GraphicsSettings();
	RegisterClass_GraphicsSettings();

	//76. PhysicsManager
	void RegisterClass_PhysicsManager();
	RegisterClass_PhysicsManager();

	//77. PhysicsMaterial2D
	void RegisterClass_PhysicsMaterial2D();
	RegisterClass_PhysicsMaterial2D();

	//78. MeshCollider
	void RegisterClass_MeshCollider();
	RegisterClass_MeshCollider();

	//79. AnimationClip
	void RegisterClass_AnimationClip();
	RegisterClass_AnimationClip();

	//80. Motion
	void RegisterClass_Motion();
	RegisterClass_Motion();

	//81. TagManager
	void RegisterClass_TagManager();
	RegisterClass_TagManager();

	//82. AudioListener
	void RegisterClass_AudioListener();
	RegisterClass_AudioListener();

	//83. Avatar
	void RegisterClass_Avatar();
	RegisterClass_Avatar();

	//84. AnimatorController
	void RegisterClass_AnimatorController();
	RegisterClass_AnimatorController();

	//85. ScriptMapper
	void RegisterClass_ScriptMapper();
	RegisterClass_ScriptMapper();

	//86. DelayedCallManager
	void RegisterClass_DelayedCallManager();
	RegisterClass_DelayedCallManager();

	//87. CGProgram
	void RegisterClass_CGProgram();
	RegisterClass_CGProgram();

	//88. MonoScript
	void RegisterClass_MonoScript();
	RegisterClass_MonoScript();

	//89. MonoManager
	void RegisterClass_MonoManager();
	RegisterClass_MonoManager();

	//90. NavMeshAreas
	void RegisterClass_NavMeshAreas();
	RegisterClass_NavMeshAreas();

	//91. PlayerSettings
	void RegisterClass_PlayerSettings();
	RegisterClass_PlayerSettings();

	//92. SkinnedMeshRenderer
	void RegisterClass_SkinnedMeshRenderer();
	RegisterClass_SkinnedMeshRenderer();

	//93. BuildSettings
	void RegisterClass_BuildSettings();
	RegisterClass_BuildSettings();

	//94. ResourceManager
	void RegisterClass_ResourceManager();
	RegisterClass_ResourceManager();

	//95. LightmapSettings
	void RegisterClass_LightmapSettings();
	RegisterClass_LightmapSettings();

	//96. RuntimeInitializeOnLoadManager
	void RegisterClass_RuntimeInitializeOnLoadManager();
	RegisterClass_RuntimeInitializeOnLoadManager();

}
