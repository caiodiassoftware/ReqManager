using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.DataAcess
{
    public class ReqManagerEntities : DbContext
    {
        public ReqManagerEntities() : base("ReqManagerDataEntities")
        {

        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<CONTROLLER_ACTION> CONTROLLER_ACTION { get; set; }
        public virtual DbSet<ROLE> ROLE { get; set; }
        public virtual DbSet<ROLE_CONTROLLER_ACTION> ROLE_CONTROLLER_ACTION { get; set; }
        public virtual DbSet<USER_ROLE> USER_ROLE { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<ARTIFACT_TYPE> ARTIFACT_TYPE { get; set; }
        public virtual DbSet<HISTORY_PROJECT_ARTIFACT> HISTORY_PROJECT_ARTIFACT { get; set; }
        public virtual DbSet<PROJECT_ARTIFACT> PROJECT_ARTIFACT { get; set; }
        public virtual DbSet<ATTRIBUTES> ATTRIBUTES { get; set; }
        public virtual DbSet<ATTRIBUTES_TYPE_LINK> ATTRIBUTES_TYPE_LINK { get; set; }
        public virtual DbSet<LINK_ARTIFACT_ATTRIBUTES> LINK_ARTIFACT_ATTRIBUTES { get; set; }
        public virtual DbSet<LINK_BETWEEN_REQUIREMENT> LINK_BETWEEN_REQUIREMENT { get; set; }
        public virtual DbSet<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS> LINK_BETWEEN_REQUIREMENTS_ARTIFACTS { get; set; }
        public virtual DbSet<LINK_REQUIREMENT_ATTRIBUTES> LINK_REQUIREMENT_ATTRIBUTES { get; set; }
        public virtual DbSet<TYPE_LINK> TYPE_LINK { get; set; }
        public virtual DbSet<HISTORY_PROJECT> HISTORY_PROJECT { get; set; }
        public virtual DbSet<PROJECT> PROJECT { get; set; }
        public virtual DbSet<PROJECT_PHASES> PROJECT_PHASES { get; set; }
        public virtual DbSet<PROJECT_REQUIREMENTS> PROJECT_REQUIREMENTS { get; set; }
        public virtual DbSet<STAKEHOLDER_REQUIREMENT> STAKEHOLDER_REQUIREMENT { get; set; }
        public virtual DbSet<STAKEHOLDERS> STAKEHOLDERS { get; set; }
        public virtual DbSet<STAKEHOLDERS_PROJECT> STAKEHOLDERS_PROJECT { get; set; }
        public virtual DbSet<MEASURE_IMPORTANCE> MEASURE_IMPORTANCE { get; set; }
        public virtual DbSet<REQUIREMENT> REQUIREMENT { get; set; }
        public virtual DbSet<REQUIREMENT_ACTION_HISTORY> REQUIREMENT_ACTION_HISTORY { get; set; }
        public virtual DbSet<REQUIREMENT_RATIONALE> REQUIREMENT_RATIONALE { get; set; }
        public virtual DbSet<REQUIREMENT_STATUS> REQUIREMENT_STATUS { get; set; }
        public virtual DbSet<REQUIREMENT_TEMPLATE> REQUIREMENT_TEMPLATE { get; set; }
        public virtual DbSet<REQUIREMENT_TYPE> REQUIREMENT_TYPE { get; set; }
        public virtual DbSet<STAKEHOLDER_CLASSIFICATION> STAKEHOLDER_CLASSIFICATION { get; set; }
        public virtual DbSet<HISTORY_TASK> HISTORY_TASK { get; set; }
        public virtual DbSet<STATUS_TASK> STATUS_TASK { get; set; }
        public virtual DbSet<TASK> TASK { get; set; }
        public virtual DbSet<TASK_TYPE> TASK_TYPE { get; set; }
        public virtual DbSet<TASK_TYPE_TEMPLATE> TASK_TYPE_TEMPLATE { get; set; }
        public virtual DbSet<USER_TASK> USER_TASK { get; set; }
    }
}
