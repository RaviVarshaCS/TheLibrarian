using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGroupManager {
    public event Action<string> OnSceneLoaded = delegate { };
    public event Action<string> OnSceneUnloaded = delegate { };
    public event Action OnSceneGroupLoaded = delegate { };
    // public event Action<string> OnActiveSceneChanged = delegate { };

    SceneGroup ActiveSceneGroup;

    public async Task LoadScenes(SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false) {
        ActiveSceneGroup = group;
        var loadedScenes = new List<string>();

        await UnloadScenes();

        int sceneCount = SceneManager.sceneCount;

        for(var i = 0; i < sceneCount; i++) {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        var totalScenesToLoad = ActiveSceneGroup.Scenes.Count;

        var operationGroup = new AsyncOperationGroup(totalScenesToLoad);
        
        for(var i = 0; i < totalScenesToLoad; i++) {
            var sceneData = group.Scenes[i];
            if(reloadDupScenes == false && loadedScenes.Contains(sceneData.Name)) continue;

            var operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);

            operationGroup.Operations.Add(operation);

            OnSceneLoaded.Invoke(sceneData.Name);
        }

        // Wait until all AsyncOperations in the group are done
        //come back?
        // this is where we should make sure data is loaded
        while(!operationGroup.IsDone) {
            progress?.Report(operationGroup.Progress);
            await Task.Delay(100);
        }

        Scene activeScene = SceneManager.GetSceneByName(ActiveSceneGroup.FindSceneNameByType(SceneType.ActiveScene));

        if(activeScene.IsValid()) {
            SceneManager.SetActiveScene(activeScene);

            // **************
            // raise event? of changing active scene?
            // OnActiveSceneChanged.Invoke(group.GroupName);
            // ***************
        }

        OnSceneGroupLoaded.Invoke();
    }

    public async Task UnloadScenes()
    {
        var scenesToUnload = new List<string>();
        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            var sceneAt = SceneManager.GetSceneAt(i);
            if (!sceneAt.isLoaded) continue;

            var sceneName = sceneAt.name;
            if (sceneName == "Bootstrapper") continue; // Keep only essential scenes

            scenesToUnload.Add(sceneName);
        }

        var unloadTasks = new List<Task>();
        foreach (var sceneName in scenesToUnload)
        {
            var operation = SceneManager.UnloadSceneAsync(sceneName);
            if (operation != null)
            {
                unloadTasks.Add(WaitForAsyncOperation(operation));
                OnSceneUnloaded.Invoke(sceneName);
            }
        }

        await Task.WhenAll(unloadTasks);

        // Ensure unused assets are unloaded
        await Resources.UnloadUnusedAssets();
        Debug.Log("All non-essential scenes have been unloaded.");
    }

    private async Task WaitForAsyncOperation(AsyncOperation operation)
    {
        while (!operation.isDone)
        {
            await Task.Yield(); // Yield to avoid blocking
        }
    }
}

public readonly struct AsyncOperationGroup {
    public readonly List<AsyncOperation> Operations;

    public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
    public bool IsDone => Operations.All(o => o.isDone);

    public AsyncOperationGroup(int initialCapacity) {
        Operations = new List<AsyncOperation>(initialCapacity);
    }
}