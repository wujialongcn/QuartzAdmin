using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Iesi.Collections.Generic;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;

namespace QuartzAdmin.web.Models
{
    [ActiveRecord(Table = "tbl_instances")]
    public class InstanceModel : ActiveRecordValidationBase<InstanceModel>
    {
        public InstanceModel()
        {
            InstanceProperties = new HashedSet<InstancePropertyModel>();
        }

        [PrimaryKey(Generator = PrimaryKeyType.Identity)]
        public virtual int InstanceID { get; set; }
        [Property, ValidateNonEmpty]
        public virtual string InstanceName { get; set; }

        [HasMany(typeof(InstancePropertyModel), Table = "tbl_instanceproperties",
                 ColumnKey = "InstanceID",
                 Cascade = ManyRelationCascadeEnum.All, Inverse = true)]
        public virtual Iesi.Collections.Generic.ISet<InstancePropertyModel> InstanceProperties { get; set; }



        private IScheduler _CurrentScheduler = null;
        public IScheduler GetQuartzScheduler()
        {
            if (_CurrentScheduler == null)
            {
                System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();

                foreach (InstancePropertyModel prop in this.InstanceProperties)
                {
                    props.Add(prop.PropertyName, prop.PropertyValue);
                }
                ISchedulerFactory sf = new StdSchedulerFactory(props);
                _CurrentScheduler = sf.GetScheduler().Result;
            }

            return _CurrentScheduler;

        }

        public IQueryable<string> FindAllGroups()
        {
            IScheduler sched = this.GetQuartzScheduler();

            //List<string> groups = new List<string>();

            //string[] jobGroups = sched.JobGroupNames;
            //string[] triggerGroups = sched.TriggerGroupNames;

            //foreach (string jg in jobGroups)
            //{
            //    groups.Add(jg);
            //}

            //foreach (string tg in triggerGroups)
            //{
            //    if (!groups.Contains(tg))
            //    {
            //        groups.Add(tg);
            //    }
            //}

            return sched.GetJobGroupNames().Result.AsQueryable();
            //return sched.JobGroupNames.AsQueryable();
        }

        public List<IJobDetail> GetAllJobs(string groupName)
        {
            List<IJobDetail> jobs = new List<IJobDetail>();
            IScheduler sched = this.GetQuartzScheduler();
            var jobKeys = sched.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)).Result;

            foreach (var key in jobKeys)
            {
                jobs.Add(sched.GetJobDetail(key).Result);
            }
            return jobs;
        }

        public List<IJobDetail> GetAllJobs()
        {
            List<IJobDetail> jobs = new List<IJobDetail>();
            IScheduler sched = this.GetQuartzScheduler();

            var jobKeys = sched.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result;

            foreach (var key in jobKeys)
            {
                jobs.Add(sched.GetJobDetail(key).Result);
            }
            return jobs;
        }

        public List<ITrigger> GetAllTriggers(string groupName)
        {
            List<ITrigger> triggers = new List<ITrigger>();
            IScheduler sched = this.GetQuartzScheduler();
            var allTriggerKeys = sched.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(groupName)).Result;

            foreach (var triggerKey in allTriggerKeys)
            {
                triggers.Add(sched.GetTrigger(triggerKey).Result);
            }
            return triggers;
        }

        public List<ITrigger> GetAllTriggers()
        {
            List<ITrigger> triggers = new List<ITrigger>();
            IScheduler sched = this.GetQuartzScheduler();
            var allTriggerKeys = sched.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup()).Result;


            foreach (var triggerKey in allTriggerKeys)
            {
                triggers.Add(sched.GetTrigger(triggerKey).Result);
            }
            return triggers;
        }



    }
}
