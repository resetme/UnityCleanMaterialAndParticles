using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System.IO;
 

public class LoadAssetBundle : MonoBehaviour
{
	private bool isShaderLoaded = false;
	private bool isWeaponLoaded = false;
	private bool isEffectLoad = false;
	private AssetBundle loadWeapon;
	private AssetBundle loadEffectTest;
	private AssetBundle loadMesh;
	
	// Use this for initialization
	public void LoadBundle () 
	{
		//First load Shaders and Warm Up
		if (!isShaderLoaded)
		{
			isShaderLoaded = true;
			var loadShader = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "shader"));
		
			if (loadShader == null) 
			{
				Debug.Log("Failed to load AssetBundle!");
				return;
			}
		
			ShaderVariantCollection variant = (loadShader.LoadAsset<ShaderVariantCollection>("LoaderShader"));
			if (!variant.isWarmedUp)
			{
				variant.WarmUp();
			}
		}


		if (!isWeaponLoaded)
		{
			isWeaponLoaded = true;
			//Load Weapons
			loadWeapon = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "weapon"));

			if (loadWeapon == null)
			{
				Debug.Log("Failed to load AssetBundle!");
				return;
			}
		
			//loadWeapon.Unload(true);
		}

		if (!isEffectLoad)
		{
			isEffectLoad = true;
			loadEffectTest = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "effect"));
			//loadMesh = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "mesh"));

			if (loadEffectTest == null)
			{
				Debug.Log("Failed to load AssetBundle!");
				return;
			}
		}
	}

	public void LoadSameMaterial()
	{
		
		var weaponSameMaterial = loadWeapon.LoadAsset<GameObject>("A");
		Instantiate(weaponSameMaterial);
	}
	
	public void LoadDmMaterial()
	{
		var weaponDmMaterial = loadWeapon.LoadAsset<GameObject>("A");
		Instantiate(weaponDmMaterial);
		
	}
	
	public void LoadNoMaterial()
	{
		var weaponNoMaterial = loadWeapon.LoadAsset<GameObject>("A");
		Instantiate(weaponNoMaterial);
	}

	public void LoadStdMaterial()
	{
		var weaponStdMaterial = loadWeapon.LoadAsset<GameObject>("A");
		Instantiate(weaponStdMaterial);
	}
	
	public void LoadCubeTestMaterial()
	{
		var weaponCubeMaterial = loadWeapon.LoadAsset<GameObject>("A");
		Instantiate(weaponCubeMaterial);
	}

	public void LoadSameName()
	{
		var weaponSameMaterial = loadWeapon.LoadAsset<GameObject>("A");
		Instantiate(weaponSameMaterial);
		
	}

	public void LoadTestOptimization()
	{
		LoadTestOptimization1();
		LoadTestOptimization2();
		LoadTestOptimization3();
	}
	
	public void LoadTestOptimization1()
	{
		var effectTest = loadEffectTest.LoadAsset<GameObject>("TestOptimization1");
		Instantiate(effectTest);
	}
	
	public void LoadTestOptimization2()
	{
		var effectTest = loadEffectTest.LoadAsset<GameObject>("TestOptimization2");
		Instantiate(effectTest);
	}
	
	public void LoadTestOptimization3()
	{
		var effectTest = loadEffectTest.LoadAsset<GameObject>("TestOptimization3");
		Instantiate(effectTest);
	}

	public void UnloadEffectTest()
	{
		isEffectLoad = false;
		loadEffectTest.Unload(true);
	}
	
	public void UnloadWeaponBundle()
	{
		isWeaponLoaded = false;
		loadWeapon.Unload(true);
		
	}
}
