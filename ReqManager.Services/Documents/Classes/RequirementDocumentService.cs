using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Services.Documents.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReqManager.Services.Documents.Classes
{
    public class RequirementDocumentService : IRequirementDocumentService
    {
        private IRequirementService requirementService { get; set; }
        private IProjectService projectService { get; set; }

        public RequirementDocumentService(
            IRequirementService requirementService,
            IProjectService projectService)
        {
            this.projectService = projectService;
            this.requirementService = requirementService;
        }

        public byte[] printRequirement(int RequirementID)
        {
            try
            {
                RequirementEntity requirement = requirementService.get(RequirementID);

                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document(PageSize.A4, 40, 40, 40, 80))
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();

                            using (var html = new StringReader(getRequirementHeader() + getRequirementHtml(requirement)))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, html);
                            }

                            doc.Close();
                        }
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] printDocumentRequirementProject(int ProjectID, int RequirementTypeID)
        {
            try
            {
                List<RequirementEntity> requirements =
                    requirementService.getRequirementsByProject(ProjectID).
                    Where(r => r.RequirementTypeID.Equals(RequirementTypeID)).ToList();

                ProjectEntity project = projectService.get(ProjectID);

                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document(PageSize.A4, 40, 40, 40, 80))
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();

                            string body = string.Empty;
                            requirements.ForEach(r => body+= r.description);                            

                            using (var html = new StringReader(getProjectHeader(project) + body))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, html);
                            }

                            doc.Close();
                        }
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getProjectHeader(ProjectEntity project)
        {
            return @"<h1 style=""text - align: center; "">Requirement Document</h1>
   < hr />
   < h1 > &nbsp;</ h1 >
      < h2 style = ""padding-left: 30px;"" >< strong > Project: " + project.description + @" & nbsp;</ strong ></ h2 >
             < h2 style = ""padding-left: 30px;"" >< strong > Code:</ strong ></ h2 >
                    < h2 style = ""padding-left: 30px;"" >< strong > Phase:</ strong ></ h2 >
                           < h2 style = ""padding-left: 30px;"" >< strong > Environmental Information:</ strong ></ h2 >
                                  < h2 style = ""padding-left: 30px;"" >< strong > Management Information:</ strong ></ h2 >
                                         < h2 style = ""padding-left: 30px;"" >< strong > Start Date:</ strong ></ h2 >
                                                < h2 style = ""padding-left: 30px;"" >< strong > End Date:</ strong ></ h2 >
                                                       < p > &nbsp;</ p >
                                                          < hr />
                                                          < p > &nbsp;</ p >
                                                             < h2 style = ""padding-left: 30px;"" >< strong > Requirements </ strong ></ h2 >
                                                              < p > &nbsp;</ p > ";
        }

        private string getRequirementHeader()
        {
            return @"<h1 style=""text - align: center; ""><strong>Requirement Document</strong></h1>
            < h2 style = ""text-align: center;"" >< strong > Type: Stories User</ strong ></ h2 >
             < p > &nbsp;</ p >
             < h4 >< strong > Requirements </ strong ></ h4 >
             < p > &nbsp;</ p > ";
        }

        private string getRequirementHtml(RequirementEntity requirement)
        {
            return @"<h1 style=""text - align: center; ""><strong>Requirement Document</strong></h1>
            < h2 style = ""text-align: center;"" >< strong > Type: Stories User</ strong ></ h2 >
             < p > &nbsp;</ p >
             < h4 >< strong > Requirements </ strong ></ h4 >
             < p > &nbsp;</ p > ";
        }
    }
}
