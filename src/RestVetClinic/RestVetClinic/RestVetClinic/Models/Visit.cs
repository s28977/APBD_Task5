namespace RestVetClinic.Models;

public class Visit
{
    public DateTime Date { get; set; }
    public int AnimalId { get; set; }
    public string Description { get; set; }
    public double price { get; set; }
}