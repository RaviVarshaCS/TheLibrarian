// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SceneGroupManager {
//     public event Action<string> OnSceneLoaded = delegate { };
//     public event Action<string> OnSceneUnloaded = delegate { };
//     public event Action OnSceneGroupLoaded = delegate { };

//     SceneGroup ActiveSceneGroup;

//     public async Task LoadScenes(SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false) {
//         ActiveSceneGroup = group;
//         var loadedScenes = new HashSet<string>();
//         var nextScenesToLoad = new HashSet<string>(group.Scenes.Select(scene => scene.Name));

//         int sceneCount = SceneManager.sceneCount;
//         for (var i = 0; i < sceneCount; i++) {
//             loadedScenes.Add(SceneManager.GetSceneAt(i).name);
//         }

//         await UnloadScenes(nextScenesToLoad);

//         var operationGroup = new AsyncOperationGroup(nextScenesToLoad.Count);
//         foreach (var sceneData in group.Scenes) {
//             if (!reloadDupScenes && loadedScenes.Contains(sceneData.Name)) continue;
            
//             var operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
//             operationGroup.Operations.Add(operation);

//             OnSceneLoaded.Invoke(sceneData.Name);
//         }

//         while (!operationGroup.IsDone) {
//             progress?.Report(operationGroup.Progress);
//             await Task.Delay(100);
//         }

//         Scene activeScene = SceneManager.GetSceneByName(ActiveSceneGroup.FindSceneNameByType(SceneType.ActiveScene));
//         if (activeScene.IsValid()) {
//             SceneManager.SetActiveScene(activeScene);
//         }

//         OnSceneGroupLoaded.Invoke();
//     }

//     public async Task UnloadScenes(HashSet<string> nextScenesToLoad) {
//         var scenesToUnload = new List<string>();
//         for (int i = SceneManager.sceneCount - 1; i >= 0; i--) {
//             var sceneAt = SceneManager.GetSceneAt(i);
//             if (!sceneAt.isLoaded) continue;

//             var sceneName = sceneAt.name;
//             if (sceneName == "Bootstrapper" || nextScenesToLoad.Contains(sceneName)) continue;

//             scenesToUnload.Add(sceneName);
//         }

//         var unloadTasks = new List<Task>();
//         foreach (var sceneName in scenesToUnload) {
//             var operation = SceneManager.UnloadSceneAsync(sceneName);
//             if (operation != null) {
//                 unloadTasks.Add(WaitForAsyncOperation(operation));
//                 OnSceneUnloaded.Invoke(sceneName);
//             }
//         }

//         await Task.WhenAll(unloadTasks);
//         await Resources.UnloadUnusedAssets();
//         Debug.Log("All non-essential scenes have been unloaded.");
//     }

//     private async Task WaitForAsyncOperation(AsyncOperation operation) {
//         while (!operation.isDone) {
//             await Task.Yield();
//         }
//     }
// }


// public readonly struct AsyncOperationGroup {
//     public readonly List<AsyncOperation> Operations;

//     public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
//     public bool IsDone => Operations.All(o => o.isDone);

//     public AsyncOperationGroup(int initialCapacity) {
//         Operations = new List<AsyncOperation>(initialCapacity);
//     }
// }