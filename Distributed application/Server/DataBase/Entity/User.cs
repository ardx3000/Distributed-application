namespace Server.DataBase.Entity
{
    public class User
    {
        public int UserID {  get; set; } //Primary key
        public string Username { get; set; }
        public string Password { get; set; }
        private int _role;
        public int Role
        {
            get { return _role; }

            set
            {
                if (value >= 0 && value <=3)
                {
                    _role =  value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Role must be between 0 and 3.");
                }
            }
        }
        public ICollection<Items> Items { get; set; }
        public ICollection<Logs> Logs { get; set; }
    }

}
