namespace DebtsWebApi.Entities
{
    public class DebtResult
    {
       public int Id { get; set; }
       public int DebtTypeId { get; set; }
       public int BusinessUnitId { get; set; }
       public long DepartmentId { get; set; }
       public long DebtorId { get; set; }
       public string DebtorName { get; set; }
       public int Month { get; set; }
       public int Year { get; set; }
       public float Cost { get; set; }
       public int Count { get; set; }
       public string Description { get; set; }
    }
}
