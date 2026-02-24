using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var githubPipeline = new GithubPipeline
{
    Name = "Sheenam Build Pipeline",

    OnEvents = new Events
    {
        PullRequest = new PullRequest
        {
            Branches = new string[] { "master" }
        },

        Push = new PushEvent
        {
            Branches = new string[] { "master" }
        }
    },

    Jobs = new Dictionary<string, Job>
    {
        {
            "build", 
            new Job
            {
                RunsOn = BuildMachines.WindowsLatest,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV2
                    {
                        Name = "Check Out code"
                    },
                    new SetupDotNetTaskV1
                    {
                        Name = "Setup .NET 10.0",

                        TargetDotNetVersion = new TargetDotNetVersion
                        {
                            DotNetVersion = "10.0"
                        }
                    },
                    new RestoreTask
                    {
                        Name = "Restore Nuget Packages"
                    },
                    new DotNetBuildTask
                    {
                        Name = "Build the project"
                    },

                    new TestTask
                    {
                        Name = "Run Unit Tests"
                    }
                }
            }
        }
    }
};

var adotNetClient = new ADotNetClient();
adotNetClient.SerializeAndWriteToFile(
    githubPipeline,
    path: @"C:\\Users\\Aziz\\OneDrive\\Desktop\\Sheenam\\.github\\workflows\\dotnet.yml");