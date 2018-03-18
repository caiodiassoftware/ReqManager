using ReqManager.Data.DataAcess;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Artifact.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReqManager.Data.Repositories.Artifact.Classes
{
    public class ArtifactRequirementTraceabilityMatrixRepository : IArtifactRequirementTraceabilityMatrixRepository
    {
        private ReqManagerEntities context { get; set; }

        public ArtifactRequirementTraceabilityMatrixRepository(IDbFactory dbFactory)
        {
            context = dbFactory.Init();
        }

        public DataTable getMatrix(int ProjectID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ReqManagerDataEntities"].ConnectionString);
                conn.Open();

                string sql = @" DECLARE @Target NVARCHAR(MAX);
                                        DECLARE @Query NVARCHAR(2000);

                                        SELECT
	                                        @Target = STUFF((SELECT DISTINCT
			                                        (O)
		                                        FROM (SELECT
				                                        ',' + QUOTENAME(R.code) AS O
			                                        FROM LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS AS L
			                                        INNER JOIN REQ.REQUIREMENT AS R
				                                        ON R.RequirementID = L.RequirementID
                                                        AND R.ProjectID = " + ProjectID + @"
                                                    ) AS Tab
		                                        FOR XML PATH (''), TYPE)
	                                        .value('.', 'NVARCHAR(MAX)')
	                                        , 1, 1, '');

                                        SET @Query = N'SELECT * FROM
                                                                                            (
                                                                                                SELECT  
														                                        R.code as req,
														                                        A.code as Artifact,
														                                        '' X'' AS Flag
														                                        FROM LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS AS L
														                                        INNER JOIN PROJ.PROJECT_ARTIFACT AS A
															                                        ON A.ProjectArtifactID = L.ProjectArtifactID
                                                                                                    AND A.ProjectID = " + ProjectID + @"
														                                        INNER JOIN REQ.REQUIREMENT AS R
															                                        ON R.RequirementID = L.RequirementID
                                                                                            ) as x
                                                                                            PIVOT
                                                                                            (
                                                                                                max(Flag)
                                                                                                for req in (' + @Target + ')
                                                                                            ) AS PIV
                                                                                            ';
                                        EXEC SP_EXECUTESQL @Query ";

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
