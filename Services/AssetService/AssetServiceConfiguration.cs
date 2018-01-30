﻿using System.Collections;
using System.Collections.Generic;
using Core.Service;
using UnityEngine;

namespace Core.Assets
{
	public class AssetServiceConfiguration : ServiceConfiguration
	{
		override protected IService ServiceClass { get { return new AssetService(); } }

		[SerializeField]
		protected string assetBundlesURL;
		public string AssetBundlesURL { get { return assetBundlesURL; } set { assetBundlesURL = value; } }

		[SerializeField]
		protected bool useStreamingAssets = false;
		public bool UseStreamingAssets { get { return useStreamingAssets; } set { useStreamingAssets = value; } }

		[SerializeField]
		protected bool useCache = true;
		public bool UseCache { get { return useCache; } set { useCache = value; } }

		[SerializeField]
		protected int manifestCachePeriod = 5;
		public int ManifestCachePeriod { get { return manifestCachePeriod; } set { manifestCachePeriod = value; } }
	}
}