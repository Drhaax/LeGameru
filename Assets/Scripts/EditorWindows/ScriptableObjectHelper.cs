using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectHelper : EditorWindow
{
	[MenuItem ("CustomTools/ScriptableObjectHelper")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(ScriptableObjectHelper));
	}

	private void OnGUI() {
		if (GUILayout.Button("ReBuild View & Popup dicts")) {
			var Views = Resources.Load<ViewContainer>("ScriptableObjects/Views");
			var Popups = Resources.Load<ViewContainer>("ScriptableObjects/Popups");
			var viewstrings = Directory.GetFiles("Assets/Prefabs/Views/").Where(f => Path.GetExtension(f).Contains(".prefab")).ToList();
			var popupStrings = Directory.GetFiles("Assets/Prefabs/Popups/").Where(f => Path.GetExtension(f).Contains(".prefab")).ToList();

			List<GameObject> viewPrefabs = new List<GameObject>();
			foreach (var v in viewstrings) {
				viewPrefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(v));
			}
			Views.RebuildList(viewPrefabs);

			List<GameObject> popupPrefabs = new List<GameObject>();
			foreach (var p in popupStrings) {
				popupPrefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(p));
			}
			Popups.RebuildList(popupPrefabs);
			EditorUtility.SetDirty(Views);
			EditorUtility.SetDirty(Popups);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		if (GUILayout.Button("DeletePlayerPrefs")) {
			PlayerPrefs.DeleteAll();
			using (StreamWriter sw = new StreamWriter("Assets/Resources/LocalDB/Player.json")) {
				sw.Write("");
			}
		}
	}
	


}
