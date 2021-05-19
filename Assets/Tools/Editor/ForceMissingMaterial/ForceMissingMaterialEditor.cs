using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Object = UnityEngine.Object;

namespace UnityTAUtils
{
	public class ForceMissingMaterialEditor : Editor
	{


		[MenuItem("Assets/Tools/Force Missing Material FBX (Folder)", true)]
		private static bool ForceMissingFolderValidation()
		{
			string path = String.Empty;

			Object obj = Selection.activeObject;
			path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

			if (!Directory.Exists(path))
			{
				return false;
			}
			else
			{
				return true;
			}

		}

		[MenuItem("Assets/Tools/Force Missing Material FBX (Folder)")]
		static void ForceMissingFolder()
		{
			string path = String.Empty;

			Object obj = Selection.activeObject;

			path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

			string[] guids = AssetDatabase.FindAssets("t: Model", new string[] {path});

			foreach (var gui in guids)
			{

				string assetPath = AssetDatabase.GUIDToAssetPath(gui);

				if (!assetPath.Contains("@"))
				{
					Object baseGo = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

					ForceMissing(baseGo);
				}

			}
		}

		[MenuItem("Assets/Tools/Force Missing Material FBX")]
		private static void ForceMissingPrefab()
		{
			Object[] baseGo = Selection.gameObjects;

			foreach (var obj in baseGo)
			{
				ForceMissing(obj);
			}

		}

		private static void ForceMissing(Object obj)
		{
			string path = AssetDatabase.GetAssetPath(obj);
			ModelImporter MI = AssetImporter.GetAtPath(path) as ModelImporter;

			MI.importMaterials = true;
			MI.materialLocation = ModelImporterMaterialLocation.External;
			MI.materialName = ModelImporterMaterialName.BasedOnModelNameAndMaterialName;
			MI.materialSearch = ModelImporterMaterialSearch.Local;
			MI.SaveAndReimport();

			GameObject loadFBX = AssetDatabase.LoadAssetAtPath<GameObject>(path);
			Renderer[] smrs = loadFBX.GetComponentsInChildren<Renderer>();

			foreach (var smr in smrs)
			{
				foreach (var mat in smr.sharedMaterials)
				{
					//if (mat.shader.name.Contains("Standard"))
					//{
					string matPath = AssetDatabase.GetAssetPath(mat);

					if (mat != null)
					{
						AssetDatabase.DeleteAsset(matPath);
					}
					// }

				}

			}

			//
			MI.materialLocation = ModelImporterMaterialLocation.InPrefab;
			MI.SaveAndReimport();

			AssetDatabase.Refresh();
		}
	}
}