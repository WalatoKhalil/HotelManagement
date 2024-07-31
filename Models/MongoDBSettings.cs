namespace HotelManagementSystem.Models
{
    // MongoDB settings, used to configure the connection to the database
    public class MongoDBSettings
    {
        // Connection string for MongoDB
        public string ConnectionString { get; set; } = null!;
        // Name of the database 
        public string DatabaseName { get; set; } = null!;
        // Name of the rooms collection
        public string RoomsCollectionName { get; set; } =  null!;
        public string BookingsCollectionName { get; set; } = null!;
        public string CustomersCollectionName { get; set; } = null!;
        public string AvailabilityCollectionName { get; set; } = null!;
        
    }
}
