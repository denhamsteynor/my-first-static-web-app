public class SensorData
{
    [Key]
    public long ID { get; set; }  // Matches BIGINT IDENTITY(1,1)

    public DateTime? LogDate { get; set; }  // Nullable datetime

    [MaxLength(10)]  // Matches VARCHAR(10)
    public string SensorType { get; set; } = string.Empty;

    [Required]
    public double SensorValue { get; set; }  // Matches FLOAT
}