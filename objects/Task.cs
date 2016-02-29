using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Configuration;

namespace ToDoList
{
  public class Task
  {
    private int _id;
    private string _description;
    private DateTime _dueDateTime;
    private bool _taskCompleted;

    public Task(string Description, DateTime dueDateTime, bool TaskCompleted, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _dueDateTime = dueDateTime;
      _taskCompleted = TaskCompleted;
    }

    public override bool Equals(System.Object otherTask)
    {
      if (!(otherTask is Task))
      {
        return false;
      }
      else
      {
        Task newTask = (Task) otherTask;
        bool idEquality = (this.GetId() == newTask.GetId());
        bool descriptionEquality = (this.GetDescription() == newTask.GetDescription());
        bool DateTimeEquality = (this.GetDateTime() == newTask.GetDateTime());
        bool TaskCompletedEquality = (this.GetTaskCompleted() == newTask.GetTaskCompleted());
        return (descriptionEquality && descriptionEquality && DateTimeEquality && TaskCompletedEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public DateTime GetDateTime()
    {
      return _dueDateTime;
    }
    public void SetDateTime(DateTime dueDateTime)
    {
      _dueDateTime = dueDateTime;
    }

    public bool GetTaskCompleted()
    {
      return _taskCompleted;
    }

    public void SetTaskCompleted(bool TaskCompleted)
    {
      _taskCompleted = TaskCompleted;
    }

    public static List<Task> GetAll()
    {
      List<Task> AllTasks = new List<Task>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks order by due_date;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        DateTime taskDueDateTime = rdr.GetDateTime(2);
        bool TaskCompleted = rdr.GetBoolean(3);
        Task newTask = new Task(taskDescription, taskDueDateTime, TaskCompleted, taskId);
        AllTasks.Add(newTask);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllTasks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description, due_date, task_completed) OUTPUT INSERTED.id VALUES (@TaskDescription, @TaskDueDateTime, @TaskCompleted);", conn);

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@TaskDescription";
      descriptionParameter.Value = this.GetDescription();

      SqlParameter DateTimeParameter = new SqlParameter();
      DateTimeParameter.ParameterName = "@TaskDueDateTime";
      DateTimeParameter.Value = this.GetDateTime();

      SqlParameter TaskCompletedParameter = new SqlParameter();
      TaskCompletedParameter.ParameterName = "@TaskCompleted";
      TaskCompletedParameter.Value = this.GetTaskCompleted();

      cmd.Parameters.Add(descriptionParameter);
      cmd.Parameters.Add(DateTimeParameter);
      cmd.Parameters.Add(TaskCompletedParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Task Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks WHERE id = @TaskId;", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = id.ToString();
      cmd.Parameters.Add(taskIdParameter);
      rdr = cmd.ExecuteReader();

      int foundTaskId = 0;
      string foundTaskDescription = null;
      DateTime foundDueDateTime = new DateTime(1996-01-01);
      bool foundTaskCompleted = false;

      Console.WriteLine(foundDueDateTime);
      while(rdr.Read())
      {
        foundTaskId = rdr.GetInt32(0);
        foundTaskDescription = rdr.GetString(1);
        foundDueDateTime = rdr.GetDateTime(2);
        foundTaskCompleted = rdr.GetBoolean(3);
      }
      Task foundTask = new Task(foundTaskDescription, foundDueDateTime, foundTaskCompleted, foundTaskId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundTask;
    }

    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
     cmd.ExecuteNonQuery();
    }

    public void AddCategory(Category newCategory)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO categories_tasks (category_id, task_id) VALUES (@CategoryId, @TaskId);", conn);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = newCategory.GetId();
      cmd.Parameters.Add(categoryIdParameter);

      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = this.GetId();
      cmd.Parameters.Add(taskIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public List<Category> GetCategories()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT category_id FROM categories_tasks WHERE task_id = @TaskId;", conn);

      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = this.GetId();
      cmd.Parameters.Add(taskIdParameter);

      rdr = cmd.ExecuteReader();

      List<int> categoryIds = new List<int> {};

      while (rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        categoryIds.Add(categoryId);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      List<Category> categories = new List<Category> {};

      foreach (int categoryId in categoryIds)
      {
        SqlDataReader queryReader = null;
        SqlCommand categoryQuery = new SqlCommand("SELECT * FROM categories WHERE id = @CategoryId;", conn);

        SqlParameter categoryIdParameter = new SqlParameter();
        categoryIdParameter.ParameterName = "@CategoryId";
        categoryIdParameter.Value = categoryId;
        categoryQuery.Parameters.Add(categoryIdParameter);

        queryReader = categoryQuery.ExecuteReader();
        while (queryReader.Read())
        {
          int thisCategoryId = queryReader.GetInt32(0);
          string categoryName = queryReader.GetString(1);
          Category foundCategory = new Category(categoryName, thisCategoryId);
          categories.Add(foundCategory);
        }
        if (queryReader != null)
        {
          queryReader.Close();
        }
      }
      if (conn != null)
      {
        conn.Close();
      }
      return categories;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM tasks WHERE id = @TaskId; DELETE FROM categories_tasks WHERE task_id = @TaskId;", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = this.GetId();

      cmd.Parameters.Add(taskIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
