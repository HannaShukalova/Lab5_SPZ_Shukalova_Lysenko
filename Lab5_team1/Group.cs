using System;
using System.Runtime.Serialization;

namespace Lab5_team1
{
    [DataContract]
    public class Group
    {
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public string GroupID { get; private set; } = "";

        public Group(string name = "Dumbasses")
        {
            Name = name;
            GroupID = Guid.NewGuid().ToString();
        }
        public void ChangeGroupName(string newName = "Dumbasses") => Name = newName;
        public override string ToString() => $"Группа \"{Name}\" с ID <{GroupID}>";
    }
}
