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

        public DataTable DataSetDependencies(int ProjectID = 0)
        {
            string sql = @"SELECT '[' + RO.code + ']' AS ReqCode, 
                        (SELECT '[' + CAST(R.code AS VARCHAR(10)) + ']'
                        FROM LINK.LINK_BETWEEN_REQUIREMENT as L
                        INNER JOIN REQ.REQUIREMENT AS R
	                        ON R.RequirementID = L.RequirementTargetID
                        WHERE L.RequirementOriginID = RO.RequirementID
                        FOR XML PATH('')) AS Dependecies
                        FROM LINK.LINK_BETWEEN_REQUIREMENT as LR
                        INNER JOIN REQ.REQUIREMENT AS RO
		                        ON RO.RequirementID = LR.RequirementOriginID";
            return generateDataTable(sql);
        }

        public DataTable DataSetPriorities(int ProjectID = 0)
        {
            string sql = @"SELECT '[' + RO.code + ']' AS ReqCode, 
                        (SELECT '[' + CAST(R.code AS VARCHAR(10)) + ']'
                        FROM LINK.LINK_BETWEEN_REQUIREMENT as L
                        INNER JOIN REQ.REQUIREMENT AS R
	                        ON R.RequirementID = L.RequirementTargetID
                        WHERE L.RequirementOriginID = RO.RequirementID
                        FOR XML PATH('')) AS Dependecies
                        FROM LINK.LINK_BETWEEN_REQUIREMENT as LR
                        INNER JOIN REQ.REQUIREMENT AS RO
		                        ON RO.RequirementID = LR.RequirementOriginID";
            return generateDataTable(sql);
        }

        public DataTable DataSetRequirementsCost(int ProjectID = 0)
        {
            string sql = @"SELECT code, cost FROM REQ.REQUIREMENT";
            return generateDataTable(sql);
        }

        public DataTable DataSetStakeholderImportances(int ProjectID = 0)
        {
            string sql = @"DECLARE @Requirements NVARCHAR(MAX);
                        DECLARE @Query NVARCHAR(2000);

                        SELECT @Requirements = STUFF((
                        SELECT DISTINCT(O) FROM(
                        SELECT ',' + QUOTENAME(R.code) as O
                        FROM REQ.REQUIREMENT AS R
                        INNER JOIN PROJ.STAKEHOLDER_REQUIREMENT AS SR
	                        ON SR.RequirementID = R.RequirementID
                        INNER JOIN PROJ.STAKEHOLDERS_PROJECT AS SP
	                        ON SP.StakeholdersProjectID = SR.StakeholdersProjectID
                        ) AS Tab
                        FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)')
                        ,1,1,'');

                        SET @Query = N'SELECT * FROM
                        (
                            SELECT U.nickName, 
                            SP.importanceValue as ImportanceToProject, 
                            R.code as ReqCode, 
                            SR.importanceValue as importanceValueRequirement 
                            FROM PROJ.STAKEHOLDERS_PROJECT AS SP
                        INNER JOIN PROJ.STAKEHOLDERS AS S
                        ON S.StakeholderID = SP.StakeholderID
                        INNER JOIN ACCESS.USERS AS U
	                        ON U.UserID = S.UserID
                        INNER JOIN PROJ.STAKEHOLDER_REQUIREMENT AS SR
	                        ON SR.StakeholdersProjectID = SP.StakeholdersProjectID
                        INNER JOIN REQ.REQUIREMENT AS R
	                        ON R.RequirementID = SR.RequirementID
                        ) as x
                        PIVOT
                        (
                            max(importanceValueRequirement)
                            for ReqCode in ('+ @Requirements +')
                        ) AS PIV
                        ';
                        EXEC SP_EXECUTESQL @Query";
            return generateDataTable(sql);
        }

        private DataTable generateDataTable(string sql)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ReqManagerDataEntities"].ConnectionString);
                conn.Open();
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
