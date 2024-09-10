using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace DemoRemoteMacroBenchmark;

class Program
{
    static void Main(string[] args)
    {
        var bdnConfig =
            DefaultConfig.Instance.AddJob(
                Job.MediumRun.WithStrategy(RunStrategy.Monitoring)
                    .WithPowerPlan(PowerPlan.UserPowerPlan)
                    .AsDefault());
        
        bdnConfig.WithOption(ConfigOptions.JoinSummary, true); // Join the summary of all benchmarks
        bdnConfig.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)); // Order the summary

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, bdnConfig);//new DebugInProcessConfig());  
    }
}