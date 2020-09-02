using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GridPlacing
{
    public class MasterGrid : MonoBehaviour
    {
        public static MasterGrid instance;

        private GridPlacer[] gridsPlacer;

        private void Awake()
        {
            if(MasterGrid.instance == null)
            {
                MasterGrid.instance = this;
            }

            if (GameObject.FindObjectsOfType<GridPlacer>().Length == 0)
            {
                Debug.LogError("There are no grids in the scene");
            }
            else
            {
                instance.gridsPlacer = GameObject.FindObjectsOfType<GridPlacer>();
            }
        }


        public static GridPlacer FindGridByID(int id)
        {
            if (instance.gridsPlacer == null)
                return null;

            foreach (GridPlacer grid in instance.gridsPlacer)
            {

                
                if (grid.gridID == id)
                {
                    

                    return grid;
                }
            }

            Debug.LogError("AttachToGrid is asking for an ID that doesn't exist. Check that there is a grid with that ID.");
            return null;
        }
    }
}

