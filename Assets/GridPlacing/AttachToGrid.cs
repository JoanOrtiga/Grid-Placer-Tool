using GridPlacing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        public bool mousePlacement = true;

        public bool mouseMoving;
        public bool keyboardMoving;

        public KeyCode moveUp = KeyCode.U;
        public KeyCode moveDown = KeyCode.N;
        public KeyCode moveRight = KeyCode.K;
        public KeyCode moveLeft = KeyCode.H;
        public KeyCode moveUpRight = KeyCode.I;
        public KeyCode moveUpLeft = KeyCode.Y;
        public KeyCode moveDownRight = KeyCode.M;
        public KeyCode moveDownLeft = KeyCode.B;


        public bool keyboardPlacement = false;

        /// <summary>
        /// Which button to place object.
        /// </summary>
        public KeyCode mousePlacingButton = KeyCode.Mouse0;

        public KeyCode keyboardPlacingButton = KeyCode.J;

        public bool confineMouse = false;






        /// <summary>
        /// Bool that tells if object is placed or is moving.
        /// </summary>
        public bool placed = false;

        /// <summary>
        /// Event invoked when object is Placed.
        /// </summary>
        public UnityEvent isPlacedOnGrid;







        public bool automaticDetection;
        public CollisionMode modeOfCollision;
        public LayerMask ignoreLayers;
        private Collider2D colliderReference;

        public CollisionType collisionType;



        /// <summary>
        /// Grid reference. It comes from the ID that you choose.
        /// </summary>
        private GridPlacer attachGrid;

        private void Start()
        {
            if (attachGrid.grid2D)
            {
                if (automaticDetection)
                {
                    if (modeOfCollision == CollisionMode.Simple)
                    {
                        if (this.GetComponent<Collider2D>() != null)
                        {
                            colliderReference = GetComponent<Collider2D>();

                            if (colliderReference is BoxCollider2D)
                            {
                               BoxCollider2D boxC = colliderReference as BoxCollider2D;
                            }
                            else if (colliderReference is CircleCollider2D)
                            {

                            }
                            else if (colliderReference is CapsuleCollider2D)
                            {

                            }
                        }
                    }
                }
            }





            if (confineMouse)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        private void OnEnable()
        {
            //Get a reference to the GridPlacer by ID.
            attachGrid = MasterGrid.FindGridByID(gridID);

            //Every time you enable script, you start moving it, so its not placed anymore.
            placed = false;
        }

        public int GridID
        {
            set
            {
                //If GridID is changed from another script, reload the attached grid.
                gridID = value;

                attachGrid = MasterGrid.FindGridByID(gridID);
            }
            get
            {
                return gridID;
            }
        }

        

        private void Update()
        {
            if (mousePlacement) //If mousePlacement is enabled.
            {

                //Change position of the object relative to MousePosition and Grid options.
                transform.position = attachGrid.GetPositionInGridByMouse();

                if (Input.GetKey(mousePlacingButton)) //If placingButton is pressed, object is placed.
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






    public enum CollisionMode
    {
        None, Simple, Complex
    }

    public enum CollisionType
    {
        Box, Circle, Cylinder
    }
}

