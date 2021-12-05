namespace Lab7_team1
{
    public class Group
    {
        public string Name { get; set; } = "";
        public int GroupID { get; private set; }

        public Group(int groupId, string name)
        {
            Name = name;
            GroupID = groupId;
        }

        public Group() { }
        public override string ToString() => $"Группа \"{Name}\" с ID <{GroupID}>";
    }
}
