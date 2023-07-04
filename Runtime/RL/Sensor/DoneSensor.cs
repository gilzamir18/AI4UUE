using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;

namespace ai4u
{
    public class DoneSensor : AbstractSensor
    {
        public DoneSensor()
        {
            SetKey("done");
            SetSensorType(SensorType.sbool);
            SetShape(new int[0]);
        }

        public override void OnSetup(Agent agent)
        {
            SetAgent((BasicAgent) agent);
        }

        public override bool GetBoolValue()
        {
            return GetAgent().Done;    
        }
    }
}
