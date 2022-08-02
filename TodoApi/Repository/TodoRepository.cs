using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repository
{
    public class TodoRepository
    {
        private MySqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = "Server=localhost;Database=todo;Uid=root;Pwd=123456789;";
            //string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new MySqlConnection(constr);

        }
        //To Add Employee details    
        public bool AddTodo(Todo obj)
        {

            connection();
            MySqlCommand com = new MySqlCommand("AddTodo", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Title", obj.Title);
            com.Parameters.AddWithValue("@AddDate", obj.AddDate);
            com.Parameters.AddWithValue("@IsDone", obj.IsDone);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }

        }
        //To view employee details with generic list     
        public List<Todo> GetAllTodo()
        {
            connection();
            List<Todo> lst = new List<Todo>();

            MySqlCommand com = new MySqlCommand("ShowTodo", con);
            com.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                lst.Add(

                    new Todo
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        AddDate = Convert.ToDateTime(dr["AddDate"]),
                        IsDone = Convert.ToBoolean(dr["IsDone"])

                    }
               );
            }

            return lst;
        }

        public Todo GetById(int Id)
        {
            connection();
            Todo todo = new Todo();

            MySqlCommand com = new MySqlCommand("GetById", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@TodoId", Id);

            MySqlDataAdapter da = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                todo = new Todo()
                {

                    Id = Convert.ToInt32(dr["Id"]),
                    Title = Convert.ToString(dr["Title"]),
                    AddDate = Convert.ToDateTime(dr["AddDate"]),
                    IsDone = Convert.ToBoolean(dr["IsDone"])

                };
            }

            return todo;
        }

        //To Update Employee details
        public bool UpdateTodo(Todo obj)
        {

            connection();
            MySqlCommand com = new MySqlCommand("UpdateTodo", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@TodoId", obj.Id);
            com.Parameters.AddWithValue("@Title", obj.Title);
            com.Parameters.AddWithValue("@AddDate", obj.AddDate);
            com.Parameters.AddWithValue("@IsDone", obj.IsDone);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        ////To delete Employee details    
        public bool DeleteTodo(int Id)
        {

            connection();
            MySqlCommand com = new MySqlCommand("DeleteTodo", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@TodoId", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
