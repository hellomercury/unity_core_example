﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Core.Assets
{
	/// <summary>
	/// Class provides a single point for loading and unloading asset bundles.
	/// Each method follows the same patterns as the AssetBundle methods counterparts, but instead 
	/// return the desired objects in a callback.
	/// </summary>
	public class LoadedBundle
	{
		protected AssetBundle assetBundle;
		protected ManifestInfo manifestInfo;

		public AssetBundle AssetBundle { get { return assetBundle; } }
		public ManifestInfo ManifestInfo { get { return manifestInfo; } }

		public string Name { get { return assetBundle.name; } }

		public LoadedBundle(AssetBundle asset)
		{
			assetBundle = asset;
			// if (asset)
			// 	manifest = asset.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
		}

		public LoadedBundle(AssetBundle asset, ManifestInfo info)
		{
			assetBundle = asset;
			manifestInfo = info;
		}

		public void Unload(bool unloadAll = false)
		{
			if (assetBundle)
				assetBundle.Unload(unloadAll);
		}

		public IObservable<T> LoadAssetAsync<T>(string name)where T : UnityEngine.Object
		{
			Debug.Log(("LoadedBundle: Asynchronously loading asset: " + name).Colored(Colors.yellow));

			return Observable.FromCoroutine<T>((observer, cancellationToken)=> RunAssetBundleRequestOperation<T>(assetBundle.LoadAssetAsync(name), observer, cancellationToken));
		}

		public IEnumerator RunAssetBundleRequestOperation<T>(UnityEngine.AssetBundleRequest asyncOperation, IObserver<T> observer, CancellationToken cancellationToken)where T : UnityEngine.Object
		{
			while (!asyncOperation.isDone && !cancellationToken.IsCancellationRequested)
				yield return null;

			if (!cancellationToken.IsCancellationRequested)
			{
				if (!asyncOperation.asset)
					observer.OnError(new System.Exception("RunAssetBundleRequestOperation: Error getting bundle."));

				//Current use case returns the component, if this changes then deal with it downstream but for now this should be ok
				var go = asyncOperation.asset as GameObject;
				var comp = go.GetComponent<T>();

				observer.OnNext(comp);
				observer.OnCompleted();
			}
		}
	}
}