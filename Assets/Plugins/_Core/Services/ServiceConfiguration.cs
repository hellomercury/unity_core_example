﻿using UnityEngine;

namespace Core.Services
{
	public struct ConfigurationServiceName
	{
		public string name;
		public Service service;

		public ConfigurationServiceName(string n, Service svc)
		{
			name = n;
			service = svc;
		}
	}

	public abstract class ServiceConfiguration : ScriptableObject
	{
		public abstract Service ServiceClass { get; }

		/// <summary>
		/// Create service. This initializes and starts the service.
		/// </summary>
		/// <returns> Observable </returns>
		//public IObservable<ConfigurationServiceName> CreateService()
		//{
		//	return Observable.Create<ConfigurationServiceName>(
		//		(IObserver<ConfigurationServiceName> observer) =>
		//		{
		//			var subject = new Subject<ConfigurationServiceName>();

		// IService service = ServiceClass; if (service == null) observer.OnError(new
		// System.Exception("Failed to create service " + name));

		// observer.OnNext(new ConfigurationServiceName(name, service)); observer.OnCompleted();

		//			return subject;
		//		});
		//}
	}
}