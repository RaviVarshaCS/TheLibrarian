using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LibraryDataLoader : MonoBehaviour {
    private static LibraryDataLoader _instance;

    public static LibraryDataLoader Instance {
        get {
            if (_instance == null) {
                GameObject obj = new GameObject("LibraryDataLoader");
                _instance = obj.AddComponent<LibraryDataLoader>();
            }
            return _instance;
        }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private Dictionary<int, DayData> _cachedDayData;

    public DayData LoadDayData(int currentDay) {
        EnsureDayDataIsCached(currentDay);
        return _cachedDayData[currentDay];
    }

    private void EnsureDayDataIsCached(int currentDay) {
        if (_cachedDayData == null) {
            _cachedDayData = new Dictionary<int, DayData>();
        }

        if (!_cachedDayData.ContainsKey(currentDay)) {
            string path = Path.Combine(Application.streamingAssetsPath, $"Day_{currentDay}.json");

            if (File.Exists(path)) {
                string json = File.ReadAllText(path);
                DayData dayData = JsonUtility.FromJson<DayData>(json);
                _cachedDayData[currentDay] = dayData;
            } else {
                Debug.LogError($"Day data file not found at path: {path}");
            }
        }
    }

    public void ClearCache() {
        _cachedDayData = null;
    }
}