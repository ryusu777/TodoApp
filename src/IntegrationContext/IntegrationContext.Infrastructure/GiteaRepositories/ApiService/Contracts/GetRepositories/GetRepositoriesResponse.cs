using System.Text.Json.Serialization;

namespace IntegrationContext.Infrastructure.GiteaRepositories.ApiService.Contracts.GetRepositories;

public class GetRepositoriesResponse
{
    public class Repository
    {
        public class RepositoryOwner
        {
            [JsonPropertyName("username")]
            public required string Username { get; set; }
        }

        [JsonPropertyName("id")]
        public required int Id { get; set; }

        [JsonPropertyName("owner")]
        public required RepositoryOwner Owner { get; set; }

        [JsonPropertyName("name")]
        public required string RepoName { get; set; }

        public string RepoOwner => Owner.Username;
    }

    [JsonPropertyName("ok")]
    public required bool Ok { get; set; }

    [JsonPropertyName("data")]
    public required ICollection<Repository> Data { get; set; }

}
