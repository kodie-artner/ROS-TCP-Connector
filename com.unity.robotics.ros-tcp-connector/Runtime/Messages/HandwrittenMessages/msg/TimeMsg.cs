using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.BuiltinInterfaces
{
    public class TimeMsg : Message
    {
        public static string k_RosMessageName;
        public override string RosMessageName => k_RosMessageName;

        public uint sec;
        public uint nanosec;

        public static void SetMessageName()
        {
            if (ROSConfig.ROS2)
            {
                k_RosMessageName = "builtin_interfaces/Time";
            }
            else
            {
                k_RosMessageName = "std_msgs/Time";
            }
        }

        public TimeMsg()
        {
            TimeMsg.SetMessageName();
            this.sec = 0;
            this.nanosec = 0;
        }

        public TimeMsg(uint sec, uint nanosec)
        {
            TimeMsg.SetMessageName();
            this.sec = sec;
            this.nanosec = nanosec;
        }

        public static TimeMsg Deserialize(MessageDeserializer deserializer) => new TimeMsg(deserializer);

        TimeMsg(MessageDeserializer deserializer)
        {
            if (ROSConfig.ROS2)
            {
                int sec;
                deserializer.Read(out sec);
                this.sec = (uint)sec;
            }
            else
            {
                deserializer.Read(out this.sec);
            }
            deserializer.Read(out this.nanosec);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            if (ROSConfig.ROS2)
            {
                serializer.Write((uint)this.sec);
            }
            else
            {
                serializer.Write(this.sec);
            }
            serializer.Write(this.nanosec);
        }

        public override string ToString()
        {
            return "Time: " +
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
            TimeMsg.SetMessageName();
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
