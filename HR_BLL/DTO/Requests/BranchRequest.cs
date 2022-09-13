namespace HR_BLL.DTO.Requests
{
    public class BranchRequest
    {
        public int Id { get; set; }

        public string Descript { get; set; } = null!;
        
        public string Address { get; set; } = null!;
    }
}
