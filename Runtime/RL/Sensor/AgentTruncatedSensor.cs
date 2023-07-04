using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ai4u
{
    public class AgentTruncatedSensor : AbstractSensor
    {
        public AgentTruncatedSensor()
        {
            SetKey("truncated");
            SetSensorType(SensorType.sbool);
            SetShape(new int[0]);
        }

        public override void OnSetup(Agent agent)
        {
            SetAgent((BasicAgent) agent);
        }

        public override bool GetBoolValue()
        {
            return GetAgent().Truncated;
        }
    }
}
