using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ai4u 
{
    public class IDSensor : AbstractSensor
    {

        public IDSensor()
        {
            SetKey("id");
            SetSensorType(SensorType.sstring);
            SetShape(new int[1]{1});
         
        }

        public override void OnSetup(Agent agent)
        {
            SetAgent((BasicAgent)agent);
        }

        public override string GetStringValue()
        {
            return GetAgent().ID;
        }
    }
}
