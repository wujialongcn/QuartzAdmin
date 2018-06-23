using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using Quartz.Impl.Matchers;

namespace QuartzAdmin.web.Models
{
    public class TriggerRepository
    {
        private InstanceModel quartzInstance;
        public TriggerRepository(string instanceName)
        {
            InstanceRepository repo = new InstanceRepository();
            quartzInstance = repo.GetInstance(instanceName);
        }

        public TriggerRepository(InstanceModel instance)
        {
            quartzInstance = instance;
        }

        public async Task<ITrigger> GetTrigger(string triggerName, string groupName)
        {
            IScheduler sched =quartzInstance.GetQuartzScheduler();

            return await sched.GetTrigger(new TriggerKey(triggerName, groupName));

        }

        public IList<TriggerStatusModel> GetAllTriggerStatus(string groupName)
        {
            IScheduler sched = quartzInstance.GetQuartzScheduler();
            string[] triggerNames = sched.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(groupName)).Result.Select(p => p.Name).ToArray();// .GetTriggerNames(groupName);
            List<TriggerStatusModel> triggerStatuses = new List<TriggerStatusModel>();
            foreach (string triggerName in triggerNames)
            {
                ITrigger trig = sched.GetTrigger(new TriggerKey(triggerName, groupName)).Result;
                TriggerState st = sched.GetTriggerState(new TriggerKey(triggerName, groupName)).Result;
                DateTimeOffset? nextFireTime = trig.GetNextFireTimeUtc();
                DateTimeOffset? lastFireTime = trig.GetPreviousFireTimeUtc();
                

                triggerStatuses.Add(new TriggerStatusModel()
                {
                    TriggerName = triggerName,
                    GroupName = groupName,
                    State = st,
                    NextFireTime = nextFireTime.HasValue?nextFireTime.Value.ToLocalTime().ToString():"",
                    LastFireTime = lastFireTime.HasValue ? lastFireTime.Value.ToLocalTime().ToString() : "",
                    JobName = trig.JobKey.Name
                });

            }

            return triggerStatuses;


        }

        public IList<TriggerStatusModel> GetAllTriggerStatus()
        {
            var groups = quartzInstance.FindAllGroups();
            List<TriggerStatusModel> triggerStatuses = new List<TriggerStatusModel>();

            foreach (string group in groups)
            {
                triggerStatuses.AddRange(GetAllTriggerStatus(group));
            }

            return triggerStatuses;
        }

        public IList<ITrigger> GetTriggersForJob(string jobName, string groupName)
        {
            ITrigger[] arrayOfTriggers = quartzInstance.GetQuartzScheduler().GetTriggersOfJob(new JobKey(jobName, groupName)).Result.ToArray();
            return arrayOfTriggers.ToList(); 
        }

    }
}
