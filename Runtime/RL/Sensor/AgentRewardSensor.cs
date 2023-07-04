using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ai4u
{
    public class AgentRewardSensor : AbstractSensor
    {
        private float rewardScale;

        public AgentRewardSensor()
        {
            SetKey("reward");
            SetSensorType(SensorType.sfloat);
            SetShape(new int[1]{1});
        }

        public override void OnSetup(Agent agent)
        {
            SetAgent( (BasicAgent) agent );
        }

        public void SetRewardScale(float rs)
        {
            this.rewardScale = rs;
        }

        public override float GetFloatValue()
        {
            return GetAgent().Reward * rewardScale;
        }
    }
}
