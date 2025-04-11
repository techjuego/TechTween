using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(AceTweener))]
public class AceTweenerEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		AceTweener tween = (AceTweener)target;
		EditorGUILayout.LabelField (" Tweeeeeeeeening :) ");
		EditorGUILayout.Space ();
		SerializedProperty PlayTweenOnStart = serializedObject.FindProperty ("PlayTweenOnStart");

		EditorGUILayout.PropertyField (PlayTweenOnStart);
		serializedObject.ApplyModifiedProperties ();
		EditorGUILayout.Space ();
		tween.selectedType =	(TweenerType)EditorGUILayout.EnumPopup ("Select Tween Type", tween.selectedType);
		tween.ease =	(Ease)EditorGUILayout.EnumPopup ("Select Tween Motion ", tween.ease);
		EditorGUILayout.Space ();
		SerializedProperty RectTrans = serializedObject.FindProperty ("rect");

		EditorGUILayout.PropertyField (RectTrans);


		serializedObject.ApplyModifiedProperties ();
		switch (tween.selectedType) {
		case TweenerType.Position:
			 
			SerializedProperty UseSlidersForPosition = serializedObject.FindProperty ("UseSlidersForPosition");

			EditorGUILayout.PropertyField (UseSlidersForPosition);
			serializedObject.ApplyModifiedProperties ();

			if (tween.UseSlidersForPosition) {
				 
				SerializedProperty OutX = serializedObject.FindProperty ("OutX");
				EditorGUILayout.PropertyField (OutX);  
				SerializedProperty OutY = serializedObject.FindProperty ("OutY");
				EditorGUILayout.PropertyField (OutY);  
				serializedObject.ApplyModifiedProperties ();

			} else {
				SerializedProperty tweenBegin = serializedObject.FindProperty ("tweenBegin");
				EditorGUILayout.PropertyField (tweenBegin);   
				serializedObject.ApplyModifiedProperties ();
				SerializedProperty tweenFinish = serializedObject.FindProperty ("tweenFinish");
				EditorGUILayout.PropertyField (tweenFinish);   
				serializedObject.ApplyModifiedProperties ();
			}
			 
		
			break;
		case TweenerType.Scale:
			SerializedProperty useScaleAmount = serializedObject.FindProperty ("useScaleAmount");

			EditorGUILayout.PropertyField (useScaleAmount);
			serializedObject.ApplyModifiedProperties ();

			 
			if (tween.useScaleAmount) {
				tween.ScaleAmount = EditorGUILayout.Slider ("scale ", tween.ScaleAmount, -10, 10);
			} else {
				SerializedProperty tweenBegin = serializedObject.FindProperty ("tweenBegin");
				EditorGUILayout.PropertyField (tweenBegin);   
				serializedObject.ApplyModifiedProperties ();
				SerializedProperty tweenFinish = serializedObject.FindProperty ("tweenFinish");
				EditorGUILayout.PropertyField (tweenFinish);   
				serializedObject.ApplyModifiedProperties ();
			 
				 
			}
			
			break;

		}
		EditorGUILayout.Space ();
		tween.delayTime = EditorGUILayout.FloatField ("Tween Delay time", tween.delayTime);
		tween.timetoFinishTween = EditorGUILayout.FloatField ("Tween life time", tween.timetoFinishTween);
		
		 
		SerializedProperty loopTween = serializedObject.FindProperty ("loopTween");
		EditorGUILayout.PropertyField (loopTween);    
		serializedObject.ApplyModifiedProperties ();
		if (tween.loopTween == false) {
			EditorGUI.indentLevel++;
			tween.tweenRepeatCount = EditorGUILayout.IntSlider ("Tweeen Repeat Count ", tween.tweenRepeatCount, 0, 10);
			EditorGUI.indentLevel--;
		}
		 
		tween.TweenRepeatDelay = EditorGUILayout.Slider ("TweenRepeatDelay ", tween.TweenRepeatDelay, 0, 1);
		tween.canDisableScript = EditorGUILayout.ToggleLeft ("After Complete Disable this Script", tween.canDisableScript);

		
	 
		SerializedProperty canSendMessageOnComplete = serializedObject.FindProperty ("canSendMessageOnComplete");
		EditorGUILayout.PropertyField (canSendMessageOnComplete);

		serializedObject.ApplyModifiedProperties ();
		if (tween.canSendMessageOnComplete) {
			EditorGUI.indentLevel++;
			SerializedProperty sprop = serializedObject.FindProperty ("UnityAction");

			EditorGUILayout.PropertyField (sprop);

			serializedObject.ApplyModifiedProperties ();


		}

		 
//		if (RectTrans != null) {
//			SerializedProperty tweenBegin = serializedObject.FindProperty ("tweenBegin");
//			EditorGUILayout.PropertyField (tweenBegin);   
//			serializedObject.ApplyModifiedProperties ();
//			SerializedProperty tweenFinish = serializedObject.FindProperty ("tweenFinish");
//			EditorGUILayout.PropertyField (tweenFinish);   
//			serializedObject.ApplyModifiedProperties ();
//
//
//		}
		EditorUtility.SetDirty (tween);
		serializedObject.ApplyModifiedProperties ();
		
	}



}
