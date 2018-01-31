using System;
using System.Collections;
using System.IO;
using Core.Service;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Assets
{
	public struct AssetOptions
	{
		private AssetCacheState assetCacheState;
		public AssetCacheState AssetCacheState { get { return assetCacheState; } }

		public AssetOptions(AssetCacheState sta)
		{
			assetCacheState = sta;
		}
	}

	public class BundleRequest
	{
		private AssetCategoryRoot assetCategory;
		public AssetCategoryRoot AssetCategory { get { return assetCategory; } }

		private string bundleName;
		public string BundleName { get { return bundleName; } }

		public string ManifestName { get { return bundleName + ".manifest"; } }

		private string assetName;
		public string AssetName { get { return assetName; } }

		public string ManifestAgeFile { get { return Application.persistentDataPath + "/" + BundleName + "age.json"; } }
		public string CachedManifestFile { get { return Application.persistentDataPath + "/" + ManifestName; } }

		public string AssetPath
		{
			get
			{
				if (AssetCategory.Equals(AssetCategoryRoot.None))
					return ServiceLocator.GetService<IAssetService>().AssetBundlesURL + BundleName + "?r=" + (UnityEngine.Random.value * 9999999); //this random value prevents caching on the web server
				else
					return ServiceLocator.GetService<IAssetService>().AssetBundlesURL + AssetCategory.ToString().ToLower()+ "/" + BundleName;
			}
		}

		public string ManifestPath
		{
			get
			{
				Debug.Log(("AssetBundleLoader: Loading Manifest " + ManifestName).Colored(Colors.aqua));

				if (AssetCategory.Equals(AssetCategoryRoot.None))
					return ServiceLocator.GetService<IAssetService>().AssetBundlesURL + ManifestName + "?r=" + (UnityEngine.Random.value * 9999999); //this random value prevents caching on the web server;
				else
					return ServiceLocator.GetService<IAssetService>().AssetBundlesURL + AssetCategory.ToString().ToLower()+ "/" + ManifestName;
			}
		}

		public string AssetPathFromLocalStreamingAssets
		{
			get
			{
				if (AssetCategory.Equals(AssetCategoryRoot.None))
					return BundleName;
				else
					return AssetCategory.ToString().ToLower()+ "/" + BundleName;
			}
		}

		public string AssetPathFromLocalStreamingAssetsManifest
		{
			get
			{
				if (AssetCategory.Equals(AssetCategoryRoot.None))
					return ManifestName;
				else
					return AssetCategory.ToString().ToLower()+ "/" + ManifestName;
			}
		}

		public AssetCacheState AssetCacheState { get { return ServiceLocator.GetService<IAssetService>().AssetCacheState; } }

		public BundleRequest(AssetCategoryRoot cat, string bundle, string asset)
		{
			assetCategory = cat;
			bundleName = bundle.ToLower();
			assetName = asset.ToLower();
		}
	}

	/// <summary>
	/// Bundle root or package containing the desired asset
	/// </summary>
	public enum AssetCategoryRoot
	{
		None,
		Configuration,
		Services,
		Levels,
		Scenes,
		Windows,
		Audio,
		Prefabs
	}

	/// <summary>
	/// Device type
	/// </summary>
	public enum AssetDeviceType
	{
		StandaloneOSXUniversal,
		StandaloneOSXIntel,
		StandaloneWindows,
		WebPlayer,
		WebPlayerStreamed,
		iOS,
		PS3,
		XBOX360,
		Android,
		StandaloneLinux,
		StandaloneWindows64,
		WebGL,
		WSAPlayer,
		StandaloneLinux64,
		StandaloneLinuxUniversal,
		WP8Player,
		StandaloneOSX,
		BlackBerry,
		Tizen,
		PSP2,
		PS4,
		PSM,
		XboxOne,
		SamsungTV,
		N3DS,
		WiiU,
		tvOS,
		Switch
	}

	/// <summary>
	/// Cache or no cache
	/// </summary>
	public enum AssetCacheState
	{
		Cache,
		NoCache
	}
}