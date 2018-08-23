using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapItem))]
[CanEditMultipleObjects]
public class _editorMapItem : Editor {

	//Mesmo esquema do _editorMapGenerator.

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MapItem mapItem = (MapItem)target;
		if(GUILayout.Button("Configure Self")){
			mapItem.ConfigureSelf ();
		}
	}
}