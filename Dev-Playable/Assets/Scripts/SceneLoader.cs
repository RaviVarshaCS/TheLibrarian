// using System;
// using System.Threading.Tasks;
// using UnityEngine;
// using UnityEngine.UI;

// public class SceneLoader : MonoBehaviour
// {
//     [SerializeField] Slider loadingBar;
//     [SerializeField] float fillSpeed = 0.5f;
//     [SerializeField] Canvas loadingCanvas;
//     [SerializeField] Camera loadingCamera;
//     [SerializeField] SceneGroup[] sceneGroups;

//     float targetProgress;
//     bool isLoading;

//     public readonly SceneGroupManager manager = new SceneGroupManager();

//     void Awake() {
//         manager.OnSceneLoaded += sceneName => Debug.Log("Scene loaded: " + sceneName);
//         manager.OnSceneUnloaded += sceneName => Debug.Log("Scene unloaded: " + sceneName);
//         manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");
//     }


//     async void Start(){
//         await LoadSceneGroup(0);
//     }

//     void Update(){
//         if(!isLoading) return;

//         float currentFillAmount = loadingBar.value;
//         float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

//         float dynamicFillSpeed = progressDifference * fillSpeed;

//         loadingBar.value = Mathf.Lerp(currentFillAmount, targetProgress, dynamicFillSpeed * Time.deltaTime);
//     }

//     public async Task LoadSceneGroup(int index){
//         loadingBar.value = 0f;
//         targetProgress = 1f;

//         if(index < 0 || index >= sceneGroups.Length){
//             Debug.LogError("Invalid scene group index " + index);
//             return;
//         }

//         LoadingProgress progress = new LoadingProgress();
//         progress.Progressed += target => targetProgress = Mathf.Max(targetProgress, targetProgress);

//         EnableLoadingCanvas();
//         await manager.LoadScenes(sceneGroups[index], progress);
//         EnableLoadingCanvas(false);
//     }

//     void EnableLoadingCanvas(bool enable = true){
//         isLoading = enable;
//         loadingCanvas.gameObject.SetActive(enable);
//         loadingCamera.gameObject.SetActive(enable);
//     }
// }
// public class LoadingProgress: IProgress<float>{
//     public event Action<float> Progressed;

//     const float ratio = 1f;

//     public void Report(float value){
//         Progressed?.Invoke(value / ratio);
//     }
// }
