using UnityEngine;
using System.Linq;
using ai4u;
using System.Collections.Generic;
using System.Text;

namespace ai4u
{
    public class ScreenSensor : Sensor
    {
            

        public bool grayScale = true;
        public Camera source;

        private Queue<byte[]> values;

        private int width;
        private int height;


        void CreateData()
        {
            values = new Queue<byte[]>(stackedObservations);
            var img = GetASCIIFrame();
            for (int i = 0; i < stackedObservations; i++)
                    values.Enqueue(img);
        }

        public override void OnSetup(Agent agent) 
        {
            width = source.targetTexture.width;
            height = source.targetTexture.height;
            type = SensorType.sstring;
            shape = new int[2]{width,  height};
            rangeMin = 0;
            rangeMax = 255;
            CreateData();
            agent.AddResetListener(this);
            this.agent = (BasicAgent) agent;
        }

        public override string GetStringValue()
        {
            values.Enqueue(GetASCIIFrame());        
            if (values.Count > stackedObservations)
            {
                values.Dequeue();
            }
            byte[] sb = new byte[0];
            foreach (var f in values)
            {
                sb = sb.Concat(f).ToArray();
            }
            return System.Convert.ToBase64String(sb);
        }

        public override void OnReset(Agent agent)
        {
            CreateData();
        }

        private byte[] GetASCIIFrame()
        {
            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture.active = source.targetTexture;

            source.Render();

            Texture2D image = new Texture2D(source.targetTexture.width, source.targetTexture.height, TextureFormat.RGBA32, false);
            image.ReadPixels(new Rect(0, 0, source.targetTexture.width, source.targetTexture.height), 0, 0);
            image.Apply();

            RenderTexture.active = activeRenderTexture;
            //image.Reinitialize(width, height, image.format, false);
            byte[] bytes = image.GetRawTextureData();
            Destroy(image);
            if (grayScale)
            {
                int CHANNELS = 4;
                byte[] gbytes = new byte[bytes.Length/CHANNELS];
                int k = 0;
                for (int i = 0; i < bytes.Length; i += CHANNELS)
                {
                    gbytes[k] = (byte)((bytes[i] + bytes[i+1] + bytes[i+2])/3.0);
                    k++;
                }
                return gbytes;
            }
            else
            {
                int CHANNELS = 4;
                byte[] gbytes = new byte[source.targetTexture.width * source.targetTexture.height * (CHANNELS-1)];
                int k = 0;
                for (int i = 0; i < bytes.Length; i += CHANNELS)
                {
                    gbytes[k] = bytes[i];
                    gbytes[k+1] = bytes[i+1];
                    gbytes[k+2] = bytes[i+2];                    
                    k += 3;
                }

                return gbytes;
            }
        }
    }
}