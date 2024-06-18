namespace Server.DataBase.Entity
{
    public class User
    {
        public int Id {  get; set; }
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
    }
}
