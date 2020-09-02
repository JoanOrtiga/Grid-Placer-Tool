using GridPlacing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace GridPlacing
{
    public class AttachToGrid : MonoBehaviour
    {
        /// <summary>
        /// Which grid belongs to.
        /// </summary>
        [SerializeField] private int gridID = 0;

        /// <summary>
        /// Is the object moveable with mouse to attach grid?
        /// </summary>
        public bool MousePlacement = true;
        
        /// <summary>
        /// Bool that tells if object is placed or is moving.
        /// </summary>
        public bool placed = false;

        /// <summary>
        /// Event invoked when object is Placed.
        /// </summary>
        public UnityEvent isPlacedOnGrid;


        

        /// <summary>
        /// Which button to place object.
        /// </summary>
        public KeyCode placingButton = KeyCode.Mouse0;

        public bool checkCollision;
        public LayerMask ignoreLayers;


        /// <summary>
        /// Grid reference. It comes from the ID that you choose.
        /// </summary>
        private GridPlacer attachGrid;

        private Collider2D colliderReference;
        private GameObject colliderChecker;

        private void Awake()
        {
            if (checkCollision)
            {
                if (this.GetComponent<Collider2D>() != null)
                {
                    colliderReference = GetComponent<Collider2D>();
                    
                }
                else
                {
                    print("If CheckCollision is ON, make sure to have a collision on gameObject so it can check if it fits.");
                }
            }
        }

        private void OnEnable()
        {
            //Get a reference to the GridPlacer by ID.
            attachGrid = MasterGrid.FindGridByID(gridID);

            //Every time you enable script, you start moving it, so its not placed anymore.
            placed = false;
        }

        public int SetGridID
        {
            set
            {
                //If GridID is changed from another script, reload the attached grid.
                gridID = value;
                attachGrid = MasterGrid.FindGridByID(gridID);
            }
        }

        private void Update()
        {
            if (MousePlacement) //If mousePlacement is enabled.
            {

                //Change position of the object relative to MousePosition and Grid options.
                transform.position = attachGrid.GetPositionInGridByMouse();

                if (Input.GetKey(placingButton)) //If placingButton is pressed, object is placed.
                {

                    placed = true; //Since now is placed, change the value of placed.
                    isPlacedOnGrid.Invoke(); //Invoke event.
                    this.enabled = false; //Disable script, no loger will be moving.
                }
            }
        }

        private bool CheckCollision()
        {
            return true;
        }
    }
}

