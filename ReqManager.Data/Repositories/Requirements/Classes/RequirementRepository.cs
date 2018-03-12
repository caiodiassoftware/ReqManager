using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementRepository : RepositoryBase<Requirement>, IRequirementRepository
    {
        public RequirementRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public DataTable getDataSetRequirementImportanceAndCost(int ProjectID = 0)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ReqManagerDataEntities"].ConnectionString);
                conn.Open();

                string sql = @"SELECT 
                            P.code AS PrjCode,
                            SP.StakeholderID as StakeholderProject,
                            SP.importanceValue as StakeholderProjectImportance,
                            R.code as ReqCode,
                            SP.StakeholderID as StakeholderRequirement,
                            SR.importanceValue as RequirementStakeholderImportance,
                            R.cost as Cost
                            FROM REQ.REQUIREMENT AS R
                            INNER JOIN PROJ.STAKEHOLDER_REQUIREMENT AS SR
	                            ON SR.RequirementID = R.RequirementID
                            INNER JOIN PROJ.STAKEHOLDERS_PROJECT AS SP
	                            ON SP.StakeholdersProjectID = SR.StakeholdersProjectID
                            INNER JOIN PROJ.PROJECT AS P
	                            ON P.ProjectID = R.ProjectID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDataSetRequirementPreconditions(int ProjectID = 0)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ReqManagerDataEntities"].ConnectionString);
                conn.Open();

                string sql = @"SELECT 
                            LR.code AS LinkCode, 
                            RequirementOrigin.code as ReqOriginCode, 
                            T.description as LinkDescription, 
                            RequirementTarget.code as ReqTargetCode
                            FROM LINK.LINK_BETWEEN_REQUIREMENT AS LR
                            INNER JOIN REQ.REQUIREMENT AS RequirementOrigin
	                            ON RequirementOrigin.RequirementID = LR.RequirementOriginID
                            INNER JOIN REQ.REQUIREMENT AS RequirementTarget
	                            ON RequirementTarget.RequirementID = LR.RequirementTargetID
                            INNER JOIN LINK.TYPE_LINK AS T
	                            ON LR.TypeLinkID = T.TypeLinkID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDataSetRequirementPreconditionsAndImportanceAndCost(int ProjectID = 0)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ReqManagerDataEntities"].ConnectionString);
                conn.Open();

                string sql = @"SELECT 
                            P.code AS PrjCode,
                            SP.StakeholderID as StakeholderProject,
                            SP.importanceValue as StakeholderProjectImportance,
                            LR.code AS LinkCode, 
                            RequirementOrigin.code as ReqOriginCode, 
                            T.description as LinkDescription, 
                            RequirementTarget.code as ReqTargetCode,
                            SP.StakeholderID as StakeholderRequirement,
                            SR.importanceValue as RequirementStakeholderImportance,
                            RequirementOrigin.cost as Cost
                            FROM LINK.LINK_BETWEEN_REQUIREMENT AS LR
                            INNER JOIN REQ.REQUIREMENT AS RequirementOrigin
	                            ON RequirementOrigin.RequirementID = LR.RequirementOriginID
                            INNER JOIN REQ.REQUIREMENT AS RequirementTarget
	                            ON RequirementTarget.RequirementID = LR.RequirementTargetID
                            INNER JOIN LINK.TYPE_LINK AS T
	                            ON LR.TypeLinkID = T.TypeLinkID
                            LEFT JOIN PROJ.STAKEHOLDER_REQUIREMENT AS SR
	                            ON SR.RequirementID = RequirementOrigin.RequirementID
                            LEFT JOIN PROJ.STAKEHOLDERS_PROJECT AS SP
	                            ON SP.StakeholdersProjectID = SR.StakeholdersProjectID
                            LEFT JOIN PROJ.PROJECT AS P
	                            ON P.ProjectID = RequirementOrigin.ProjectID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
