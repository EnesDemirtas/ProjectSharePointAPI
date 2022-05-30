namespace PSP.Api.Contracts.Category.Request; 

public class CreateCategory {
    [Required]
    public string CategoryName { get; set; }

    [Required]
    public string CategoryDescription { get; set; }
    
}