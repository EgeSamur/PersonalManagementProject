namespace PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;

public class UpdatePermissionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string? Description { get; set; }
}
