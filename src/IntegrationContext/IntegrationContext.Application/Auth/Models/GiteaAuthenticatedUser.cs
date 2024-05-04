namespace IntegrationContext.Application.Auth.Models;

public class GiteaAuthenticatedUser
{
    public int id { get; set; }
    public bool active { get; set; }
    public string? avatar_url { get; set; }
    public DateTime created { get; set; }
    public string? description { get; set; }
    public string? email { get; set; }
    public int followers_count { get; set; }
    public int following_count { get; set; }
    public string? full_name { get; set; }
    public bool is_admin { get; set; }
    public string? language { get; set; }
    public DateTime last_login { get; set; }
    public string? location { get; set; }
    public required string login { get; set; }
    public string? login_name { get; set; }
    public bool prohibit_login { get; set; }
    public bool restricted { get; set; }
    public int starred_repos_count { get; set; }
    public string? visibility { get; set; }
    public string? website { get; set; }
}
