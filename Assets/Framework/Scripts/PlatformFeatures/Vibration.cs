using UnityEngine;
using System.Collections;
using System;
 
public static class Vibration
{

	#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
  

#else
	public static AndroidJavaClass unityPlayer;
	public static AndroidJavaObject currentActivity;
	public static AndroidJavaObject vibrator;
	#endif

	 


	public static void Vibrate (long milliseconds)
	{ 
		#if UNITY_ANDROID
		vibrator.Call ("vibrate", milliseconds);
		#endif

	
	}

 
	 
	public static void Cancel ()
	{
		#if UNITY_ANDROID
		vibrator.Call ("cancel");
		#endif
	
	}

	 
}