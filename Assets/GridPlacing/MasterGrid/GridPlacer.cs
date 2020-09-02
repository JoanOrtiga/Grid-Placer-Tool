using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


namespace GridPlacing
{
    [RequireComponent(typeof(Grid))]
    public class GridPlacer : MonoBehaviour
    {
        /// <summary>
        /// Checks if grid is 2D or 3D. False if 3D, True if 2D.
        /// </summary>
        public bool grid2D = false;

        /// <summary>
        /// Size of cells in 2D grid
        /// </summary>
        public Vector2 gridCellSize2D = Vector2.one;

        /// <summary>
        /// Size of cells in 3D grid
        /// </summary>
        public Vector3 gridCellSize3D = Vector2.one;

        /// <summary>
        /// Cell Gap in 2D grid
        /// </summary>
        public Vector2 gridCellGap2D = Vector2.zero;

        /// <summary>
        /// Cell Gap in 3D grid
        /// </summary>
        public Vector3 gridCellGap3D = Vector2.zero;

        /// <summary>
        /// Layout for Grid
        /// </summary>
        public Grid.CellLayout gridCellLayout;

        /// <summary>
        /// Cell Swizzle for grid
        /// </summary>
        public Grid.CellSwizzle gridCellSwizzle;

        /// <summary>
        /// Grid Reference ID
        /// </summary>
        public int gridID;

        /// <summary>
        /// Draw Visual Grid Points
        /// </summary>
        public bool drawGridPoints = true;
        
        /// <summary>
        /// Color of Grid Points
        /// </summary>
        public Color gridPointsColor = Color.yellow;

        /// <summary>
        /// Radius of Grid Points
        /// </summary>
        public float radiusGridPoints = 0.1f;

        /// <summary>
        /// Keep object in grid if grid is moved.
        /// </summary>
        public bool keepObjectInGrid = true;

        /// <summary>
        /// Place Object Relative To Grid Position
        /// </summary>
        public bool placeObjectRelativeToGridPosition = true;

        private Grid grid;

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void OnDrawGizmos()
        {
            if (drawGridPoints)
            {
                Gizmos.color = gridPointsColor;
                for (int i = -20; i < 20; i++)
                {
                    for (int e = -20; e < 20; e++)
                    {
                        Gizmos.DrawSphere(new Vector2(i, e), radiusGridPoints);
                    }
                }
            }
        }

        public Vector3 GetPositionInGrid(Vector3 position)
        {
            Vector3 newPos = new Vector3();

            switch (gridCellLayout)
            {
                case GridLayout.CellLayout.Rectangle:
                    break;
                case GridLayout.CellLayout.Hexagon:
                    break;
                case GridLayout.CellLayout.Isometric:
                    break;
                case GridLayout.CellLayout.IsometricZAsY:
                    break;
            }

            return newPos;
        }

        public Vector3 GetPositionInGridByMouse()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(keepObjectInGrid)
                mousePosition -= (Vector2)transform.position; //If grid isn't x=0, y=0

            //We find nearest point where position can attach to.
            int xCount = Mathf.RoundToInt(mousePosition.x / grid.cellSize.x);
            int yCount = Mathf.RoundToInt(mousePosition.y / grid.cellSize.y);

            Vector3 result = new Vector2(xCount * gridCellSize2D.x, yCount * gridCellSize2D.y);

            if(placeObjectRelativeToGridPosition)
                result += transform.position; //If grid isn't x=0, y=0

            switch (gridCellLayout)
            {
                case GridLayout.CellLayout.Rectangle:
                    break;
                case GridLayout.CellLayout.Hexagon:
                    break;
                case GridLayout.CellLayout.Isometric:
                    break;
                case GridLayout.CellLayout.IsometricZAsY:
                    break;
            }

            return result;
        }
    }
}
