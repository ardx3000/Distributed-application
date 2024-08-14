namespace Server.DataBase.Entity
{
    public class Items
    {
        public int ItemID { get; set; }//primary key
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal? TotalPrice { get; set; }
        public int UserID {  get; set; }//Forign key

        // Navigation property
        public User User { get; set; }
    }
}
