using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class Binder : MonoBehaviour {
  public Component SourceObject;
  public string SourcePropertyName;
  public Component TargetObject;
  public string TargetPropertyName;

  private FieldInfo SourceProperty;
  private FieldInfo TargetProperty;

	void Start () {
    SourceProperty = SourceObject.GetType().GetField(SourcePropertyName);
    TargetProperty = TargetObject.GetType().GetField(TargetPropertyName);

	}
	
	// Update is called once per frame
	void Update () {
    var value = SourceProperty.GetValue(SourceObject);
    var convertedValue = Convert.ChangeType(value, TargetProperty.FieldType);
    TargetProperty.SetValue(TargetObject, convertedValue);
	}
}
