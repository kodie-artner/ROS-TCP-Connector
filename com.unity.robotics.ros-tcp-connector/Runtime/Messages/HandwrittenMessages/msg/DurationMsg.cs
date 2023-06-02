using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.BuiltinInterfaces
{
    public class DurationMsg : Message
    {
        public const string k_RosMessageName = "builtin_interfaces/Duration";
        public override string RosMessageName => k_RosMessageName;

        public int sec;
        public int nanosec;

        public DurationMsg()
        {
            this.sec = 0;
            this.nanosec = 0;
        }

        public DurationMsg(int sec, int nanosec)
        {
            this.sec = sec;
            this.nanosec = nanosec;
        }

        public static DurationMsg Deserialize(MessageDeserializer deserializer) => new DurationMsg(deserializer);

        DurationMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.sec);
            if (ROSConfig.ROS2)
            {
                uint nanosec;
                deserializer.Read(out nanosec);
                this.nanosec = (int)nanosec;
            }
            else
            {
                deserializer.Read(out this.nanosec);
            }
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.sec);
            if (ROSConfig.ROS2)
            {
                serializer.Write((uint)this.nanosec);
            }
            else
            {
                serializer.Write(this.nanosec);
            }
        }

        public override string ToString()
        {
            return "Duration: " +
            "\nsec: " + sec.ToString() +
            "\nnanosec: " + nanosec.ToString();
        }


#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
