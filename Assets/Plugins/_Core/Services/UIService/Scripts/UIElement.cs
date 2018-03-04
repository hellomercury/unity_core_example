﻿using System;
using System.Collections;
using System.Collections.Generic;
using Core.Services.Audio;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Services.UI
{
	/// <summary>
	/// UIElement is the base class for any UI element that is controlled by the _uiService. 
	/// </summary>
	public abstract class UIElement : CoreBehaviour
	{
		[Inject]
		protected UIService uiService;

		[Inject]
		protected AudioService audioService;

		public UIElementTransitionOptions inTransition, outTransition;
		public RectTransform RTransform { get { return GetComponent<RectTransform>(); } }

		protected Subject<UIElement> onOpened = new Subject<UIElement>();
		public IObservable<UIElement> OnOpened { get { return onOpened; } }

		protected Subject<UIElement> onClosed = new Subject<UIElement>();
		public IObservable<UIElement> OnClosed { get { return onClosed; } }

		protected override void Awake() { }

		/// <summary>
		/// Triggers after the transition on Show ends. 
		/// </summary>
		/// <param name="observer"></param>
		protected abstract void OnElementShow();

		/// <summary>
		/// Triggers after the transition on Hide ends. 
		/// </summary>
		protected abstract void OnElementHide();

		protected override void Start()
		{
			base.Start();

			Show().Subscribe(OnWindowOpened);
		}

		/// <summary>
		/// Shows the UI Element and performs any transition 
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<UIElement> Show()
		{
			return Observable.Create<UIElement>(
				(IObserver<UIElement> observer) =>
				{
					var subject = new Subject<UIElement>();

					if (inTransition.transitionSound)
						audioService.PlayClip(inTransition.transitionSound);

					Action<UIElement> OnShow = uiElement =>
					{
						observer.OnNext(this);
						observer.OnCompleted();

						OnElementShow();
					};

					if (inTransition != null && !inTransition.transitionType.Equals(TransitionType.NotUsed))
						return inTransition.PlayTransition(this).Subscribe(OnShow);
					else
						return subject.Subscribe(OnShow);
				});
		}

		/// <summary>
		/// Hides the UI Element after playing the out transition. 
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<UIElement> Hide(bool isClose = false)
		{
			return Observable.Create<UIElement>(
				(IObserver<UIElement> observer) =>
				{
					var subject = new Subject<UIElement>();

					//if isClose wait until PlayClip AND PlayTransition are done before doing OnNext
					//for this PlayClip needs to be an observable
					if (outTransition.transitionSound)
						audioService.PlayClip(outTransition.transitionSound);

					Action<UIElement> OnHide = uiElement =>
					{
						observer.OnNext(this);
						observer.OnCompleted();

						OnElementHide();
					};

					if (outTransition != null && !outTransition.transitionType.Equals(TransitionType.NotUsed))
						return outTransition.PlayTransition(this, true).Subscribe(OnHide);
					else
						return subject.Subscribe(OnHide);
				});
		}

		/// <summary>
		/// Close window and tells iservice to destroy the uielement and unload the asset 
		/// </summary>
		/// <returns> Observable </returns>
		public virtual IObservable<UIElement> Close()
		{
			return Observable.Create<UIElement>(
				(IObserver<UIElement> observer) =>
				{
					Action<UIElement> OnCLosed = window =>
					{
						observer.OnNext(this);
						observer.OnCompleted();

						OnWindowClosed(this);
					};

					return Hide(true).Subscribe(OnCLosed);
				});
		}

		/// <summary>
		///Communicates to _uiService that the window is opened and active
		/// </summary>
		/// <param name="uiElement"></param>
		protected virtual void OnWindowOpened(UIElement uiElement)
		{
			onOpened.OnNext(this);
			onOpened.OnCompleted();
		}

		/// <summary>
		/// Communicates to _uiService that the window is ready to be destroyed and the asset unloaded 
		/// </summary>
		/// <param name="uiElement"></param>
		protected virtual void OnWindowClosed(UIElement uiElement)
		{
			onClosed.OnNext(this);
			onClosed.OnCompleted();
		}
	}
}