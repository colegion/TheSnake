using Helpers;
using UnityEditor;
using UnityEngine;

namespace LevelDesign
{
    [CustomEditor(typeof(LevelEditor))]
    public class CustomInspector : Editor
    {
        private LevelEditor _levelEditor;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _levelEditor = (LevelEditor)target;
            
            ConfigureGridSizeFields();
            ConfigureCreateGridButton();
            ConfigureAddWallButton();
            DisplayWallConfigs();
            
            
        }
        
        
        private void ConfigureGridSizeFields()
        {
            _levelEditor.width = EditorGUILayout.IntField("Grid Width", _levelEditor.width);
            _levelEditor.height = EditorGUILayout.IntField("Grid Height", _levelEditor.height);
        }

        private void ConfigureCreateGridButton()
        {
            if (GUILayout.Button("Create Grid"))
            {
                _levelEditor.CreateGrid();
            }
        }

        private void ConfigureAddWallButton()
        {
            if (!_levelEditor.gridGenerated) return;
            //_levelEditor.selectedType = (WallType)EditorGUILayout.EnumPopup("Wall Type", _levelEditor.selectedType);
            if (GUILayout.Button("Add Wall"))
            {
                _levelEditor.AddWall();
            }
        }

        private void DisplayWallConfigs()
        {
            if (_levelEditor.walls.Count <= 0) return;
            EditorGUILayout.LabelField("Spawned Walls", EditorStyles.boldLabel);

            for (int i = _levelEditor.walls.Count - 1; i >= 0; i--)
            {
                var wall = _levelEditor.walls[i];
                WallType newType = (WallType)EditorGUILayout.EnumPopup("Wall Type", wall.GetWallType());
                int newX = EditorGUILayout.IntField("Wall X Coordinate", wall.X);
                int newY = EditorGUILayout.IntField("Wall Y Coordinate", wall.Y);

                if (newX != wall.X) wall.SetXCoordinate(newX);
                if (newY != wall.Y) wall.SetYCoordinate(newY);
                if(newType != wall.GetWallType()) wall.SetWallType(newType);

                if (GUILayout.Button("Save Wall"))
                {
                    if (IsInBounds(wall.X, wall.Y))
                    {
                        wall.SetTransform();
                    }
                    else
                    {
                        Debug.LogWarning($"Given coordinates for wall is not on bounds! X: {wall.X} Y: {wall.Y}");
                    }
                }

                if (GUILayout.Button("Delete Wall"))
                {
                    _levelEditor.RemoveWall(wall);
                }
            }
        }


        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < _levelEditor.width && y >= 0 && y < _levelEditor.height;
        }
    }
}
