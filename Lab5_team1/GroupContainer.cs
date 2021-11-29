using System.Collections.Generic;

namespace Lab5_team1
{
    public class GroupContainer
    {
        public List<Group> Groups { get; private set; } = new List<Group>();

        public void AddGroup(Group group) => Groups.Add(group);
        public void RemoveGroup(Group group) => Groups.Remove(group);
        public void ChangeStudentGroup(Student student, string srcGroupID, string dstGroupID)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].GroupID == srcGroupID)
                {
                    if (Groups[i].Students.Contains(student))
                    {
                        Groups[i].RemoveStudent(student);
                        break;
                    }
                }
            }
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].GroupID == dstGroupID)
                {
                    Groups[i].AddStudent(student);
                    break;
                }
            }
        }
    }
}
