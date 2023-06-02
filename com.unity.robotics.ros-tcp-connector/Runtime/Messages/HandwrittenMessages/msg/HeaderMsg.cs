using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Std
{
    public class HeaderMsg : Message
    {
        public const string k_RosMessageName = "std_msgs/Header";
        public override string RosMessageName => k_RosMessageName;


        //  Standard metadata for higher-level stamped data types.
        //  This is generally used to communicate timestamped data
        //  in a particular coordinate frame.

        //  sequence ID: consecutively increasing ID
        public uint seq;

        //  Two-integer timestamp that is expressed as seconds and nanoseconds.
        public BuiltinInterfaces.TimeMsg stamp;
        //  Transform frame with which this data is associated.
        public string frame_id;

        public HeaderMsg()
        {
            this.seq = 0;
            this.stamp = new BuiltinInterfaces.TimeMsg();
            this.frame_id = "";
        }

        public HeaderMsg(BuiltinInterfaces.TimeMsg stamp, string frame_id)
        {
            this.seq = seq;
            this.stamp = stamp;
            this.frame_id = frame_id;
        }

        public HeaderMsg(uint seq, BuiltinInterfaces.TimeMsg stamp, string frame_id)
        {
            this.seq = seq;
            this.stamp = stamp;
            this.frame_id = frame_id;
        }

        public static HeaderMsg Deserialize(MessageDeserializer deserializer) => new HeaderMsg(deserializer);

        HeaderMsg(MessageDeserializer deserializer)
        {
            if (!ROSConfig.ROS2)
            {
                deserializer.Read(out this.seq);
            }
            this.stamp = BuiltinInterfaces.TimeMsg.Deserialize(deserializer);
            deserializer.Read(out this.frame_id);
        }
        public override void SerializeTo(MessageSerializer serializer)
        {
            if (!ROSConfig.ROS2)
            {
                serializer.Write(seq);
            }
            serializer.Write(stamp);
            serializer.Write(this.frame_id);
        }

        public override string ToString()
        {
            return "Header: " +
            "\nstamp: " + stamp.ToString() +
            "\nframe_id: " + frame_id.ToString();
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
