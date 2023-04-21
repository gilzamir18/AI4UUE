using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ai4u
{
    public class LinearRaycasting : Sensor
    {
        public int noObjectCode;
        public GameObject eye;
        public float fieldOfView = 180.0f;
        public float verticalShift = 0;
        public float horizontalShift = 0;
        public float maxDistance = 500f;
        public bool returnDepthMatrix = false;
        public int numberOfRays = 5;
        public bool automaticTagMapping = false;
        public int tagCodeDistance = 10;
        public  ObjectMapping[] objectMapping;
        public bool debugMode = false;

        private HistoryStack<float> stack;
        private int depth = 1;
        private float angleStep;
        private Dictionary<string, int> mapping;
        private float halfFOV = 90;
        
        public override void OnSetup(Agent agent)
        {
            type = SensorType.sfloatarray;
            shape = new int[1]{(int)numberOfRays};
            mapping = new Dictionary<string, int>();
            if (returnDepthMatrix)
            {
                depth += 1;
            }
            
            stack = new HistoryStack<float>(stackedObservations * shape[0] * depth);

            if (automaticTagMapping)
            {
                #if UNITY_EDITOR
                for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
                {
                        string tag = UnityEditorInternal.InternalEditorUtility.tags[i];
                        mapping[tag] = i * tagCodeDistance;
                }
                #endif
            }
            else
            {
                foreach(ObjectMapping obj in objectMapping)
                {
                    mapping[obj.tag] = obj.code;
                }
            }

            agent.AddResetListener(this);
        }

        public override void OnReset(Agent agent) 
        {
            
            stack = new HistoryStack<float>(stackedObservations * shape[0] * depth);
            mapping = new Dictionary<string, int>();
            foreach(ObjectMapping obj in objectMapping)
            {
                mapping[obj.tag] = obj.code;
            }
            GetFloatArrayValue();
        }

        void OnDrawGizmos()
        {

            if (numberOfRays > 1)
            {
                angleStep = fieldOfView/(numberOfRays - 1);
            }
            else
            {
                angleStep = 0;
            }

            halfFOV = fieldOfView/2;
            if (shape == null || stack == null)
            {
                type = SensorType.sfloatarray;
                shape = new int[1]{numberOfRays};
                stack = new HistoryStack<float>(stackedObservations * numberOfRays);
                mapping = new Dictionary<string, int>();
                foreach(ObjectMapping obj in objectMapping)
                {
                    mapping[obj.tag] = obj.code;
                }
                RayCasting();
            }
        }


        void RayCasting()
        {
            Vector3 pos = eye.transform.position;
            Vector3 fwd = eye.transform.forward;

            if (numberOfRays > 1)
            {
                angleStep = fieldOfView/(numberOfRays - 1);
            }
            else
            {
                angleStep = 0;
            }

            halfFOV = fieldOfView/2;
            
            for (uint i = 0; i < numberOfRays; i++)
            {
                float angle =  0;
                
                if (numberOfRays > 1)
                {
                    angle = i * angleStep - halfFOV;
                }
                
                Vector3 direction = Quaternion.Euler(0, angle + horizontalShift, verticalShift) * fwd;
                RaycastHit hitinfo;
                if (Physics.Raycast(pos, direction, out hitinfo, maxDistance))
                {
                    GameObject gobj = hitinfo.collider.gameObject;
                    string objtag = gobj.tag;
                    if (mapping.ContainsKey(objtag))
                    {
                        int code = mapping[objtag];
                        stack.Push(code);
                        if (returnDepthMatrix)
                        {
                            stack.Push(hitinfo.distance);
                        }
                    }
                    else 
                    {
                        stack.Push(noObjectCode);
                        if (returnDepthMatrix)
                        {
                            stack.Push(-1);
                        }
                    }
                    if (debugMode)
                    {
                        Debug.DrawRay(pos, direction * hitinfo.distance, Color.red);
                    }
                }
                else
                {
                    if (debugMode)
                    {
                        Debug.DrawRay(pos, direction * hitinfo.distance, Color.yellow);
                    }
                    stack.Push(noObjectCode);
                    if (returnDepthMatrix)
                    {
                        stack.Push(-1);
                    }
                }
            }
        }

        public override float[] GetFloatArrayValue()
        {
            RayCasting();
            return stack.Values;
        }
    }
}
