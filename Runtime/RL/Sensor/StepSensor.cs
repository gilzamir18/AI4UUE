using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ai4u 
{
    public class StepSensor : AbstractSensor
    {
        public StepSensor()
        {
            SetKey("steps");
            SetSensorType(SensorType.sint);
            SetShape(new int[1]{1});
        }

        public override int GetIntValue()
        {
            return GetAgent().NSteps;    
        }
    }
}