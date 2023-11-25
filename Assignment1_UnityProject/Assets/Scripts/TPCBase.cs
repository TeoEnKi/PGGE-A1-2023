using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        public void RepositionCamera()
        {
            //https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
            //-------------------------------------------------------------------
            // Implement here.
            //-------------------------------------------------------------------
            //-------------------------------------------------------------------
            // Hints:
            //-------------------------------------------------------------------
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position two the nearest intersected point
            //-------------------------------------------------------------------


            //!!!GET THE PLAYER"S ACTUAL CENTER WORLD POS


            CharacterController character = mPlayerTransform.GetComponent<CharacterController>();
            int layerMask = ~LayerMask.GetMask("Player");
            Vector3 characterPos = mPlayerTransform.GetChild(0).transform.position;

            RaycastHit[] hits = Physics.RaycastAll(mCameraTransform.position, Vector3.Normalize(characterPos - mCameraTransform.position), Vector3.Distance(mCameraTransform.position, mPlayerTransform.position),layerMask);
            float smallestDist = Vector3.Distance(mCameraTransform.position, mPlayerTransform.position);
            //zero means there is no intersection
            Vector3 nearestIntersection = Vector3.zero;

            foreach (RaycastHit hit in hits)
            {
                Debug.Log("hit: " + hit.collider.name);
                if (Vector3.Distance(hit.point, mPlayerTransform.position) < smallestDist && Vector3.Dot(mPlayerTransform.forward, hit.point-mPlayerTransform.position) < 0 )
                {
                    smallestDist = Vector3.Distance(hit.point, mPlayerTransform.position);
                    Debug.Log("smallestDist: " + smallestDist);
                    nearestIntersection = hit.point;
                }

            }
            if (nearestIntersection != Vector3.zero)
            {
                mCameraTransform.position = nearestIntersection;
            }
            Debug.DrawLine(mCameraTransform.position, Vector3.Normalize(characterPos - mCameraTransform.position) * Vector3.Distance(mCameraTransform.position, mPlayerTransform.position));

        }

        public abstract void Update();
    }
}
