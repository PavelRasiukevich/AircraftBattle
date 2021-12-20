using System.Collections.Generic;
using Assets.Scripts.AirCrafts;
using Assets.Scripts.Utils;
using Managers.Data.ScriptableObjects;
using TO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Managers.Data.CustomEditors
{
#if UNITY_EDITOR

    public class PlanesDataEditor : BaseCustomEditor
    {
        private PlanesDataScriptableObject _planesData;

        private ReorderableList reorderableList = null;

        private PlaneInfo _selectedPlane;

        [MenuItem("Window/Game Data/Planes")]
        static void Init() => GetWindow(typeof(PlanesDataEditor));

        private void OnEnable()
        {
            _planesData =
                AssetDatabase.LoadAssetAtPath(Const.PlanesDataPath,
                    typeof(PlanesDataScriptableObject)) as PlanesDataScriptableObject;
            reorderableList = new ReorderableList(_planesData.PlaneList, typeof(PlaneInfo));
            reorderableList.elementHeight =
                EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            reorderableList.onSelectCallback = list => _selectedPlane = _planesData.PlaneList[list.index];
            reorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Planes:", EditorStyles.boldLabel);
            reorderableList.drawElementCallback =
                (rect, index, isactive, isfocused) =>
                    EditorGUI.LabelField(rect,
                        _planesData.PlaneList[index].DisplayName,
                        index == 0 ? EditorStyles.boldLabel : EditorStyles.label);
        }

        private void DrawSelected()
        {
            if (_selectedPlane == null) return;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Plane: {_selectedPlane.DisplayName}", EditorStyles.boldLabel);

            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(true);
            if (_selectedPlane.ID == 0) _selectedPlane.ID = Random.Range(100000000, 999999999);
            EditorGUILayout.IntField("ID", _selectedPlane.ID);
            EditorGUI.EndDisabledGroup();
            _selectedPlane.DisplayName = EditorGUILayout.TextField("Name", _selectedPlane.DisplayName);
            _selectedPlane.IsViewInEditor =
                EditorGUILayout.Foldout(_selectedPlane.IsViewInEditor, $"Plane: {_selectedPlane.DisplayName}");

            if (_selectedPlane.IsViewInEditor)
            {
                EditorGUI.indentLevel++;
                _selectedPlane.Icon = (Sprite) EditorGUILayout.ObjectField("Icon", _selectedPlane.Icon,
                    typeof(Sprite),
                    allowSceneObjects: true);
                _selectedPlane.PlanePrefab = (AirCraft) EditorGUILayout.ObjectField("Prefab",
                    _selectedPlane.PlanePrefab,
                    typeof(AirCraft), allowSceneObjects: true);
                if (_selectedPlane.PlanePrefab != null)
                {
                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.IntField("Health", _selectedPlane.PlanePrefab.DataModel.Hp);
                    EditorGUILayout.FloatField("Speed", _selectedPlane.PlanePrefab.DataModel.MoveSpeed);
                    EditorGUILayout.FloatField("Mobility", _selectedPlane.PlanePrefab.DataModel.Mobility);
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.indentLevel--;
                }
                _selectedPlane.FirePower = EditorGUILayout.FloatField("Fire Power (for shop)", _selectedPlane.FirePower);
                _selectedPlane.GamePrice =
                    EditorGUILayout.FloatField("Game price", _selectedPlane.GamePrice);
                EditorGUI.EndDisabledGroup();
                EditorGUI.indentLevel--;
            }
        }

        private void OnGUI()
        {
            List<PlaneInfo> planesInEditor = new List<PlaneInfo>();
            reorderableList.DoLayoutList();
            DrawSelected();

            if (_planesData.PlaneList.Count > 0)
                planesInEditor.AddRange(_planesData.PlaneList);

            _planesData.PlaneList.Clear();
            _planesData.PlaneList.AddRange(planesInEditor);
            EditorGUI.indentLevel = 0;
            if (GUI.changed)
                EditorUtility.SetDirty(_planesData);
        }
    }
#endif
}