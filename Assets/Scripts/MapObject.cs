using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MapObject : MonoBehaviour
{
    [Serializable]
    public struct MapObjects
    {
        public Vector3 position;
        public Quaternion rotation;
        public types type;

        public MapObjects(Vector3 pos, Quaternion rot, types name)
        {
            this.position = pos;
            this.rotation = rot;
            this.type = name;
        }
    }

    public enum types
    {
        StartLine,
        EndLine,
        Checkpoint,
        Bumper,
        Shooter,
        Fan,
        Trampoline,
        MedCircle,
        MedFlatRectangle,
        MedFlatSquare,
        SmallCircle,
        SmallFlatSquare,
        SmallFlatRectangle,
        SmallHill,
        SmallLongHill,
        Void,
        MovingPlatform,

    }


}