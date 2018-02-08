﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class Constants
	{
		//Service locator GameObject name
		public const string ServiceLocator = "ServiceLocator";

		//Name used for the pool container Gameobject. Each pool is going to have it's own container as _PooledObjects_OBJECTNAME
		public const string PooledObject = "_PooledObjects_";
	}
}