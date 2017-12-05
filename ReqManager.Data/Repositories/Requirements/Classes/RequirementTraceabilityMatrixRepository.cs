using ReqManager.Data.DataAcess;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementTraceabilityMatrixRepository : IRequirementTraceabilityMatrixRepository
    {
        private ReqManagerEntities context { get; set; }

        public RequirementTraceabilityMatrixRepository(IDbFactory dbFactory)
        {
            context = dbFactory.Init();
        }

        public DataTable getMatrix(int ProjectID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ReqManagerDataEntities"].ConnectionString);
                conn.Open();

                string sql = @"                     DECLARE @Target NVARCHAR(MAX);
                                                    DECLARE @Query NVARCHAR(2000);

	                                                SELECT @Target = STUFF((SELECT DISTINCT(O) FROM(SELECT ',' + QUOTENAME(R.code) as O
                                                            from LINK.LINK_BETWEEN_REQUIREMENT AS L
                                                            INNER JOIN REQ.REQUIREMENT AS R
																ON R.RequirementID = L.RequirementTargetID
                                                            ) AS Tab
                                                            FOR XML PATH(''), TYPE
                                                            ).value('.', 'NVARCHAR(MAX)')
                                                            ,1,1,'');

	                                                SET @Query = N'SELECT * FROM
                                                    (
                                                        SELECT RO.code as Origin, RT.code as T, ''X'' AS Flag
                                                        FROM LINK.LINK_BETWEEN_REQUIREMENT as L
                                                        INNER JOIN REQ.REQUIREMENT AS RT
																ON RT.RequirementID = L.RequirementTargetID
                                                                AND RT.ProjectID = " + ProjectID + @"
														INNER JOIN REQ.REQUIREMENT AS RO
																ON RO.RequirementID = L.RequirementOriginID
                                                                AND RO.ProjectID = " + ProjectID + @"
                                                    ) as x
                                                    PIVOT
                                                    (
                                                        max(Flag)
                                                        for T in ('+ @Target +')
                                                    ) AS PIV
                                                    ';
                                                    EXEC SP_EXECUTESQL @Query";

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
