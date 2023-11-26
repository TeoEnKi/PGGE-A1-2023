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


            //!!!GET THE OFFSET????

            CharacterController character = mPlayerTransform.GetComponent<CharacterController>();
            int layerMask = ~LayerMask.GetMask("Player");

            Vector3 characterPos = mPlayerTransform.position;
            characterPos.y += character.height;
            Debug.Log("posistion of player: "+ characterPos);
            RaycastHit[] hits = Physics.RaycastAll(characterPos, Vector3.Normalize( mCameraTransform.position - characterPos), Vector3.Distance(mCameraTransform.position, characterPos),layerMask);
            float smallestDist = Vector3.Distance(mCameraTransform.position, mPlayerTransform.position);
            //zero means there is no intersection
            Vector3 nearestIntersection = Vector3.zero;

            //check nearest intersection
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
            Debug.DrawLine(mCameraTransform.position, characterPos);
            Debug.Log("nearestIntersection" + nearestIntersection);

        }

        public abstract void Update();
    }
}
