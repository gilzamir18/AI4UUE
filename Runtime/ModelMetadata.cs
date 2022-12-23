using System.Collections;
using System.Collections.Generic;
using System;
using ai4u;

namespace ai4u
{
    [Serializable]
    public struct ModelInput
    {
        public string name;
        public int[] shape;
        public SensorType type;

        public ModelInput(string name, SensorType type, int[] shape, int stackedObservations)
        {
            this.name = name;
            if (stackedObservations == 1)
            {
                this.shape = shape;
            }
            else
            {
                this.shape = new int[shape.Length + 1];
                this.shape[0] = stackedObservations;
                for (int i = 1; i < this.shape.Length; i++)
                {
                    this.shape[i] = shape[i];
                }
            }

            this.type = type;
        }
    }

    [Serializable]
    public struct ModelOutput
    {
        public string name;
        public bool isContinuous;
        public int[] shape;


        public ModelOutput(string name, int[] shape, bool isContinuous)
        {
            this.name = name;
            this.shape = shape;
            this.isContinuous = isContinuous;
        }
    }

    [Serializable]
    public class ModelMetadata
    {
        public ModelInput[] inputs;
        public ModelOutput[] outputs;

        public ModelMetadata(int inputCount, int outputCount)
        {
            inputs = new ModelInput[inputCount];
            outputs = new ModelOutput[outputCount];
        }

        public void SetInput(int idx, ModelInput i)
        {
            this.inputs[idx] = i;
        }

        public ModelInput GetInput(int idx)
        {
            return this.inputs[idx];
        }

        public void SetOutput(int idx, ModelOutput o)
        {
            this.outputs[idx] = o;
        }

        public ModelOutput GetOutput(int idx)
        {
            return this.outputs[idx];
        }

        public int InputCount()
        {
            return inputs.Length;
        }

        public int OutputCount()
        {
            return outputs.Length;
        }
    }
}
