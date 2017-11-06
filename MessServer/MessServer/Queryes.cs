using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessServer
{
    static class Queryes
    {
        const string path = @"Data Source=DESKTOP-4OT12Q3;Initial Catalog=MessengerDB;Integrated Security=True";

        public static string Registration(string message)
        {
            string[] str = message.Split(':');
            using (SqlConnection cn = new SqlConnection(path))
            {
                try
                {
                    cn.Open();
                    string insert = string.Format("INSERT INTO Users" + "(Name, SurName, NickName, Gender, Birthday, Country, City, Pass) VALUES(@Name, @SurName, @NickName, @Gender, @Birthday, @Country, @City, @Pass)");
                    SqlCommand cmd = new SqlCommand(insert, cn);
                    cmd.Parameters.AddWithValue("@Name", str[0]);
                    cmd.Parameters.AddWithValue("@SurName", str[1]);
                    cmd.Parameters.AddWithValue("@NickName", str[2]);
                    cmd.Parameters.AddWithValue("@Gender", str[3]);
                    cmd.Parameters.AddWithValue("@Birthday", str[4]);
                    cmd.Parameters.AddWithValue("@Country", str[5]);
                    cmd.Parameters.AddWithValue("@City", str[6]);
                    cmd.Parameters.AddWithValue("@Pass", str[7]);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("New user is added");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                finally
                {
                    cn.Close();
                }
                return null;
            }
        }

        public static string Authorisation(string message)
        {
            string[] str = message.Split(':');
            using (SqlConnection cn = new SqlConnection(path))
            {
                try
                {
                    cn.Open();
                    string select = "SELECT NickName, Pass FROM Users";
                    SqlCommand cmd = new SqlCommand(select, cn);
                    SqlDataReader data = cmd.ExecuteReader();
                    bool authorisation = false;
                    while (data.Read())
                    {
                        if (data[0].ToString() == str[0] && data[1].ToString() == str[1])
                            authorisation = true;
                    }
                    if (authorisation == false)
                        throw new Exception("not found");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                finally
                {
                    cn.Close();
                }
                return null;
            }
        }

        public static string MyInfo(string message)
        {
            string[] str = message.Split(':');
            using (SqlConnection cn = new SqlConnection(path))
            {
                string result = "";
                try
                {
                    cn.Open();
                    string select = "SELECT * FROM Users";
                    SqlCommand cmd = new SqlCommand(select, cn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        if (data[2].ToString() == str[0])
                        {
                            for (int i = 0; i < data.FieldCount; i++)
                                result = result + data[i].ToString() + ":";
                            return result;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                finally
                {
                    cn.Close();
                }
                return null;
            }
        }

        public static void UpdatePass(string message)
        {
            string[] str = message.Split(':');
            using (SqlConnection cn = new SqlConnection(path))
            {
                try
                {
                    cn.Open();
                    string update = string.Format("UPDATE Users SET Pass = '{0}' WHERE NickName = '{1}'", str[7], str[2]);
                    SqlCommand cmd = new SqlCommand(update, cn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public static string Search(string message)
        {
            string[] str = message.Split(':');
            using (SqlConnection cn = new SqlConnection(path))
            {
                string result = "";
                string select = "";
                try
                {
                    cn.Open();
                    switch (str[0])
                    {
                        case "Gender":
                            select = "SELECT * FROM Users WHERE Gender='" + str[1] + "'";
                            break;
                        case "Name":
                            select = "SELECT * FROM Users WHERE Name like '%" + str[1] + "%'";
                            break;
                        case "Surname":
                            select = "SELECT * FROM Users WHERE SurName like '%" + str[1] + "%'";
                            break;
                        case "Nickname":
                            select = "SELECT * FROM Users WHERE NickName like '%" + str[1] + "%'";
                            break;
                        case "Country":
                            select = "SELECT * FROM Users WHERE Country like '%" + str[1] + "%'";
                            break;
                        case "City":
                            select = "SELECT * FROM Users WHERE City like '%" + str[1] + "%'";
                            break;
                    }
                    SqlCommand cmd = new SqlCommand(select, cn);
                    SqlDataReader data = cmd.ExecuteReader();
                    result = Convertations.DataReaderToString(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                finally
                {
                    cn.Close();
                }
                return result;
            }
        }

        public static void CreateChat(string message)
        {
            string[] s = message.Split(';');
            if (!CheckEqualChats(s))
            {
                using (SqlConnection cn = new SqlConnection(path))
                {
                    try
                    {
                        cn.Open();
                        string s1, s2, s3, s4, s5;
                        s3 = s4 = s5 = "";
                        Console.WriteLine(s.Count());
                        s1 = s[0];
                        s2 = s[1];
                        if (s.Count() > 2)
                            s3 = s[2];
                        if (s.Count() > 3)
                            s4 = s[3];
                        if (s.Count() > 4)
                            s5 = s[4];

                        string insert = string.Format("INSERT INTO Chats" + "(ChatName, User1, User2, User3, User4, User5) VALUES(@ChatName, @User1, @User2, @User3, @User4, @User5)");
                        SqlCommand cmd = new SqlCommand(insert, cn);
                        cmd.Parameters.AddWithValue("@ChatName", s1 + s2 + s3 + s4 + s5);
                        cmd.Parameters.AddWithValue("@User1", s1);
                        cmd.Parameters.AddWithValue("@User2", s2);
                        cmd.Parameters.AddWithValue("@User3", s3);
                        cmd.Parameters.AddWithValue("@User4", s4);
                        cmd.Parameters.AddWithValue("@User5", s5);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("New chat created");
                        insert = string.Format("INSERT INTO Msgs" + "(ChatName, UserName, Msg, TImeOfMsg) VALUES(@ChatName, @UserName, @Msg, GETDATE())");
                        SqlCommand command = new SqlCommand(insert, cn);
                        command.Parameters.AddWithValue("@ChatName", s1 + s2 + s3 + s4 + s5);
                        command.Parameters.AddWithValue("@UserName", s1);
                        command.Parameters.AddWithValue("@Msg", "Created chat");
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            else
                Console.WriteLine("Equal chat");
        }

        public static string SelctAllUsers()
        {
            using (SqlConnection cn = new SqlConnection(path))
            {
                string result = "";
                try
                {
                    cn.Open();
                    string select = "SELECT * FROM Users";
                    SqlCommand cmd = new SqlCommand(select, cn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        for (int i = 0; i < data.FieldCount; i++)
                            result = result + data[i].ToString() + ":";
                        result += ";";
                    }
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public static string SelectChats(string message)
        {
            using (SqlConnection cn = new SqlConnection(path))
            {
                string result = "";
                try
                {
                    cn.Open();
                    string select = string.Format(
                        "SELECT User1, User2, User3, User4, User5 FROM  Chats WHERE User1 = '{0}' or User2 = '{0}' or User3 = '{0}' or User4 = '{0}' or User5 = '{0}'", message);
                    SqlCommand cmd = new SqlCommand(select, cn);
                    SqlDataReader data = cmd.ExecuteReader();

                    while (data.Read())
                    {
                        for (int i = 0; i < 5; i++)
                            if (data[i].ToString() != message)
                                result += data[i].ToString() + ":";
                        result += ";";
                    }
                    Console.WriteLine(result);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public static void CreateMsg(string message)
        {
            string[] str = message.Split(':');
            using (SqlConnection cn = new SqlConnection(path))
            {
                try
                {
                    cn.Open();
                    string insert = string.Format("INSERT INTO Msgs" + "(ChatName, UserName, Msg, TimeOfMsg) VALUES(@ChatName, @UserName, @Msg, GETDATE())");
                    SqlCommand cmd = new SqlCommand(insert, cn);
                    cmd.Parameters.AddWithValue("@ChatName", str[0]);
                    cmd.Parameters.AddWithValue("@UserName", str[1]);
                    cmd.Parameters.AddWithValue("@Msg", str[2]);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public static bool CheckEqualChats(string[] users)
        {
            using (SqlConnection cn = new SqlConnection(path))
            {
                try
                {
                    cn.Open();
                    int counter = 0;
                    int counterOfData = 0;
                    string select = "SELECT User1, User2, User3, User4, User5 FROM Chats";
                    SqlCommand cmd = new SqlCommand(select, cn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (data[i].ToString() != "")
                                counterOfData++;
                            for (int j = 0; j < users.Count(); j++)
                            {
                                if (data[i].ToString() == users[j])
                                    counter++;
                            }
                        }
                        if (counter == counterOfData)
                            return true;
                        else
                            counter = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return false;
        }
    }
}
//TODO: 271 -- select ChatName + LastMsg;