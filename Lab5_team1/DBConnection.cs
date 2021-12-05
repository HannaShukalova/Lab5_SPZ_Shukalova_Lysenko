using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;

namespace Lab7_team1
{
    public class DBConnection
    {
        private static string SELECT_ALL_STUDENTS = "SELECT * FROM Students ORDER BY gr_id, st_id ASC";

        private static string SELECT_ALL_GROUPS = "SELECT * FROM Groups ORDER BY gr_id ASC";

        private static string SELECT_GROUP_BY_NAME = "SELECT * FROM Groups WHERE gr_name = :groupName";

        private static string ADD_NEW_GROUP = "INSERT INTO Groups (gr_name) VALUES (:name)";

        private static string ADD_NEW_STUDENT = "INSERT INTO Students (first_name, patronymic, last_name, age, gr_id) " +
            "VALUES (:firstName, :patronymic, :lastName, :age, :groupId)";

        private static string DELETE_GROUP = "DELETE FROM Groups WHERE gr_id = :groupID ";

        private static string DELETE_STUDENT = "DELETE FROM Students " +
            "WHERE st_id = :studentId";

        private static string DELETE_STUDENT_BY_GROUP_ID = "DELETE FROM Students " +
            "WHERE gr_id = :groupId";

        private static string CHANGE_GROUP_BY_ID = "UPDATE Groups SET gr_name = :newName WHERE gr_id = :groupId";

        private static string CHANGE_STUDENT_BY_ID = "UPDATE Students SET first_name = :firstName, patronymic = :patronymic, last_name = :lastName WHERE st_id = :studentId";

        private static string CHANGE_STUDENT_GROUP_ID = "UPDATE Students SET gr_id = :groupId WHERE st_id = :studentId";



        private static string connString =
           "DATA SOURCE=localhost:1521/xe; " +
           "PERSIST SECURITY INFO=True; " +
           "USER ID=sys; " +
           "DBA Privilege=SYSDBA; " +
           "password=admin; " +
           "Pooling = False;";

        public OracleConnection getConnection()
        {
            try
            {
                OracleConnection connection = new OracleConnection(connString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Connection error", ex);
            }
        }

        public ObservableCollection<Student> getAllStudents()
        {
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(SELECT_ALL_STUDENTS, connection))
                {
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine(executeStudent(reader));
                        students.Add(executeStudent(reader));
                    }

                    return students;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while loading data (students)", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public Group getGroupByName(string groupName)
        {
            OracleConnection connection = null;
            Group group = new Group();
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(SELECT_GROUP_BY_NAME, connection))
                {
                    OracleParameter nameParam = new OracleParameter(":groupName", groupName);
                    command.Parameters.Add(nameParam);

                    Console.WriteLine(groupName);

                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine(executeGroup(reader));
                        group = executeGroup(reader);
                    }

                    return group;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while choosing group", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public ObservableCollection<Group> getAllGroups()
        {
            ObservableCollection<Group> groups = new ObservableCollection<Group>();
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(SELECT_ALL_GROUPS, connection))
                {
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine(executeGroup(reader));
                        groups.Add(executeGroup(reader));
                    }

                    return groups;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while loading data (groups)", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void addNewGroup(Group newGroup)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(ADD_NEW_GROUP, connection))
                {
                    OracleParameter nameParam = new OracleParameter("name", newGroup.Name);
                    command.Parameters.Add(nameParam);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Added objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void addNewStudent(Student newStudent)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(ADD_NEW_STUDENT, connection))
                {
                    OracleParameter[] parameters = new OracleParameter[] {
                            new OracleParameter(":firstName", newStudent.FirstName),
                            new OracleParameter(":lastName", newStudent.LastName),
                            new OracleParameter(":patronymic", newStudent.Patronymic),
                            new OracleParameter(":age", newStudent.Age),
                            new OracleParameter(":groupId", newStudent.StudentGroupID)
                    };
                    command.Parameters.AddRange(parameters);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Added objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void changeStudentById(Student newStudent, int studentId)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(CHANGE_STUDENT_BY_ID, connection))
                {
                    OracleParameter[] parameters = new OracleParameter[] {
                            new OracleParameter(":firstName", newStudent.FirstName),
                            new OracleParameter(":lastName", newStudent.LastName),
                            new OracleParameter(":patronymic", newStudent.Patronymic),
                            new OracleParameter(":studentId", studentId)
                    };
                    command.Parameters.AddRange(parameters);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Updated objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating data", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void changeStudentGroupById(int newGroupid, int studentId)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(CHANGE_STUDENT_GROUP_ID, connection))
                {
                    OracleParameter[] parameters = new OracleParameter[] {
                            new OracleParameter(":groupId", newGroupid),
                            new OracleParameter(":studentId", studentId)
                    };
                    command.Parameters.AddRange(parameters);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Updated objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating data", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void deleteGroupById(int groupId)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(DELETE_GROUP, connection))
                {
                    OracleParameter groupIdParam = new OracleParameter(":groupId", groupId);
                    command.Parameters.Add(groupIdParam);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Deleted objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void deleteStudentById(int studentId)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(DELETE_STUDENT, connection))
                {
                    OracleParameter studentIdParam = new OracleParameter(":studentId", studentId);
                    command.Parameters.Add(studentIdParam);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Deleted objects: {0}", number);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void deleteStudentByGroupId(int groupId)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(DELETE_STUDENT_BY_GROUP_ID, connection))
                {
                    OracleParameter groupIdParam = new OracleParameter(":groupId", groupId);
                    command.Parameters.Add(groupIdParam);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Deleted objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        public void changeGroupById(Group group, string newName)
        {
            OracleConnection connection = null;
            try
            {
                connection = getConnection();
                using (OracleCommand command = new OracleCommand(CHANGE_GROUP_BY_ID, connection))
                {
                    OracleParameter[] parameters = new OracleParameter[] {
                            new OracleParameter(":newName", newName),
                            new OracleParameter(":groupId", group.GroupID)
                    };

                    command.Parameters.AddRange(parameters);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Changed objects: {0}", number);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating data", ex);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
            }
        }

        private Student executeStudent(OracleDataReader reader)
        {
            return new Student(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetInt32(4),
                reader.GetInt32(5));
        }

        private Group executeGroup(OracleDataReader reader)
        {
            return new Group(
                reader.GetInt32(0),
                reader.GetString(1));
        }
    }
}
