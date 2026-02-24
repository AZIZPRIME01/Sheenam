using ADotNet.Models.Pipelines.GithubPipelines.DotNets;

internal class PullRequest : PullRequestEvent
{
    public string[] Branches { get; set; }
}