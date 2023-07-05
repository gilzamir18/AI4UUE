using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ai4u
{
    public enum SensorType {
        sfloat,
        sstring,
        sbool,
        sint,
        sbytearray,
        sfloatarray,
        sintarray
    }

    public interface ISensor: IAgentResetListener
    {
        public void SetAgent(BasicAgent own);
        public void OnSetup(Agent agent);
        public float GetFloatValue();
        public string GetStringValue();
        public bool GetBoolValue();
        public byte[] GetByteArrayValue();
        public int GetIntValue();
        public int[] GetIntArrayValue();
        public float[] GetFloatArrayValue();
        public SensorType GetSensorType();
        public string GetName();
        public string GetKey();
        public int[] GetShape();
        public bool IsState();
        public bool IsResetable();
        public bool IsActive();
        public bool IsInput();
        public int GetStackedObservations();
        public void SetKey(string newkey);
        public void SetShape(int[] newshape);
        public void SetIsActive(bool v);
        public void SetIsInput(bool v);
        public void SetStackedObservations(int so);
        public void SetSensorType(SensorType t);
        public float GetRangeMin();
        public float GetRangeMax();
        public void SetRange(float min, float max);
        public void SetIsResetable(bool v);
    }

    public abstract class AbstractSensor: ISensor
    {
        private SensorType type;
        private int[] shape;
        private BasicAgent agent;
        private string key;
        private string name;
        private int stackedObservations;
        private bool isActive;
        private bool isInput;
        private bool isState;
        private bool resetable;
        private float rangeMin;
        private float rangeMax;
        
        public void SetAgent(BasicAgent own)
        {
            this.agent = own;
        }

        public virtual void OnSetup(Agent agent)
        {
            this.agent = (BasicAgent) agent;
        }

        public virtual float GetFloatValue() {
            throw new System.NotSupportedException();
        }

        public virtual string GetStringValue() {
            throw new System.NotSupportedException();
        }

        public virtual bool GetBoolValue() {
            throw new System.NotSupportedException();
        }

        public virtual byte[] GetByteArrayValue() {
            throw new System.NotSupportedException();
        }

        public virtual int GetIntValue() {
            throw new System.NotSupportedException();
        }

        public virtual int[] GetIntArrayValue() {
            throw new System.NotSupportedException();
        }

        public virtual float[] GetFloatArrayValue() {
            throw new System.NotSupportedException();
        }

        public virtual SensorType GetSensorType()
        {
            return type;
        }

        public virtual string GetName()
        {
            return name;
        }

        public virtual string GetKey()
        {
            return key;
        }

        public virtual int[] GetShape()
        {
            return shape;
        }

        public virtual bool IsState()
        {
            return isState;
        }

        public bool IsInput()
        {
            return isInput;
        }

        public virtual bool IsResetable()
        {
            return resetable;
        }

        public virtual bool IsActive()
        {
            return isActive;
        }

        public virtual int GetStackedObservations()
        {
            return stackedObservations;
        }

        public BasicAgent GetAgent()
        {
            return this.agent;
        }

        public virtual void OnReset(Agent agent) 
        {
        }
        
        public void SetKey(string newkey)
        {
            this.key = newkey;
        }
        
        public void SetShape(int[] newshape) 
        {
            this.shape = newshape;
        }

        public void SetIsActive(bool v)
        {
            this.isActive = v;
        }

        public void SetIsInput(bool v)
        {
            this.isInput = v;
        }

        public void SetIsResetable(bool v)
        {
            this.resetable = v;
        }

        public void SetStackedObservations(int so)
        {
            this.stackedObservations = so;
        }

        public void SetSensorType(SensorType t)
        {
            this.type = t;
        }

        public float GetRangeMin()
        {
            return rangeMin;
        }

        public float GetRangeMax()
        {
            return rangeMax;
        }

        public void SetRange(float min, float max)
        {
            this.rangeMin = min;
            this.rangeMax = max;
        }
    }

    public class Sensor : MonoBehaviour, ISensor
    {
        [Tooltip("'perceptionKey' represents a unique key for an identifiable sensor component, which will be used by the controller to retrieve information from the sensor.")]
        public string perceptionKey;
        [Tooltip("The 'stackedObservation' property represents a collection of observations that have been stacked together in a specific format, where it allows multiple observations to be processed and analyzed as a single input.")]
        public int stackedObservations = 1;
        [Tooltip("If active, the sensor is processed and sent to the controller, otherwise it is as if it does not exist.")]
        public bool isActive = true;
        [Tooltip("The 'resetable' property indicates whether the associated component should be reset at the beginning of each episode, whenever the associated agent is reset.")]
        public bool resetable = true;
        [Tooltip(" The 'isInput' property is a boolean flag that indicates whether the associated component is an agent's input or not.")]
        public bool isInput = false;
        protected SensorType Type;
        protected bool isState;
        protected int[] Shape;
        protected BasicAgent agent;
        protected bool  normalized = true;
        protected float rangeMin = 0.0f;
        protected float rangeMax = 1.0f;


        public bool Normalized
        {
            get
            {
                return normalized;
            }
        }

        public bool IsInput()
        {
            return isInput;
        }

        public float GetRangeMin()
        {
            return rangeMin;
        }

        public float GetRangeMax()
        {
            return rangeMax;
        }

        public void SetRange(float min, float max)
        {
            this.rangeMin = min;
            this.rangeMax = max;
        }

        public void SetIsResetable(bool v)
        {
            this.resetable = v;
        }

        public SensorType type
        {
            get 
            {
                return Type;
            }

            set
            {
                Type = value;
            }
        }

        public int[] shape 
        {
            get
            {
                return Shape;
            }

            set
            {
                Shape = value;
            }
        }

        public void SetAgent(BasicAgent own)
        {
            agent = own;
        }

        public virtual void OnSetup(Agent agent)
        {
        }

        public virtual float GetFloatValue() {
            return 0;
        }

        public virtual string GetStringValue() {
            return string.Empty;
        }

        public virtual bool GetBoolValue() {
            return false;
        }

        public virtual byte[] GetByteArrayValue() {
            return null;
        }

        public virtual int GetIntValue() {
            return 0;
        }

        public virtual int[] GetIntArrayValue() {
            return null;
        }

        public virtual float[] GetFloatArrayValue() {
            return null;
        }

        public SensorType GetSensorType()
        {
            return type;
        }

        public string GetName()
        {
            return name;
        }

        public string GetKey()
        {
            return perceptionKey;
        }

        public int[] GetShape()
        {
            return shape;
        }

        public bool IsState()
        {
            return isState;
        }

        public bool IsResetable()
        {
            return resetable;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public int GetStackedObservations()
        {
            return stackedObservations;
        }

        public void SetKey(string newkey)
        {
            this.perceptionKey = newkey;
        }
        
        public void SetShape(int[] newshape) 
        {
            this.shape = newshape;
        }

        public void SetIsActive(bool v)
        {
            isActive = v;
        }

        public void SetIsInput(bool v)
        {
            isInput = v;
        }

        public void SetStackedObservations(int v)
        {
            this.stackedObservations = v;
        }

        public void SetSensorType(SensorType t)
        {
            this.type = t;
        }

        public BasicAgent GetAgent()
        {
            return this.agent;
        }

        public virtual void OnReset(Agent agent) {

        }
    }
}
