namespace Models
{
    public class Department
    {
        public int _id { get; set;}
        public string _title {get;set;}

        public Department(int id, string title)
        {
            this._id = id;
            this._title = title;
        }

        // public int getId(){ return this._id;}
        // public void setId(int id){ this._id = id;}
        // public string getTitle(){ return this._title;}
        // public void setTitle(string title){ this._title = title;}
    }
}