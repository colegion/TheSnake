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
            ConfigureAddSnakeButton();
            ConfigureAddWallButton();
            DisplayWallConfigs();
            ConfigureTargetCountField();
            ConfigureSaveAndPlayButton();
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

        private void ConfigureAddSnakeButton()
        {
            if (_levelEditor.snake == null)
            {
                if (GUILayout.Button("Add Snake"))
                {
                    _levelEditor.AddSnake();
                }
            }
            else
            {
                var snake = _levelEditor.snake;
                int newX = EditorGUILayout.IntField("Wall X Coordinate", snake.X);
                int newY = EditorGUILayout.IntField("Wall Y Coordinate", snake.Y);

                if (newX != snake.X && IsInWidthBound(newX))
                {
                    snake.SetXCoordinate(newX);
                    snake.SetTransform();
                }

                if (newY != snake.Y && IsInHeightBound(newY))
                {
                    snake.SetYCoordinate(newY);
                    snake.SetTransform();
                }

                if (GUILayout.Button("Rotate Snake"))
                {
                    snake.UpdateDirection();
                }
            }
        }

        private void ConfigureAddWallButton()
        {
            if (!_levelEditor.gridGenerated) return;
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

                if (newX != wall.X && IsInWidthBound(newX)) wall.SetXCoordinate(newX);
                if (newY != wall.Y && IsInHeightBound(newY)) wall.SetYCoordinate(newY);
                if(newType != wall.GetWallType()) wall.SetWallType(newType);

                if (GUILayout.Button("Delete Wall"))
                {
                    _levelEditor.RemoveWall(wall);
                }
            }
        }

        private void ConfigureTargetCountField()
        {
            _levelEditor.targetCount = EditorGUILayout.IntField("Target Count: ", _levelEditor.targetCount);
        }

        private void ConfigureSaveAndPlayButton()
        {
            if (GUILayout.Button("Save & Play"))
            {
                _levelEditor.SaveLevel();
            }
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < _levelEditor.width && y >= 0 && y < _levelEditor.height;
        }

        private bool IsInWidthBound(int x)
        {
            return x >= 0 && x < _levelEditor.width;
        }
        
        private bool IsInHeightBound(int y)
        {
            return y >= 0 && y < _levelEditor.height;
        }
    }
}
